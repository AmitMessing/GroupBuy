using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GroupBuyServer.Models;
using GroupBuyServer.Utils;
using GroupBuyServer.ViewModels;
using NHibernate.Linq;
using NHibernate.Criterion;
using NHibernate;
using System.Collections;

namespace GroupBuyServer.Controllers
{
    [RoutePrefix("GroupBuyServer/api/products")]
    public class ProductsController : ApiController
    {
        [HttpGet]
        [ActionName("product")]
        public IHttpActionResult Product(Guid id)
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
                    Seller = new SellerViewModel
                    {
                        Id = product.Seller.Id, 
                        UserName = product.Seller.UserName, 
                        FullName = product.Seller.FirstName + " " + product.Seller.LastName
                    },
                    Buyers = product.Buyers.Select(x => 
                        new BuyerViewModel
                        {
                            UserName = x.UserName, 
                            FullName = x.FirstName + " " + x.LastName
                        }).ToList(),
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

                    ElasticSearchHandler.IndexProduct(productToSave);

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

        
        [HttpGet]
        [ActionName("suggestions")]
        public IHttpActionResult Suggestions(Guid id)
        {
            List<Product> lstSuggestions = new List<Product>();
            using (var session = NHibernateHandler.CurrSession)
            {
                Product objProduct = session.QueryOver<Product>().Where(x => x.Id == id).SingleOrDefault();
                foreach (User currBuyer in objProduct.Buyers)
                {
                    List<Product> currBuyerProductsId = session.Query<Product>().Where(p => p.Buyers.Any<User>(b => b.Id == currBuyer.Id)).ToList();
                    ISQLQuery temp = session.CreateSQLQuery("select product_id,count(*) from rel_product_buyers where user_id in(select user_id from t_products where user_id in buyers)");
                    IEnumerable temp2 = temp.List();
                }
            }

            return Ok(lstSuggestions.Count > 0? lstSuggestions: null);
        }

//        [HttpPost]
//        [Route("products/{id}/buyers")]
//        public IHttpActionResult R(Guid id, [FromBody]User buyer)
//        {
//            using (var session = NHibernateHandler.CurrSession)
//            {
//                using (var tran = session.BeginTransaction())
//                {
//                    var product = session.Get<Product>(id);
//                    if (product.Buyers.Contains(buyer) == false)
//                    {
//                        product.Buyers.Add(buyer);
//                        session.Save(product);
//                        tran.Commit();
//                    }
//                }
//            }
//
//            return Ok(id);
//        }

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
