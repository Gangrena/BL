using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BetterLife.Domain.FixedForEntities;

namespace BetterLife.Domain.Entities
{
    public  class PersonProfile
    {
        public PersonProfile()
        {
            Location = new List<Location>();
            GlobalMovieLikes = new HashSet<GlobalMovieLike>();
            Photos = new List<Photo>();
            PersonProfileMessages = new List<PersonProfileMessage>();
            GlobalBookLikes = new HashSet<GlobalBookLike>();
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
        public ICollection<Photo> Photos { get; set; }
        public ICollection<GlobalBookLike> GlobalBookLikes { get; set; }
        public ICollection<GlobalMovieLike> GlobalMovieLikes { get; set; } 
        public ICollection<PersonProfileMessage> PersonProfileMessages { get; set; } 

    }
}
