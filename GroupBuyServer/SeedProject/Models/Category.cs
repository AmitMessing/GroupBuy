using FluentNHibernate.Mapping;

namespace SeedProject.Models
{
    public class Category
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int ParentId { get; set; }
    }

    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Table("t_categories");

            Id(x => x.Id, "id").GeneratedBy.Assigned();
            Map(x => x.Name, "name");
            Map(x => x.ParentId, "parent_id");
        }
    }
}