
namespace BetterLife.Domain.Entities
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string DataId { get; set; }
        public int PersonId { get; set; }
        public virtual PersonProfile PersonProfile { get; set; }
    }
}
