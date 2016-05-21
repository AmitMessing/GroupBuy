﻿using System;
using System.Collections.Generic;
using System.Drawing;
using FluentNHibernate.Mapping;

namespace GroupBuyServer.Models
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual User Seller { get; set; }
        public virtual DateTime PublishDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual double BasicPrice { get; set; }
        public virtual Image Image { get; set; }
        public virtual IList<Category> Categories { get; set; }
        public virtual IList<User> Buyers { get; set; }
        public virtual IList<Discount> Discounts { get; set; }        
    }

    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Table("t_products");

            Id(x => x.Id, "id").GeneratedBy.Assigned();
            Map(x => x.Name, "name");
            Map(x => x.Description, "description");
            References(x => x.Seller).Column("seller_id");
            Map(x => x.PublishDate, "publish_date");
            Map(x => x.BasicPrice, "basic_price");
            Map(x => x.EndDate, "end_date");
            Map(x => x.Image, "picture");

            HasManyToMany(x => x.Categories)
                .Table("rel_product_category")
                .ParentKeyColumn("product_id")
                .ChildKeyColumn("category_id");

            HasMany(x => x.Discounts).KeyColumn("product_id").Inverse().Cascade.None();
        }
    }
}