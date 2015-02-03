using System;
using BetterLife.Domain.Abstract;
using BetterLife.Domain.Entities;

namespace BetterLife.Domain.Concrete
{
    public class AggregateRepositories : IAggregateRepositories, IDisposable
    {
        private EfDbContext DbContext { get; set; }

        public AggregateRepositories()
        {
            CreateDbContext();
        }
        protected void CreateDbContext()
        {
            DbContext = new EfDbContext();
            // Do NOT enable proxied entities, else serialization fails.
            //if false it will not get the associated certification and skills when we
            //get the applicants
            DbContext.Configuration.ProxyCreationEnabled = false;

            // Load navigation properties explicitly (avoid serialization trouble)
            DbContext.Configuration.LazyLoadingEnabled = false;

            // Because Web API will perform validation, we don't need/want EF to do so
            DbContext.Configuration.ValidateOnSaveEnabled = false;

            //DbContext.Configuration.AutoDetectChangesEnabled = false;
            // We won't use this performance tweak because we don't need 
            // the extra performance and, when autodetect is false,
            // we'd have to be careful. We're not being that careful.
        }
        public void Commit()
        {
            DbContext.SaveChanges();
        }
        private IRepository<Book> _books;
        private IRepository<Comment> _comments;
        private IRepository<Location> _locations;
        private IRepository<Message> _messages;
        private IRepository<Movie> _movies;
        private IRepository<PersonProfile> _personProfiles;
        private IRepository<PersonProfileMessage> _personProfileMessages;
        private IRepository<Photo> _photos;
        private IRepository<GlobalBookLike> _globalBookLikes;
        private IRepository<GlobalBook> _globalBooks;

        public IRepository<GlobalBook> GlobalBooks
        {
            get { return _globalBooks ?? (_globalBooks = new Repository<GlobalBook>(DbContext)); }
        }  

        public IRepository<GlobalBookLike> GlobalBookLikes
        {
            get { return _globalBookLikes ?? (_globalBookLikes = new Repository<GlobalBookLike>(DbContext)); }
        }
        public IRepository<Book> Books
        {
            get { return _books ?? (_books = new Repository<Book>(DbContext)); }
        }
        public IRepository<Comment> Comments
        {
            get
            {
                return _comments ?? (_comments = new Repository<Comment>(DbContext));
            }
        }
        public IRepository<Location> Locations
        {
            get
            {
                return _locations ?? (_locations = new Repository<Location>(DbContext));
            }
        }
        public IRepository<Message> Messages
        {
            get
            {
                return _messages ?? (_messages = new Repository<Message>(DbContext));
            }
        }
        public IRepository<Movie> Movies { get { return _movies ?? (_movies = new Repository<Movie>(DbContext)); } }
        public IRepository<PersonProfile> PersonProfiles
        {
            get
            {
                return _personProfiles ?? (_personProfiles = new Repository<PersonProfile>(DbContext));
            }
        }
        public IRepository<PersonProfileMessage> PersonProfileMessages
        {
            get
            {
                return _personProfileMessages ?? (_personProfileMessages = new Repository<PersonProfileMessage>(DbContext));
            }
        }
        public IRepository<Photo> Photos { get { return _photos ?? (_photos = new Repository<Photo>(DbContext)); } }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                }
            }
        }
    }
}
