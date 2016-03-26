using FluentNHibernate.Mapping;

namespace GroupBuyServer.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LasstName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
    }

    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("t_users");

            Id(x => x.Id, "id");
            Map(x => x.FirstName, "first_name");
            Map(x => x.LasstName, "last_name");
            Map(x => x.Email, "email");
            Map(x => x.Password, "password");
        }
    }
}