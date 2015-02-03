using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterLife.Domain.Entities
{
    public class GlobalBook
    {
        public GlobalBook()
        {
            GlobalBookLikes = new HashSet<GlobalBookLike>();
        }
        public GlobalBook(GlobalBook item)
        {
            Title = item.Title;
            AuthorFirstName = item.AuthorFirstName;
            AuthorLastName = item.AuthorLastName;
            DataId = item.DataId;
            Created = item.Created;
            GlobalBookLikes = new HashSet<GlobalBookLike>();
        }
        public int GlobalBookId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string AuthorFirstName { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string AuthorLastName { get; set; }
        public string DataId { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<GlobalBookLike> GlobalBookLikes { get; set; } 
        
    }
}
