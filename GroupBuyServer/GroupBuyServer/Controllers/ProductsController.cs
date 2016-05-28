using System;
using System.Web.Http;
using GroupBuyServer.Models;
using GroupBuyServer.Utils;

namespace GroupBuyServer.Controllers
{
    [RoutePrefix("/GroupBuyServer/api/products")]
    public class ProductsController : ApiController
    {
        [HttpGet]
//        [Route("{id}")]
        public IHttpActionResult Get(Guid id)
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                var product = session.Get<Product>(id);
//                var b = product.Buyers;
                if (product != null)
                {
                    return Ok(product);
                }

                return BadRequest("Product not exists");
            }
        }
    }
}
