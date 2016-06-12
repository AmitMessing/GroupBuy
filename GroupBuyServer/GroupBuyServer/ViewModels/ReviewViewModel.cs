using System;
using System.Collections.Generic;
using GroupBuyServer.Models;
using NHibernate.Mapping;

namespace GroupBuyServer.ViewModels
{
    public class ReviewViewModel
    {
        public Guid Id { get; set; }
        public bool IsOnSeller { get; set; }
        public string Reviewer { get; set; }
        public Guid OnUserId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime PublishDate { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }

        public ReviewViewModel(Review review, string reviewerName)
        {
            Id = review.Id;
            IsOnSeller = review.IsOnSeller;
            Reviewer = reviewerName;
            OnUserId = review.OnUserId;
            ProductId = review.ProductId;
            PublishDate = review.PublishDate;
            Content = review.Content;
            Rating = review.Rating;
        }
    }

    public class UserResviewsViewModel
    {
        public Guid UserId { get; set; }
        public List<ReviewViewModel> SellerReviews { get; set; }
        public List<ReviewViewModel> BuyerReviews { get; set; }
    }
}