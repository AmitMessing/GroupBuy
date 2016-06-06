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
        }

        public static void IndexProduct(Product p_objProduct)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = UTF8Encoding.UTF8;
                ProductIndexData currProductIndexData = new ProductIndexData(p_objProduct.Id, p_objProduct.Name);
                string strResponse = 
                    client.UploadString(m_strElasticSearchConnectionString + "/" + INDEX_NAME + "/" + INDEX_TYPE + "/", "POST", JsonConvert.SerializeObject(currProductIndexData));
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
        private Guid m_intProductId;
        private string m_strProductName;

        public Guid ProductId { 
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

        public ProductIndexData(Guid p_intProductId, string p_strProductName)
        {
            this.m_intProductId = p_intProductId;
            this.m_strProductName = p_strProductName;
        }
    }
}