using System;
using System.Linq;
using BetterLife.Domain.Concrete;
using BetterLife.Domain.Entities;

namespace BetterLife.WebUi.Models
{
    public class FirstLogin
    {
        private AggregateRepositories _repo;
        private bool NeedRegister;

        public FirstLogin(string login)
        {
            _repo = new AggregateRepositories();
            NeedRegister = LoginCheck(login);
            
        }
        private bool LoginCheck(string login)
        {
            var dbEntity = _repo.PersonProfiles.GetAll().FirstOrDefault(x => x.Login.ToLower() == login.ToLower());
            return dbEntity == null ? true : false;
        }

        public string PersonIdForSession(string userLogin,string password)
        {
            if (NeedRegister == true)
            {
                _repo.PersonProfiles.Add(new PersonProfile {Login = userLogin, Password = password,Birthday = DateTime.UtcNow});
                _repo.Commit();
            }
            var personProfile =
                _repo.PersonProfiles.GetAll().FirstOrDefault(x => x.Login.ToLower() == userLogin.ToLower());
            return personProfile.PersonProfileId.ToString();
        }
              
        
    }
}