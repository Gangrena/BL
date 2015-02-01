using System;
using System.Linq;
using BetterLife.Domain.Abstract;
using BetterLife.Domain.Concrete;
using BetterLife.Domain.Entities;
using BetterLife.WebUi.ControllersLogic.PersonController;
using WebGrease.Css.Extensions;

namespace BetterLife.WebUi.ControllersLogic.LocationController
{
    public class LocationRepository:ILocationRepository
    {
        private readonly IAggregateRepositories _repo;
        private readonly IPersonRepository _personRepository = new PersonRepository();

        public LocationRepository()
        {
            _repo = new AggregateRepositories();
        }
        public Location Add(Location item,string login)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            var profiles = _personRepository.GetAll();
            PersonProfile dbEntry = profiles.SingleOrDefault(x => x.Login == login);
            if (dbEntry != null)
            {
                if (item.IsHomeTown)
                {
                    var locations = _repo.Locations.GetAll();
                    locations.Where(x => x.IsHomeTown  && x.PersonProfile.PersonProfileId == dbEntry.PersonProfileId)
                        .ForEach(x => x.IsHomeTown = false);
                    _repo.Commit();
                }
                if (item.IsCurrentLocation)
                {
                    var locations = _repo.Locations.GetAll();
                    locations.Where(
                        x => x.IsCurrentLocation  && x.PersonProfile.PersonProfileId == dbEntry.PersonProfileId)
                        .ForEach(x => x.IsCurrentLocation = false);
                    _repo.Commit();
                }
                dbEntry.Location.Add(item);
                _personRepository.Update(dbEntry);

            }
            return item;
        }

        public bool Delete(int id)
        {
            try
            {
                _repo.Locations.Delete(id);
                _repo.Commit();
                return true;
            }
            catch
            {
                return false;
            }
           
        }

        public Location Get(int id)
        {
            return _repo.Locations.GetById(id);
        }

        public IQueryable<Location> GetAll()
        {
            return _repo.Locations.GetAll();
        }

        public bool Update(Location item,string login)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            var profiles = _personRepository.GetAll();
            PersonProfile dbEntry = profiles.SingleOrDefault(x => x.Login == login);
            if (dbEntry != null)
            {
                if (item.IsHomeTown)
                {
                    var locations = _repo.Locations.GetAll();
                    locations.Where(x => x.IsHomeTown  && x.PersonProfile.PersonProfileId == dbEntry.PersonProfileId)
                        .ForEach(x => x.IsHomeTown = false);
                    _repo.Commit();
                }
                if (item.IsCurrentLocation)
                {
                    var locations = _repo.Locations.GetAll();
                    locations.Where(
                        x => x.IsCurrentLocation  && x.PersonProfile.PersonProfileId == dbEntry.PersonProfileId)
                        .ForEach(x => x.IsCurrentLocation = false);
                    _repo.Commit();
                }
                var dbUpdateLocation = _repo.Locations.GetById(item.LocationId);
                dbUpdateLocation.City = item.City;
                dbUpdateLocation.Country = item.Country;
                dbUpdateLocation.HouseNumber = item.HouseNumber;
                dbUpdateLocation.IsCurrentLocation = item.IsCurrentLocation;
                dbUpdateLocation.IsHomeTown = item.IsHomeTown;
                dbUpdateLocation.Street = item.Street;
                dbUpdateLocation.ZipCode = item.ZipCode;
                _repo.Commit();
                return true;
            }
            return false;
        }
    }
}