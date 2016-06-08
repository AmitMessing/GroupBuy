﻿using System;
using System.Collections.Generic;

namespace GroupBuyServer.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public SellerViewModel Seller { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime EndDate { get; set; }
        public double BasicPrice { get; set; }
        public List<string> Categories { get; set; }
        public List<BuyerViewModel> Buyers { get; set; }
        public List<DiscountViewModel> Discounts { get; set; }
    }

    public class BuyerViewModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
    }

    public class SellerViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public float Rating { get; set; }
    }

    public class DiscountViewModel
    {
        public int UsersAmount { get; set; }
        public float Precent { get; set; }
    }
}