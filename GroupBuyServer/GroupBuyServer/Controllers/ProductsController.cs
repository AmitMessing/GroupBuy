﻿using System;
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
                    try
                    {
                        session.Save(product);
                        tran.Commit();
                        return Ok();
                    }
                    catch (Exception ex)
                    {

                        return BadRequest("failed to save product");
                    }
                }
            }
        }
    }
}
