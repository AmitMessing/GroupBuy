using System;
using GroupBuyServer.Models;

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
}