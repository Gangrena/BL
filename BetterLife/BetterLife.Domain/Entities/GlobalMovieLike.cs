using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterLife.Domain.Entities
{
    public class GlobalMovieLike
    {
        public GlobalMovieLike()
        {
            
        }

        public GlobalMovieLike(GlobalMovie item,PersonProfile profile)
        {
            GlobalMovie = item;
            PersonProfile = profile;
        }

        public int GlobalMovieLikeId { get; set; }
        public int PersonProfileId { get; set; }
        public virtual GlobalMovie GlobalMovie { get; set; }
        public virtual PersonProfile PersonProfile { get; set; }
    }
}
