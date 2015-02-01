using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterLife.Domain.Entities
{
    public class Photo
    {
        public Photo()
        {
            Comments = new List<Comment>();
        }
        public int PhotoId { get; set; }
        public string PhotoTitle { get; set; }
        public DateTime Created { get; set; }
        public string DataId { get; set; }
        public bool IsProfile { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual PersonProfile PersonProfile { get; set; }//tutaj moga być problemy

        [NotMapped]
        public bool DeletePhoto { get; set; }
    
    }
}
