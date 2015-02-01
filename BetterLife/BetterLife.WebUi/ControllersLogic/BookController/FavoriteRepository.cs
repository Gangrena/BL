using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BetterLife.Domain.Concrete;
using BetterLife.Domain.Entities;
using DotNetOpenAuth.AspNet.Clients;

namespace BetterLife.WebUi.ControllersLogic.BookController
{
    public class FavoriteRepository : IFavoriteRepository
    {
        //bardzo zle napisane ale nie chce mi się już 
        private readonly AggregateRepositories _repositories = new AggregateRepositories();
        public bool AddToFavorite(int bookId, string login)
        {
            if (bookId == 0 | login == "")
            {
                return false;
            }
            var profiles = _repositories.PersonProfiles.GetAll();
            PersonProfile person = profiles.SingleOrDefault(x => x.Login == login);
            if (person != null)
            {
                Book book = _repositories.Books.GetById(bookId);
                if (book != null)
                {
                    List<Book> bookCheck2 =
                        _repositories.Books.GetAll()
                            .Where(x=> x.PersonId == person.PersonProfileId).ToList();
                    Book bookCheck3 = bookCheck2.SingleOrDefault(x => x.Title == book.Title);
                    if (bookCheck3 == null)
                    {
                        Book newBook = new Book
                        {
                            PersonId = person.PersonProfileId,
                            Title = book.Title,
                            DataId = book.DataId,
                            AuthorLastName = book.AuthorLastName,
                            AuthorFirstName = book.AuthorFirstName,
                            Created = book.Created,
                        };
                        person.FavoriteBooks.Add(newBook);
                        _repositories.PersonProfiles.Update(person);
                        _repositories.Commit();
                        return true;
                    }
                    return true;
                }


            }
            return false;
        }
    }
}