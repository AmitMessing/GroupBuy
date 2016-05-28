using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GroupBuyServer.Models;
using GroupBuyServer.Utils;
using GroupBuyServer.ViewModels;
using NHibernate;
using NHibernate.Linq;

namespace GroupBuyServer.Controllers
{
    [RoutePrefix("/GroupBuyServer/api/products")]
    public class ProductsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                var product = session.Get<Product>(id);
                var productViewModel = new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Categories = product.Categories.Select(x => x.Name).ToList(),
                    BasicPrice = product.BasicPrice,
                    PublishDate = product.PublishDate,
                    EndDate = product.EndDate,
                    Seller = new SellerViewModel {Id = product.Seller.Id, UserName = product.Seller.UserName},
                    Buyers = product.Buyers.Select(x => x.UserName).ToList(),
                };

                var discounts =
                    session.Query<Discount>()
                        .Where(x => x.ProductId == product.Id)
                        .Select(x => new DiscountViewModel {UsersAmount = x.UsersAmount, Precent = x.Precent});

                productViewModel.Discounts = discounts.ToList();
                return Ok(productViewModel);
            }
        }

        [HttpPost]
        public IHttpActionResult Save(ProductViewModel product)
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                using (var tran = session.BeginTransaction())
                {
                    var userFromDb = session.QueryOver<User>().Where(x => x.Id == product.Seller.Id).SingleOrDefault(); 

                    var productToSave = new Product()
                    {
                        Id = Guid.NewGuid(),
                        BasicPrice = product.BasicPrice,
                        Buyers = new List<User>(),
                        Categories = new List<Category>(),
                        Description = product.Description,
                        EndDate = product.EndDate.Date,
                        Image = GetBytes(product.Image),
                        Name = product.Name,
                        PublishDate = DateTime.Now,
                        Seller = userFromDb
                    };

                    session.Save(productToSave);
                    foreach (var discount in product.Discounts)
                    {
                        session.Save(new Discount()
                        {
                            Precent = discount.Precent,
                            Id = Guid.NewGuid(),
                            ProductId = productToSave.Id,
                            UsersAmount = discount.UsersAmount
                        });
                    }
                    tran.Commit();
                    return Ok();
                }
            }
        }
        static byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            var chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}
