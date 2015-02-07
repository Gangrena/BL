using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BetterLife.Domain.Abstract;
using BetterLife.Domain.Concrete;
using BetterLife.WebUi.Models.SocialController;
using WebGrease.Css.Extensions;

namespace BetterLife.WebUi.ControllersLogic.SocialController
{
    public class SocialRepository:ISocialRepository
    {
        private readonly IAggregateRepositories _repositories = new AggregateRepositories();
        public string GetDataIdForPerson(int personId)
        {
  
            var isProfileData = _repositories.Photos.GetAll()
                .SingleOrDefault(x => x.PersonProfile.PersonProfileId == personId && x.IsProfile);
            if (isProfileData != null)
            {
                return isProfileData.DataId;
            }
            var anotherRandomData =
                _repositories.Photos.GetAll()
                    .Where(y => y.PersonProfile.PersonProfileId == personId)
                    .OrderBy(c => Guid.NewGuid())
                    .FirstOrDefault();
            return anotherRandomData != null ? anotherRandomData.DataId : "No Image";
        }

        public List<SocialPersonViewModel> GetAll()
        {
            var socialPersonViewModels = new List<SocialPersonViewModel>();
            _repositories.PersonProfiles.GetAll().OrderByDescending(x=>x.PersonProfileId).ForEach(x => socialPersonViewModels.Add(new SocialPersonViewModel
            {
// ReSharper disable once PossibleInvalidOperationException
                Age = DateTime.Today.Year - x.Birthday.Value.Year,
                FirstName = x.FirstName,
                LastName = x.LastName,
                LookingFor = x.LookingFor,
                PersonProfileId = x.PersonProfileId,
                Gender = x.Gender,
                RelationshipStatus = x.RelationshipStatus,
                DataId = GetDataIdForPerson(x.PersonProfileId)                      
            }));
            return socialPersonViewModels;
        }

        public List<SocialPersonViewModel> GetAllByName(string search)
        {
            var socialPersonViewModels = new List<SocialPersonViewModel>();
            _repositories.PersonProfiles.GetAll().Where(s=>s.FirstName.Contains(search) || s.LastName.Contains(search)).ForEach(x => socialPersonViewModels.Add(new SocialPersonViewModel
            {
                // ReSharper disable once PossibleInvalidOperationException
                Age = DateTime.Today.Year - x.Birthday.Value.Year,
                FirstName = x.FirstName,
                LastName = x.LastName,
                LookingFor = x.LookingFor,
                PersonProfileId = x.PersonProfileId,
                Gender = x.Gender,
                RelationshipStatus = x.RelationshipStatus,
                DataId = GetDataIdForPerson(x.PersonProfileId)
            }));
            return socialPersonViewModels;
        }

      
    }
}