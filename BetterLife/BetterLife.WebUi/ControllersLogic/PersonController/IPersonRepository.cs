using System.Linq;
using BetterLife.Domain.Entities;

namespace BetterLife.WebUi.ControllersLogic.PersonController
{
    public interface IPersonRepository
    {
        PersonProfile Add(PersonProfile item);
        bool Delete(int id);
        PersonProfile Get(int id);
        IQueryable<PersonProfile> GetAll();
        bool Update(PersonProfile item); 
    }
}