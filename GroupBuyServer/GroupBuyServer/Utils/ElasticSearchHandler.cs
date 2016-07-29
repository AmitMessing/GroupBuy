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
        private const int PAGE_SIZE = 10;
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
                ProductIndexData currProductIndexData = new ProductIndexData(p_objProduct.Id, p_objProduct.Name, p_objProduct.Image, p_objProduct.BasicPrice);
                string strResponse = 
                    client.UploadString(m_strElasticSearchConnectionString + "/" + INDEX_NAME + "/" + INDEX_TYPE + "/", "POST", JsonConvert.SerializeObject(currProductIndexData));
            }
        }

        public static List<ProductIndexData> Search(string p_strSearchQuery, int p_intPage, out int p_intPagesNeeded)
        {
            List<ProductIndexData> results = null;

            using (WebClient client = new WebClient())
            {
                var searchQuery = new
                {
                    query = new
                    {
                        multi_match = new
                        {
                            query = p_strSearchQuery,
                            fields = new[] { "Name^10", "Name.stemmed^2", "Name.shingles^2", "Name.ngrams" },
                            @operator = "and"
                        }
                    },
                    highlight = new
                    {
                        order = "score",
                        fields = new
                        {
                            Name = new {
                                matched_fields = new[] { "Name", "Name.stemmed", "Name.shingles", "Name.ngrams" },
                                Name = new { 
                                    pre_tags = new[] { "<strong>" }, 
                                    post_tags = new[] { "</strong>" }
                                }
                            }
                        }
                    }
                };

                string strSearchQuery = JsonConvert.SerializeObject(searchQuery);
                //string strSearchQuery = "{\"query\":{ \"multi_match\":{ \"query\":\"" + p_strSearchQuery + "\", \"type\":\"most_fields\", \"fields\": [\"ProductName\",\"ProductNameRow^2\"], \"operator\": \"and\"}}, \"highlight\": {\"fields\": {\"ProductName\":{}, \"ProductNameRow\":{}}}}";
                //string strSearchQuery = "{" +
                //                              "\"query\": {" +
                //                                "\"bool\": {" +
                //                                  "\"should\": [" +
                //                                    "{ \"match\": {" +
                //                                        "\"ProductNameRaw\":  {" +
                //                                          "\"query\": \"" + p_strSearchQuery + "\"," +
                //                                          "\"boost\": 2" +
                //                                    "}}}," +
                //                                    "{ \"match\": {" +
                //                                        "\"ProductName\":  {" +
                //                                          "\"query\": \"" + p_strSearchQuery + "\"" +
                //                                    "}}}" +
                //                                  "]" +
                //                                "}" +
                //                              "}" +
                //                            "}";
                string strResponse =
                    client.UploadString(m_strElasticSearchConnectionString + "/" + INDEX_NAME + "/" + INDEX_TYPE + "/_search?size=" + PAGE_SIZE + "&from=" + ((p_intPage - 1) * PAGE_SIZE), "POST", strSearchQuery);
                JObject objResponse = JsonConvert.DeserializeObject<JObject>(strResponse);
                JToken objHits = objResponse.GetValue("hits");
                int intTotal = objHits.Value<int>("total");
                if (intTotal > 0)
                {
                    results = new List<ProductIndexData>();
                    JArray objHitsResult = objHits.Value<JArray>("hits");

                    foreach (JObject currResult in objHitsResult.Children<JObject>())
                    {
                        ProductIndexData currProductIndexData = currResult.GetValue("_source").ToObject<ProductIndexData>();
                        currProductIndexData.Name = currResult.GetValue("highlight").Value<JArray>("Name").First.Value<string>();
                        results.Add(currProductIndexData);
                    }
                }
                p_intPagesNeeded = (int)Math.Ceiling(((double)intTotal / PAGE_SIZE));
            }
            
            return results;
        }
    }

    public class ProductIndexData
    {
        public Guid Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
        public string Image
        {
            get;
            set;
        }

        public double Price 
        { 
            get;
            set; 
        }

        public ProductIndexData(Guid p_intProductId, string p_strProductName, string p_strProductImage, double p_dblPrice)
        {
            this.Id = p_intProductId;
            this.Name = p_strProductName;
            this.Image = p_strProductImage;
            this.Price = p_dblPrice;
        }
    }
}