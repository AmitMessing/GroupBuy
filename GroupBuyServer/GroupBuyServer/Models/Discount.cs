using System;
using FluentNHibernate.Mapping;

namespace GroupBuyServer.Models
{
    public class Discount
    {
        public virtual Guid Id { get; set; }
        public virtual int UsersAmount { get; set; }
        public virtual float Present { get; set; }
    }

    public class DiscountMap : ClassMap<Discount>
    {
        public DiscountMap()
        {
            Table("rel_product_discount");

            Id(x => x.Id, "id");
            Map(x => x.Present, "present");
            Map(x => x.UsersAmount, "users_amount");
        }
    }
}
