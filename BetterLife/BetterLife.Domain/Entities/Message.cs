using System;
using System.ComponentModel.DataAnnotations;

namespace BetterLife.Domain.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Field is Empty !")]
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public int SecPersonProfileId { get; set; }
        public PersonProfileMessage PersonProfileMessage { get; set; }

    }
}
