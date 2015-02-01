using System.Data.Entity;
using BetterLife.Domain.Entities;

namespace BetterLife.Domain.Concrete
{
    public class EfDbContext:DbContext
    {

        public DbSet<Book> Books { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<PersonProfile> PersonProfiles { get; set; }
        public DbSet<PersonProfileMessage> PersonProfileMessages { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
