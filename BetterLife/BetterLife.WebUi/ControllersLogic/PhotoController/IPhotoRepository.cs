using System.Linq;
using BetterLife.Domain.Entities;

namespace BetterLife.WebUi.ControllersLogic.PhotoController
{
    public interface IPhotoRepository
    {
        Photo Add(Photo item, string login);
        bool Delete(int id);
        Photo Get(int id);
        IQueryable<Photo> GetAll();
        bool Update(Photo item,string login);
    }
}