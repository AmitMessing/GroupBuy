using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using GroupBuyServer.Models;
using GroupBuyServer.Utils;
using GroupBuyServer.ViewModels;
using Newtonsoft.Json.Linq;
using NHibernate.Dialect.Schema;
using NHibernate.Linq;

namespace GroupBuyServer.Controllers
{
    [RoutePrefix("/GroupBuyServer/api/purchases")]
    public class PurchasesController : ApiController
    {
        [HttpGet]
        [ActionName("userPurchases")]
        public IHttpActionResult GetUserPurchases(Guid id)
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                List<Product> purchases = session.Query<Product>()
                    .Where(x => (x.Buyers.Any(b => b.Id == id))).ToList();
                return Ok(purchases.ConvertAll(p => new NewestProductViewModel(p)));
            }
        }
    }
}