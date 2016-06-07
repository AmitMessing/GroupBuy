using System;
using System.Linq;
using System.Web.Http;
using GroupBuyServer.Models;
using GroupBuyServer.Utils;
using NHibernate.Linq;

namespace GroupBuyServer.Controllers
{
    public class ReviewsController : ApiController
    {
        protected IHttpActionResult SaveInDb(Review review)
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                using (var tran = session.BeginTransaction())
                {
                    session.Save(review);
                    tran.Commit();
                }
            }

            return Ok(review);
        }
    }

    [RoutePrefix("GroupBuyServer/api/onSellerReviews")]
    public class OnSellerReviewsController : ReviewsController
    {
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                var reviews =
                    session.Query<Review>()
                        .Where(x => (x.OnUserId == id && x.IsOnSeller)).ToList();

                return Ok(reviews);
            }
        }

        [HttpPost]
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
                        .Where(x => (x.OnUserId == id && x.IsOnSeller == false)).ToList();

                return Ok(reviews);
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
