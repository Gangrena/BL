using System;
using System.Collections.Generic;

namespace BetterLife.Domain.Entities
{
    public class PhotoAlbum
    {
        public PhotoAlbum()
        {
            Photos = new List<Photo>();
        }
        public int PhotoAlbumId { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual PersonProfile PersonProfile { get; set; }
    }
}
