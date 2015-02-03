using System.Linq;
using BetterLife.Domain.Entities;

namespace BetterLife.WebUi.ControllersLogic.BookController
{
    public interface IBookRepository
    {
        GlobalBook Add(GlobalBook item, string login);
        bool Delete(int id, string login);
        GlobalBook Get(int id);
        IQueryable<GlobalBook> GetAll();
        bool AddToFavorite(int bookId, string login);

    }
}