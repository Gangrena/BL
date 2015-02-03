using System;
using System.Linq;
using BetterLife.Domain.Abstract;
using BetterLife.Domain.Concrete;
using BetterLife.Domain.Entities;
using BetterLife.WebUi.ControllersLogic.PersonController;

namespace BetterLife.WebUi.ControllersLogic.BookController
{
    public class BookRepository:IBookRepository
    {
        private readonly IAggregateRepositories _repo;
        public BookRepository()
        {
            _repo = new AggregateRepositories();
        }
        public GlobalBook Add(GlobalBook item, string login)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            var dbEntry = _repo.PersonProfiles.GetAll().SingleOrDefault(x => x.Login == login);
            if (dbEntry != null)
            {
                GlobalBook antiDoubleGlobalBook = _repo.GlobalBooks.GetAll().SingleOrDefault(x => x.Title == item.Title);
                if (antiDoubleGlobalBook == null)
                {
                    var newDbEntry = new GlobalBookLike(item, dbEntry);
                    dbEntry.GlobalBookLikes.Add(newDbEntry);
                    _repo.PersonProfiles.Update(dbEntry);
                    _repo.Commit();
                    return new GlobalBook();

                }
                return null;
            }
            return item;
        }

        public bool Delete(int globalBookId, string login)
        {
            try
            {
                var personProfile = _repo.PersonProfiles.GetAll().SingleOrDefault(x => x.Login == login);
                if (personProfile == null) return false;
                var likeForDelete =
                    _repo.GlobalBookLikes.GetAll()
                        .SingleOrDefault(
                            x => x.GlobalBook.GlobalBookId == globalBookId && x.PersonProfile.Login == login);
                    
                if (likeForDelete == null) return false;
                personProfile.GlobalBookLikes.Remove(likeForDelete);
                _repo.GlobalBookLikes.Delete(likeForDelete);
                _repo.PersonProfiles.Update(personProfile);
                _repo.Commit();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool AddToFavorite(int bookId, string login)
        {

            var globalBookLikes = _repo.GlobalBookLikes.GetAll()
                  .SingleOrDefault(
                      x => x.PersonProfile.Login == login && x.GlobalBook.GlobalBookId == bookId);
            if (globalBookLikes != null) return false;
            var personProfile = _repo.PersonProfiles.GetAll().SingleOrDefault(x => x.Login == login);
            if (personProfile == null) return false;
            var newDbEntry = _repo.GlobalBooks.GetById(bookId);
            if (newDbEntry == null) return false;
            var newGlobalBookLike = new GlobalBookLike(newDbEntry, personProfile);
            _repo.GlobalBookLikes.Add(newGlobalBookLike);
            _repo.Commit();
            return true;
        }

        public GlobalBook Get(int id)
        {
            return _repo.GlobalBooks.GetById(id);
        }

        public IQueryable<GlobalBook> GetAll()
        {
            return _repo.GlobalBooks.GetAll();
        }

    }
}