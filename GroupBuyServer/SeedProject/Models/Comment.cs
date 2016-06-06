using System;

namespace SeedProject.Models
{
    public class Comment
    {
        public virtual Guid Id { get; set; }
        public virtual User User { get; set; }
        public virtual string Content { get; set; }
        public virtual DateTime Date { get; set; }
    }
}