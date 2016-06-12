using System;
using FluentNHibernate.Mapping;

namespace GroupBuyServer.Models
{
    public class User
    {
        public virtual Guid Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual float SellerRate { get; set; }
        public virtual float BuyerRate { get; set; }
        public virtual string Image { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime RegisterDate { get; set; }
    }

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
            Map(x => x.Email, "email");
            Map(x => x.RegisterDate, "register_date");
            Map(x => x.Image, "image").CustomType("StringClob").CustomSqlType("nvarchar(max)");
        }
    }
}