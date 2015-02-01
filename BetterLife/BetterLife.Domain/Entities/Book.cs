
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BetterLife.Domain.Entities
{
    public class Book
    {
        public Book()
        {
            this.PersonProfile = new HashSet<PersonProfile>();
        }
        public int BookId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string AuthorFirstName { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string AuthorLastName { get; set; }
        public string DataId { get; set; }
        public DateTime Created { get; set; }
        public int PersonId { get; set; }
        public virtual ICollection<PersonProfile> PersonProfile { get; set; }
    }
}
