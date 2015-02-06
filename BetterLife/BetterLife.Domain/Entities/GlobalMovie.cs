using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterLife.Domain.Entities
{
    public class GlobalMovie
    {
        public GlobalMovie()
        {
            
        }

        public GlobalMovie(GlobalMovie item)
        {
            Title = item.Title;
            DataId = item.DataId;
            Created = item.Created;
            GlobalMovieLikes = new HashSet<GlobalMovieLike>();
        }
        public int GlobalMovieId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string DataId { get; set; }

        public DateTime Created { get; set; }
        public int PersonProfileId { get; set; }
        public virtual ICollection<GlobalMovieLike> GlobalMovieLikes { get; set; } 
   
    }
}
