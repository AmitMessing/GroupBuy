using System;
using FluentNHibernate.Mapping;

namespace GroupBuyServer.Models
{
    public class Review
    {
        public virtual Guid Id { get; set; }
        public virtual bool IsOnSeller { get; set; }
        public virtual Guid ReviewerId { get; set; }
        public virtual Guid OnUserId { get; set; }
        public virtual DateTime PublishDate { get; set; }
        public virtual string Conetent { get; set; }
        public virtual int Rating { get; set; }
    }

    public class ReviewMap : ClassMap<Review>
    {
        public ReviewMap()
        {
            Table("t_reviews");

            Id(x => x.Id, "id");
            Map(x => x.IsOnSeller, "is_on_seller");
            Map(x => x.ReviewerId, "reviewer_id");
            Map(x => x.OnUserId, "on_user_id");
            Map(x => x.PublishDate, "publish_date");
            Map(x => x.Conetent, "conetent");
            Map(x => x.Rating, "rating");
        }
    }
}