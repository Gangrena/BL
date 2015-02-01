using System.Collections.Generic;


namespace BetterLife.Domain.Entities
{
    public class PersonProfileMessage
    {
        public PersonProfileMessage()
        {
            Messages = new List<Message>();
        }
        public int PersonProfileMessageId { get; set; }
        public int SecPersonProfile { get; set; }
        public virtual ICollection<Message> Messages { get; set; } 
        public virtual PersonProfile PersonProfile { get; set; }
    }
}
