using Elasticsearch.Net;
using GroupBuyServer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace GroupBuyServer.Utils
{
    public static class ElasticSearchHandler
    {
        private const string INDEX_NAME = "group_buy";
        private const string INDEX_TYPE = "products"; 
        private static string m_strElasticSearchConnectionString = null;

        static ElasticSearchHandler()
        {
            m_strElasticSearchConnectionString = 
                ConfigurationManager.ConnectionStrings["ElasticSearchConnectionString"].ConnectionString;

            try
            {
                WebRequest request = WebRequest.Create(m_strElasticSearchConnectionString + "/" + INDEX_NAME + "/");
                WebResponse response = request.GetResponse();
            }
            catch (WebException ex)
            {
                HttpWebResponse response = (HttpWebResponse)ex.Response;
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    using (WebClient client = new WebClient())
                    {
                        var temp = new
                        {
                            settings = new
                            {
                                analysis = new
                                {
                                    analyzer = new
                                    {
                                        products_analyzer = new
                                        {
                                            type = "snowball",
                                            language = "English"
                                        }
                                    }
                                }
                            },
                            mappings = new
                            {
                                products = new
                                {
                                    properties = new
                                    {
                                        ProductId = new
                                        {
                                            type = "long",
                                            index = "not_analyzed"
                                        },
                                        ProductName = new
                                        {
                                            type = "string",
                                            analyzer = "products_analyzer"
                                        }
                                    }
                                }
                            }
                        };

                        string data = JsonConvert.SerializeObject(temp);

                        //string data = "{\"index\" : {\"analysis\" : {\"analyzer\" : {\"products_analyzer\" : {\"type\" : \"snowball\",\"language\" : \"English\"}}}}}";

                        string strResponse =
                            client.UploadString(m_strElasticSearchConnectionString + "/" + INDEX_NAME + "/", "PUT", data);
                    }
                }
            }
        }

        public static void IndexAllProducts()
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                IList<Product> lstAllProducts = session.QueryOver<Product>().List();

                if (lstAllProducts != null && lstAllProducts.Count > 0)
                {
                    using (WebClient client = new WebClient())
                    {
                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
                        client.Encoding = UTF8Encoding.UTF8;

                        foreach (Product currProduct in lstAllProducts)
                        {
                            ProductIndexData currProductIndexData = new ProductIndexData(currProduct.Id, currProduct.Name);
                            string strResponse = 
                                client.UploadString(m_strElasticSearchConnectionString + "/" + INDEX_NAME + "/" + INDEX_TYPE + "/", "POST", JsonConvert.SerializeObject(currProductIndexData));
                        }
                    }
                }
            }
        }

        public static List<ProductIndexData> Search(string p_strSearchQuery)
        {
            List<ProductIndexData> results = null;

            using (WebClient client = new WebClient())
            {
                var searchQuery = new {
                    query = new {
                        match = new {
                            ProductName = p_strSearchQuery
                        }
                    },
                    highlight = new { 
                        fields = new {
                            ProductName = new {}
                        }
                    }
                };

                string strSearchQuery = JsonConvert.SerializeObject(searchQuery);

                string strResponse =
                    client.UploadString(m_strElasticSearchConnectionString + "/" + INDEX_NAME + "/" + INDEX_TYPE + "/_search?pretty", "POST", strSearchQuery);
                JObject objResponse = JsonConvert.DeserializeObject<JObject>(strResponse);
                JToken objHits = objResponse.GetValue("hits");
                if (objHits.Value<int>("total") > 0)
                {
                    results = new List<ProductIndexData>();
                    JArray objHitsResult = objHits.Value<JArray>("hits");

                    foreach (JObject currResult in objHitsResult.Children<JObject>())
                    {
                        ProductIndexData currProductIndexData = currResult.GetValue("_source").ToObject<ProductIndexData>();
                        currProductIndexData.ProductName = currResult.GetValue("highlight").Value<JArray>("ProductName").First.Value<string>();
                        results.Add(currProductIndexData);
                    }
                }
            }

            return results;
        }
    }

    public class ProductIndexData
    {
        private int m_intProductId;
        private string m_strProductName;

        public int ProductId { 
            get {
                return this.m_intProductId;
            } 
            set {
                this.m_intProductId = value;
            } 
        }

        public string ProductName
        {
            get
            {
                return this.m_strProductName;
            }
            set
            {
                this.m_strProductName = value;
            }
        }

        public ProductIndexData(int p_intProductId, string p_strProductName)
        {
            this.m_intProductId = p_intProductId;
            this.m_strProductName = p_strProductName;
        }
    }
}