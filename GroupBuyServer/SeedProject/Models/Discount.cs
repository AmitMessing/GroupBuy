﻿using FluentNHibernate.Mapping;

namespace SeedProject.Models
{
    public class Discount
    {
        public virtual Product Product { get; set; }
        public virtual int UsersAmount { get; set; }
        public virtual float Present { get; set; }

        public override bool Equals(object obj)
        {
            var otherProduct = (Discount) obj;
            return Product.Id == otherProduct.Product.Id && UsersAmount == otherProduct.UsersAmount;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class DiscountMap : ClassMap<Discount>
    {
        public DiscountMap()
        {
            Table("rel_product_discount");

            CompositeId()
                .KeyReference(x => x.Product, "product_id")
                .KeyProperty(x => x.UsersAmount, "users_amount");

            Map(x => x.Present, "present");

            References(x => x.Product, "product_id").Not.Insert();
        }
    }
}