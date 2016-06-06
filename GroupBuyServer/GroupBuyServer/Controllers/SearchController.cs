using GroupBuyServer.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GroupBuyServer.Controllers
{
    public class SearchController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Search(JObject p_objSearchText)
        {
            List<ProductIndexData> lstSearchResults = null; 
            string strSearchText = p_objSearchText.Value<string>("searchText");
            if (!string.IsNullOrWhiteSpace(strSearchText)){
                lstSearchResults = ElasticSearchHandler.Search(strSearchText);
            }
            return Ok(lstSearchResults);
        }
    }
}
