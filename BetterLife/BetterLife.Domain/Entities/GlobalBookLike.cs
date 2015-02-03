using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterLife.Domain.Entities
{
    public class GlobalBookLike
    {
        public GlobalBookLike()
        {
            
        }

        public GlobalBookLike(GlobalBook item,PersonProfile profile)
        {
            GlobalBook = item;
            PersonProfile = profile;
        }

        public int GlobalBookLikeId { get; set; }
        public virtual GlobalBook GlobalBook { get; set; }
        public virtual PersonProfile PersonProfile { get; set; }
    }
}
