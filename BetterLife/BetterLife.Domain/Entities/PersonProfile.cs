using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BetterLife.Domain.FixedForEntities;

namespace BetterLife.Domain.Entities
{
    public sealed class PersonProfile
    {
        public PersonProfile()
        {
            Location = new List<Location>();
            FavoriteBooks = new List<Book>();
            FavoriteMovies = new List<Movie>();
            Photos = new List<Photo>();
            PersonProfileMessages = new List<PersonProfileMessage>();
        }
        //no relations
        public int PersonProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Birthday is required")]
        public DateTime? Birthday { get; set; }
        //enums
        public Gender Gender { get; set; }
        public LookingFor LookingFor { get; set; }
        public RelationshipStatus RelationshipStatus { get; set; }
        public MembershipType MembershipType { get; set; }
        public Status Status { get; set; }
        //one to many relations
        public ICollection<Location> Location { get; set; }
        public ICollection<Book> FavoriteBooks { get; set; }
        public ICollection<Movie> FavoriteMovies { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<PersonProfileMessage> PersonProfileMessages { get; set; } 

    }
}
