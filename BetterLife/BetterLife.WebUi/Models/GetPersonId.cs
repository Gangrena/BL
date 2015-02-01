using System.Linq;
using BetterLife.Domain.Concrete;
using BetterLife.Domain.Entities;

namespace BetterLife.WebUi.Models
{
    public class GetPersonId
    {
        private readonly AggregateRepositories _repo = new AggregateRepositories();

        public int GetId(string login)
        {
            PersonProfile dbEntry =
                _repo.PersonProfiles.GetAll().FirstOrDefault(x => x.Login.ToLower() == login.ToLower());
            if (dbEntry != null) return dbEntry.PersonProfileId;
            return 0;
        }
    }
}