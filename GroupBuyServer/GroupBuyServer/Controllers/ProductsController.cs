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
                    Image = product.Image,
                    Seller = new SellerViewModel
                    {
                        Id = product.Seller.Id, 
                        UserName = product.Seller.UserName, 
                        FullName = product.Seller.FirstName + " " + product.Seller.LastName,
                        Rating = product.Seller.SellerRate
                    },
                    Buyers = product.Buyers.Select(x => 
                        new BuyerViewModel
                        {
                            Id = x.Id,
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
        [ActionName("save")]
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
                        Image = product.Image,
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
        public IHttpActionResult Suggestions(Guid id, Guid userId)
        {
            List<NewestProductViewModel> lstSuggestions = new List<NewestProductViewModel>();
            using (var session = NHibernateHandler.CurrSession)
            {
                Product objProduct = session.QueryOver<Product>().Where(x => x.Id == id).SingleOrDefault();

                Dictionary<Product, int> dicProductsCount = new Dictionary<Product, int>();
                List<User> lstSimilarBuyers = RecommenderSystem.GetUserIdsInNeighborhood(userId, objProduct.Buyers);

                foreach (User currBuyer in lstSimilarBuyers)
                {
                    IQueryable<Product> currBuyerProducts = session.Query<Product>().Where(p => p.Buyers.Any<User>(b => b.Id == currBuyer.Id));

                    foreach (Product currProduct in currBuyerProducts)
                    {
                        if(!dicProductsCount.ContainsKey(currProduct))
                        {
                            dicProductsCount.Add(currProduct, 0);
                        }
                        dicProductsCount[currProduct] += 1;
                    }
                }
                lstSuggestions = dicProductsCount.OrderByDescending(x => x.Value).Take(10).Select(p => p.Key).ToList()
                                                 .ConvertAll<NewestProductViewModel>(x=> new NewestProductViewModel(x));
            }

            return Ok(lstSuggestions);
        }

        [HttpPost]
        [ActionName("UpdateEndDate")]
        public IHttpActionResult UpdateEndDate(ProductViewModel product)
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                using (var tran = session.BeginTransaction())
                {
                    var productFromDb = session.Get<Product>(product.Id);
                    productFromDb.EndDate = product.EndDate.AddDays(1);

                    session.Save(productFromDb);
                    tran.Commit();
                    return Ok();
                }
            }
        }
    }
}
