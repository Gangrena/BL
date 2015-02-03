using BetterLife.Domain.Entities;

namespace BetterLife.Domain.Abstract
{
    public interface IAggregateRepositories
    {
        void Commit();
        IRepository<Book> Books { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Location> Locations { get; }
        IRepository<Message> Messages { get; }
        IRepository<Movie> Movies { get; }
        IRepository<PersonProfile> PersonProfiles { get; }
        IRepository<PersonProfileMessage> PersonProfileMessages { get; }
        IRepository<Photo> Photos { get; }
        IRepository<GlobalBookLike> GlobalBookLikes { get; }
        IRepository<GlobalBook> GlobalBooks { get; } 

    }
}