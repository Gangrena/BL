using System;
using System.Linq;
using BetterLife.Domain.Abstract;
using BetterLife.Domain.Concrete;
using BetterLife.Domain.Entities;
using BetterLife.WebUi.ControllersLogic.PersonController;
using WebGrease.Css.Extensions;


namespace BetterLife.WebUi.ControllersLogic.PhotoController
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly IAggregateRepositories _repo;
        private readonly IPersonRepository _personRepository = new PersonRepository();
        public PhotoRepository()
        {
            _repo = new AggregateRepositories();
        }
        public Photo Add(Photo item, string login)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            var profiles = _personRepository.GetAll();
            PersonProfile dbEntry = profiles.SingleOrDefault(x => x.Login == login);
            if (dbEntry != null)
            {
                if (item.IsProfile)
                {
                    var photos = _repo.Photos.GetAll();
                    photos.Where(x => x.IsProfile  && x.PersonProfile.PersonProfileId == dbEntry.PersonProfileId)
                        .ForEach(x => x.IsProfile = false);
                    foreach (var photo in photos)
                    {
                        _repo.Photos.Update(photo);
                    }
                    _repo.Commit();
                }
                dbEntry.Photos.Add(item);
                _personRepository.Update(dbEntry);
            }
            return item;
        }

        public bool Delete(int id)
        {
            try
            {
                _repo.Photos.Delete(id);
                _repo.Commit();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public Photo Get(int id)
        {
            return _repo.Photos.GetById(id);
        }

        public IQueryable<Photo> GetAll()
        {
            return _repo.Photos.GetAll();
        }

        public bool Update(Photo item, string login)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            var profiles = _personRepository.GetAll();
            PersonProfile dbEntry = profiles.SingleOrDefault(x => x.Login == login);
            if (dbEntry != null)
            {
                if (item.IsProfile)
                {
                    var photos = _repo.Photos.GetAll();
                    photos.Where(x => x.IsProfile && x.PersonProfile.PersonProfileId == dbEntry.PersonProfileId)
                        .ForEach(x => x.IsProfile = false);
                    foreach (var photo in photos)
                    {
                        _repo.Photos.Update(photo);
                    }
                    _repo.Commit();
                }
                //walczylem z tym ze 2 godziny , az mi sie przypomnialo ze jakis prof powiedzial ze wszystkie obiekty ef sa sledzone i wystarczylo tylko tyle zrobic lol
                var dbUpdatePhoto = _repo.Photos.GetById(item.PhotoId);
                dbUpdatePhoto.IsProfile = item.IsProfile;
                dbUpdatePhoto.PhotoTitle = item.PhotoTitle;
                _repo.Commit();
                return true;


            }
            return false;
        }
    }
}