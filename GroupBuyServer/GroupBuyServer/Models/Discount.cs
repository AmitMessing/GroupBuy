using System;
using FluentNHibernate.Mapping;

namespace GroupBuyServer.Models
{

    public class Discount
    {
        public virtual Guid Id { get; set; }
        public virtual Guid ProductId { get; set; }
        public virtual int UsersAmount { get; set; }
        public virtual float Precent { get; set; }
    }

    public class DiscountMap : ClassMap<Discount>
    {
        public DiscountMap()
        {
            Table("rel_product_discount");

            Id(x => x.Id, "id");
            Map(x => x.Precent, "present");
            Map(x => x.UsersAmount, "users_amount");
            Map(x => x.ProductId).Column("product_id");
        }
    }
}
