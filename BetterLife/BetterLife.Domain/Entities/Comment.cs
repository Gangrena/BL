using System;

namespace BetterLife.Domain.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
