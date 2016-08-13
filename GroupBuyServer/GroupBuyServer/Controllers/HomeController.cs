using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using GroupBuyServer.Models;
using GroupBuyServer.Utils;
using GroupBuyServer.ViewModels;
using Newtonsoft.Json;
using NHibernate.Criterion;

namespace GroupBuyServer.Controllers
{
    [RoutePrefix("/GroupBuyServer/api/home")]
    public class HomeController : ApiController
    {
        [HttpGet]
        [ActionName("home")]
        public IHttpActionResult Get()
        {
            IList<NewestProductViewModel> newestProducts = new List<NewestProductViewModel>();
            using (var session = NHibernateHandler.CurrSession)
            {
                var list = session.QueryOver<Product>().OrderBy(x => x.PublishDate).Desc.Take(15).List();
                foreach (var product in list)
                {
                    newestProducts.Add(new NewestProductViewModel()
                    {
                        Id = product.Id,
                        Image = product.Image,
                        PublishDate = product.PublishDate,
                        Name = product.Name,
                        BasicPrice = product.BasicPrice,
                        Description = product.Description,
                        EndDate = product.EndDate
                    });

                }

                return Ok(newestProducts);
            }
        }
    }
}