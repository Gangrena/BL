using System;

namespace BetterLife.Domain.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public PersonProfileMessage PersonProfileMessage { get; set; }
       
    }
}
