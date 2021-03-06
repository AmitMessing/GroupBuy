﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using GroupBuyServer.Models;
using GroupBuyServer.Utils;
using GroupBuyServer.ViewModels;
using NHibernate.Linq;

namespace GroupBuyServer.Controllers
{
    [RoutePrefix("/GroupBuyServer/api/users")]
    public class UsersController : ApiController
    {
        [HttpGet]
        [ActionName("user")]
        public IHttpActionResult GetUser(Guid id)
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                var user = session.Get<User>(id);
                return Ok(user);
            }
        }

        [HttpPost]
        [ActionName("register")]
        public IHttpActionResult Save(User user)
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                using (var transaction = session.BeginTransaction())
                {
                    // check if the user name allready exists
                    var userFromDb =
                        session.QueryOver<User>().Where(x => x.FirstName == user.UserName).SingleOrDefault();
                    if (userFromDb != null)
                    {
                        return BadRequest("User allready exists");
                    }

                    user.Id = Guid.NewGuid();
                    user.RegisterDate = DateTime.Now;
                    session.Save(user);
                    transaction.Commit();
                    return Ok(user);
                }
            }
        }

        [HttpPost]
        [ActionName("login")]
        public IHttpActionResult Login(User user)
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                var userFromDb = session.QueryOver<User>()
                    .Where(x => x.UserName == user.UserName).And(x => x.Password == user.Password).SingleOrDefault();

                if (userFromDb == null)
                {
                    return BadRequest("User Not Found");
                }
                return Ok(userFromDb);
            }
        }

        [HttpPost]
        [ActionName("changePassword")]
        public IHttpActionResult ChangePassword(User user)
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                using (var transaction = session.BeginTransaction())
                {
                    var userFromDb = session.Get<User>(user.Id);

                    if (userFromDb == null)
                    {
                        return BadRequest("User Not Found");
                    }

                    userFromDb.Password = user.Password;
                    session.Save(user);
                    transaction.Commit();
                    return Ok(userFromDb);
                }
            }
        }


        [HttpGet]
        [ActionName("products")]
        public IHttpActionResult Products(Guid id)
        {
            IList<NewestProductViewModel> userProducts = new List<NewestProductViewModel>();
            using (var session = NHibernateHandler.CurrSession)
            {
               var products = session.QueryOver<Product>().Where(x => x.Seller.Id == id).OrderBy(x => x.PublishDate).Desc.Take(10).List();
                foreach (var product in products)
                {
                    userProducts.Add(new NewestProductViewModel()
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
            }
            return Ok(userProducts);
        }

        [HttpGet]
        [ActionName("reviews")]
        public IHttpActionResult GetReviews(Guid id)
        {
            var buyersReviews = new List<ReviewViewModel>();
            var sellerReviews = new List<ReviewViewModel>();
            UserResviewsViewModel viewModelToReturn;

            using (var session = NHibernateHandler.CurrSession)
            {
                var user = session.Get<User>(id);
                if (user == null)
                {
                    return BadRequest("Error in user");
                }
                
                var reviewsList = session.QueryOver<Review>().Where(x => x.OnUserId == id).List();
                foreach (var review in reviewsList)
                {
                    var reviewer =
                        session.QueryOver<User>().Where(x => x.Id == review.ReviewerId).SingleOrDefault().UserName;
                    if (review.IsOnSeller)
                    {
                        sellerReviews.Add(new ReviewViewModel(review, reviewer));
                    }
                    else
                    {
                        buyersReviews.Add(new ReviewViewModel(review, reviewer));    
                    }
                }
                 viewModelToReturn = new UserResviewsViewModel
                {
                    BuyerReviews = buyersReviews,
                    SellerReviews = sellerReviews,
                    UserId = id
                };
            }
            return Ok(viewModelToReturn);
        }
    }
}