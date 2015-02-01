using System;
using System.Linq;
using BetterLife.Domain.Abstract;
using BetterLife.Domain.Concrete;
using BetterLife.Domain.Entities;

namespace BetterLife.WebUi.ControllersLogic.PersonController
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IAggregateRepositories _repo;

        public PersonRepository()
        {
            _repo = new AggregateRepositories();
        }
        public PersonProfile Add(PersonProfile item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            _repo.PersonProfiles.Add(item);
            _repo.Commit();
            return item;
        }

        public bool Delete(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException();
            }
            PersonProfile dbEntry = _repo.PersonProfiles.GetById(id);
            if (dbEntry == null)
            {
                return false;
            }
            _repo.PersonProfiles.Delete(id);
            _repo.Commit();
            return true;
        }

        public PersonProfile Get(int id)
        {
            return _repo.PersonProfiles.GetById(id);

        }

        public IQueryable<PersonProfile> GetAll()
        {
            return _repo.PersonProfiles.GetAll();
        }

        public bool Update(PersonProfile item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            _repo.PersonProfiles.Update(item);
            _repo.Commit();
            return true;
        }
    }
}