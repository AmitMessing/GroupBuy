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
        [ActionName("search")]
        public IHttpActionResult Search(JObject p_objParams)
        {
            List<ProductIndexData> lstSearchResults = null;
            string strSearchText = p_objParams.Value<string>("searchText");
            int intPagesNeeded = 1;
            int intPage = 1;
            JToken objToken = null;
            if (p_objParams.TryGetValue("page", out objToken))
            {
                intPage = objToken.Value<int>();
            }
            if (!string.IsNullOrWhiteSpace(strSearchText)){
                
                lstSearchResults = ElasticSearchHandler.Search(strSearchText, intPage, out intPagesNeeded);
            }
            var result = new { searchResult = lstSearchResults, pagesNeeded = intPagesNeeded};
            return Ok(result);
        }
    }
}
