using System.Linq;
using BetterLife.Domain.Entities;
namespace BetterLife.WebUi.ControllersLogic.LocationController
{
    public interface ILocationRepository
    {
        Location Add(Location item,string login);
        bool Delete(int id);
        Location Get(int id);
        IQueryable<Location> GetAll();
        bool Update(Location item,string login);  
    }
}