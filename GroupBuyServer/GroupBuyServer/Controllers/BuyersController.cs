using System;
using System.Web.Http;
using GroupBuyServer.Models;
using GroupBuyServer.Utils;

namespace GroupBuyServer.Controllers
{
    [RoutePrefix("GroupBuyServer/api/products/buyers")]
    public class BuyersController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(id);
        }

        [HttpPost]
        public IHttpActionResult Save(Guid id, User buyer)
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                using (var tran = session.BeginTransaction())
                {
                    var product = session.Get<Product>(id);
                    if (product.Buyers.Contains(buyer) == false)
                    {
                        product.Buyers.Add(buyer);
                        session.Save(product);
                        tran.Commit();
                    }
                }
            }

            return Ok(id);
        }
    }
}
