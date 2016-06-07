using System;
using System.Linq;
using System.Web.Http;
using GroupBuyServer.Models;
using GroupBuyServer.Utils;
using GroupBuyServer.ViewModels;

namespace GroupBuyServer.Controllers
{
    [RoutePrefix("GroupBuyServer/api/buyers")]
    public class BuyersController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Save(ProductBuyerViewModel productBuyerViewModel)
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                using (var tran = session.BeginTransaction())
                {
                    var product = session.Get<Product>(productBuyerViewModel.ProductId);
                    var user = session.Get<User>(productBuyerViewModel.BuyerId);
                    if (product.Buyers.Any(x => x.Id == productBuyerViewModel.BuyerId) == false)
                    {
                        product.Buyers.Add(user);
                        session.Save(product);
                        tran.Commit();
                    }
                }
            }

            return Ok(productBuyerViewModel);
        }
    }
}
