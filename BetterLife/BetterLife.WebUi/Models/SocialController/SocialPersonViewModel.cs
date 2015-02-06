using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BetterLife.Domain.Abstract;
using BetterLife.Domain.FixedForEntities;

namespace BetterLife.WebUi.Models.SocialController
{
    public class SocialPersonViewModel
    {

        public int PersonProfileId { get; set; }
        public string DataId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Age1 { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public LookingFor LookingFor { get; set; }
        public RelationshipStatus RelationshipStatus { get; set; }
    }
}