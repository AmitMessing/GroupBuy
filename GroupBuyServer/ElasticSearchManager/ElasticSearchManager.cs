using GroupBuyServer.Models;
using GroupBuyServer.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElasticSearchManager
{
    public partial class ElasticSearchManager : Form
    {
        private const string INDEX_NAME = "group_buy";
        private const string INDEX_TYPE = "products";
        private static string m_strElasticSearchConnectionString = null;

        public ElasticSearchManager()
        {
            InitializeComponent();

            m_strElasticSearchConnectionString =
                ConfigurationManager.ConnectionStrings["ElasticSearchConnectionString"].ConnectionString;
        }

        private void btnCreateIndex_Click(object sender, EventArgs e)
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
                                Id = new
                                {
                                    type = "string",
                                    index = "not_analyzed",
                                    norms = new {
                                        enabled = false
                                    }
                                },
                                Name = new
                                {
                                    type = "string",
                                    fields = new {
                                        NameRaw = new
                                        {
                                            type = "string",
                                            index = "not_analyzed"
                                        }
                                    },
                                    analyzer = "products_analyzer"
                                },
                                Image = new
                                {
                                    type = "binary"
                                },
                                Price = new
                                {
                                    type = "double",
                                    index = "not_analyzed"
                                }
                            }
                        }
                    }
                };

                string data = JsonConvert.SerializeObject(temp);

                string strResponse =
                    client.UploadString(m_strElasticSearchConnectionString + "/" + INDEX_NAME + "/", "PUT", data);
            }
        }

        private void btnDeleteIndex_Click(object sender, EventArgs e)
        {
            string strUrl = m_strElasticSearchConnectionString + "/" + INDEX_NAME + "/";

            WebRequest request = WebRequest.Create(strUrl);
            request.Method = "DELETE";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        }

        private void btnIndexAllProducts_Click(object sender, EventArgs e)
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
                            ProductIndexData currProductIndexData = new ProductIndexData(currProduct.Id, currProduct.Name, currProduct.Image, currProduct.BasicPrice);
                            string strResponse =
                                client.UploadString(m_strElasticSearchConnectionString + "/" + INDEX_NAME + "/" + INDEX_TYPE + "/", "POST", JsonConvert.SerializeObject(currProductIndexData));
                        }
                    }
                }
            }
        }
    }
}
