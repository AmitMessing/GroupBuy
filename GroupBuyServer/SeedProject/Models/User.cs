using System;
using System.Collections.Generic;

namespace SeedProject.Models
{
    public class User
    {
        public virtual Guid Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual float SellerRate { get; set; }
        public IList<Comment> SellerRateComments { get; set; }
        public IList<Comment> BuyerRateComments { get; set; }
        public virtual float BuyerRate { get; set; }
    }
}