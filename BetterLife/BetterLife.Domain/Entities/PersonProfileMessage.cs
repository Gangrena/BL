using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BetterLife.Domain.Entities
{
    public class PersonProfileMessage
    {
        public PersonProfileMessage()
        {
            Messages = new List<Message>();
        }
 
        public int PersonProfileMessageId { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public int PersonProfileId { get; set; }
   
        public int SecPersonProfileId { get; set; }
        public virtual PersonProfile PersonProfile { get; set; }
        public virtual PersonProfile SecPersonProfile { get; set; }

    }
}
