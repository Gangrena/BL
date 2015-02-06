using System;
using System.Linq;
using BetterLife.Domain.Abstract;
using BetterLife.Domain.Concrete;
using BetterLife.Domain.Entities;

namespace BetterLife.WebUi.ControllersLogic.MovieController
{
    public class MovieRepository:IMovieRepository
    {
         private readonly IAggregateRepositories _repo;
        public MovieRepository()
        {
            _repo = new AggregateRepositories();
        }
        public GlobalMovie Add(GlobalMovie item, string login)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            var dbEntry = _repo.PersonProfiles.GetAll().SingleOrDefault(x => x.Login == login);
            if (dbEntry != null)
            {
                GlobalMovie antiDoubleGlobalMovie = _repo.GlobalMovies.GetAll().SingleOrDefault(x => x.Title == item.Title);
                if (antiDoubleGlobalMovie == null)
                {
                    var newDbEntry = new GlobalMovieLike(item, dbEntry);
                    dbEntry.GlobalMovieLikes.Add(newDbEntry);
                    _repo.PersonProfiles.Update(dbEntry);
                    _repo.Commit();
                    return new GlobalMovie();

                }
                return null;
            }
            return item;
        }

        public bool Delete(int globalMovieId, string login)
        {
            try
            {
                var personProfile = _repo.PersonProfiles.GetAll().SingleOrDefault(x => x.Login == login);
                if (personProfile == null) return false;
                var likeForDelete =
                    _repo.GlobalMovieLikes.GetAll()
                        .SingleOrDefault(
                            x => x.GlobalMovie.GlobalMovieId == globalMovieId && x.PersonProfile.Login == login);
                    
                if (likeForDelete == null) return false;
                personProfile.GlobalMovieLikes.Remove(likeForDelete);
                _repo.GlobalMovieLikes.Delete(likeForDelete);
                _repo.PersonProfiles.Update(personProfile);
                _repo.Commit();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool AddToFavorite(int bookId, string login)
        {

            var globalMovieLikes = _repo.GlobalMovieLikes.GetAll()
                  .SingleOrDefault(
                      x => x.PersonProfile.Login == login && x.GlobalMovie.GlobalMovieId == bookId);
            if (globalMovieLikes != null) return false;
            var personProfile = _repo.PersonProfiles.GetAll().SingleOrDefault(x => x.Login == login);
            if (personProfile == null) return false;
            var newDbEntry = _repo.GlobalMovies.GetById(bookId);
            if (newDbEntry == null) return false;
            var newGlobalMovieLike = new GlobalMovieLike(newDbEntry, personProfile);
            _repo.GlobalMovieLikes.Add(newGlobalMovieLike);
            _repo.Commit();
            return true;
        }

        public GlobalMovie Get(int id)
        {
            return _repo.GlobalMovies.GetById(id);
        }

        public IQueryable<GlobalMovie> GetAll()
        {
            return _repo.GlobalMovies.GetAll();
        }
    }
}