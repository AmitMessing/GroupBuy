using System;
using System.Collections.Generic;
using GroupBuyServer.Models;

namespace GroupBuyServer.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public SellerViewModel Seller { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime EndDate { get; set; }
        public double BasicPrice { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Buyers { get; set; }
        public List<DiscountViewModel> Discounts { get; set; }
    }

    public class SellerViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }

    public class DiscountViewModel
    {
        public int UsersAmount { get; set; }
        public float Present { get; set; }
    }
}