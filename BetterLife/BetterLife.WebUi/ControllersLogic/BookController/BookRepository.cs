using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BetterLife.Domain.Abstract;
using BetterLife.Domain.Concrete;
using BetterLife.Domain.Entities;
using BetterLife.WebUi.ControllersLogic.PersonController;
using WebGrease.Css.Extensions;

namespace BetterLife.WebUi.ControllersLogic.BookController
{
    public class BookRepository:IBookRepository
    {
        private readonly IAggregateRepositories _repo;
        private readonly IPersonRepository _personRepository = new PersonRepository();

        public BookRepository()
        {
            _repo = new AggregateRepositories();
        }
        public Book Add(Book item, string login)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            var profiles = _personRepository.GetAll();
            PersonProfile dbEntry = profiles.SingleOrDefault(x => x.Login == login);
            if (dbEntry != null)
            {
                item.PersonId = dbEntry.PersonProfileId;
                dbEntry.FavoriteBooks.Add(item);
                _personRepository.Update(dbEntry);
            }
            return item;
        }

        public bool Delete(int id)
        {
            try
            {
                _repo.Books.Delete(id);
                _repo.Commit();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Book Get(int id)
        {
            return _repo.Books.GetById(id);
        }

        public IQueryable<Book> GetAll()
        {
            return _repo.Books.GetAll();
        }

        public bool Update(Book item, string login)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            var profiles = _personRepository.GetAll();
            PersonProfile dbEntry = profiles.SingleOrDefault(x => x.Login == login);
            if (dbEntry != null)
            {
                var dbUpdateBook = _repo.Books.GetById(item.BookId);
                dbUpdateBook.AuthorFirstName = item.AuthorFirstName;
                dbUpdateBook.AuthorLastName = item.AuthorLastName;
                dbUpdateBook.Title = item.Title;
                _repo.Commit();
                return true;
            }
            return false;
        }
    }
}