using FluentNHibernate.Mapping;

namespace SeedProject.Models
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("t_users");

            Id(x => x.Id, "id");
            Map(x => x.FirstName, "first_name");
            Map(x => x.LastName, "last_name");
            Map(x => x.UserName, "user_name");
            Map(x => x.Password, "password");
            Map(x => x.SellerRate, "seller_rate");
            Map(x => x.BuyerRate, "buyer_rate");
        }
    }
}