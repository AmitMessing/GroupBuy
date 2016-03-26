using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace GroupBuyServer.Models
{
    public class Category
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }

    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Table("t_categories");

            Id(x => x.Id, "id");
            Map(x => x.Name, "name");
            Map(x => x.Description, "description");
        }
    }
}