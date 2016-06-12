using System;
using System.Linq;
using System.Web.Http;
using GroupBuyServer.Models;
using GroupBuyServer.Utils;
using GroupBuyServer.ViewModels;
using NHibernate.Linq;

namespace GroupBuyServer.Controllers
{
    public class ReviewsController : ApiController
    {
        protected IHttpActionResult SaveInDb(Review review)
        {
            float newRating;

            using (var session = NHibernateHandler.CurrSession)
            {
                using (var tran = session.BeginTransaction())
                {
                    var reviews =
                    session.Query<Review>()
                        .Where(x => (x.OnUserId == review.OnUserId && x.IsOnSeller == review.IsOnSeller)).Select(x => x.Rating).ToList();

                    if (!reviews.Any())
                    {
                        newRating = review.Rating;
                    }
                    else
                    {
                        double avg = ((double)(reviews.Sum() + review.Rating)) / ((double)(reviews.Count() + 1));
                        newRating = (float)Math.Round(avg, MidpointRounding.AwayFromZero);
                    }

                    var user = session.Get<User>(review.OnUserId);

                    if (review.IsOnSeller)
                    {
                        user.SellerRate = newRating;
                    }
                    else
                    {
                        user.BuyerRate = newRating;
                    }

                    session.Save(review);
                    session.Save(user);
                    tran.Commit();
                }
            }

            return Ok(new { newRating });
        }
    }

    [RoutePrefix("GroupBuyServer/api/onSellerReviews")]
    public class OnSellerReviewsController : ReviewsController
    {
        [HttpGet]
        [ActionName("reviews")]
        public IHttpActionResult Get(Guid id)
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                var reviews =
                    session.Query<Review>()
                        .Where(x => (x.OnUserId == id && x.IsOnSeller)).ToList();

                var reviewersId = reviews.Select(x => x.ReviewerId).ToList();
                var users = session.Query<User>()
                    .Where(x => reviewersId.Contains(x.Id))
                    .ToDictionary(x => x.Id, y => y.UserName);

                return Ok(reviews.Select(x => new ReviewViewModel(x, users[x.ReviewerId])));
            }
        }

        [HttpPost]
        [ActionName("save")]
        public IHttpActionResult Save(Review review)
        {
            review.IsOnSeller = true;
            return SaveInDb(review);
        }
    }

    [RoutePrefix("GroupBuyServer/api/onBuyerReviews")]
    public class OnBuyerReviewsController : ReviewsController
    {
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                var reviews =
                    session.Query<Review>()
                        .Where(x => (x.OnUserId == id && !x.IsOnSeller)).ToList();

                var reviewersId = reviews.Select(x => x.ReviewerId).ToList();
                var users = session.Query<User>()
                    .Where(x => reviewersId.Contains(x.Id))
                    .ToDictionary(x => x.Id, y => y.UserName);

                return Ok(reviews.Select(x => new ReviewViewModel(x, users[x.ReviewerId])));
            }
        }

        [HttpPost]
        public IHttpActionResult Save(Review review)
        {
            review.IsOnSeller = false;
            return SaveInDb(review);
        }

    }
}
