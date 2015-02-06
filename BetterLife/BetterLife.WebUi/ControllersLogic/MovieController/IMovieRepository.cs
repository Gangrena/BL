using System.Linq;
using BetterLife.Domain.Entities;

namespace BetterLife.WebUi.ControllersLogic.MovieController
{
    public interface IMovieRepository
    {
        GlobalMovie Add(GlobalMovie item, string login);
        bool Delete(int id, string login);
        GlobalMovie Get(int id);
        IQueryable<GlobalMovie> GetAll();
        bool AddToFavorite(int bookId, string login);
    }
}