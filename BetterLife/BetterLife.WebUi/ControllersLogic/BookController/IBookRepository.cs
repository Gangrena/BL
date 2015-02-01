using System.Linq;
using BetterLife.Domain.Entities;

namespace BetterLife.WebUi.ControllersLogic.BookController
{
    public interface IBookRepository
    {
        Book Add(Book item, string login);
        bool Delete(int id);
        Book Get(int id);
        IQueryable<Book> GetAll();
        bool Update(Book item, string login); 
    }
}