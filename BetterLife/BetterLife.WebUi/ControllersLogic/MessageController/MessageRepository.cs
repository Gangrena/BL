using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using BetterLife.Domain.Abstract;
using BetterLife.Domain.Concrete;
using BetterLife.Domain.Entities;
using BetterLife.WebUi.Models.SocialController;
using DotNetOpenAuth.AspNet.Clients;
using WebGrease.Css.Extensions;

namespace BetterLife.WebUi.ControllersLogic.MessageController
{
    public class MessageRepository
    {
        private readonly IAggregateRepositories _repo;

        public MessageRepository()
        {
            _repo = new AggregateRepositories();
        }

        public bool SendMessage(Message message, string login)
        {
            message.Created = DateTime.UtcNow;
            PersonProfile sender = _repo.PersonProfiles.GetAll().SingleOrDefault(x => x.Login == login);
            if (sender == null)
                return false;
            PersonProfile receiver = _repo.PersonProfiles.GetById(message.SecPersonProfileId);
            if (receiver == null)
                return false;
            //sprawdzam czy mial juz jakis kontakt z denatem
            PersonProfileMessage messages =
                _repo.PersonProfileMessages.GetAll()
                    .SingleOrDefault(
                        x =>
                            x.PersonProfile.PersonProfileId == sender.PersonProfileId && x.SecPersonProfile.PersonProfileId == receiver.PersonProfileId ||
                            x.PersonProfile.PersonProfileId == receiver.PersonProfileId && x.SecPersonProfile.PersonProfileId == sender.PersonProfileId);
            if (messages == null) // ta dluzsza opcja
            {
                var newContact = new PersonProfileMessage
                {
                    Messages = new Collection<Message>(),
                    PersonProfile = sender,
                    SecPersonProfile = receiver,
                    PersonProfileId = sender.PersonProfileId,
                    SecPersonProfileId = receiver.PersonProfileId
                };
                newContact.Messages.Add(message);
                _repo.Messages.Add(message);
                _repo.PersonProfileMessages.Add(newContact);
                _repo.Commit();
                return true;
            }
            _repo.Messages.Add(message);
            messages.Messages.Add(message);
            _repo.Commit();
            return true;
        }
        public List<SocialPersonViewModel> GetAll(string login)
        {
            //bardzo szybkie rozwiazanie
            
            var socialPersonViewModels = new List<SocialPersonViewModel>();
            PersonProfile sender = _repo.PersonProfiles.GetAll().SingleOrDefault(x => x.Login == login);
            if (sender == null)
                return null;
            //jezus maria ale piekne 
            IQueryable<PersonProfileMessage> personProfileMessages =
                _repo.PersonProfileMessages.GetAll()
                    .Where(
                        x =>
                            x.PersonProfile.PersonProfileId == sender.PersonProfileId ||
                            x.SecPersonProfile.PersonProfileId == sender.PersonProfileId);
   
            personProfileMessages.OrderBy(x => x.Messages.Max(y => y.Created)).ForEach(x => socialPersonViewModels.Add(new SocialPersonViewModel
            {
                PersonProfileId = x.SecPersonProfileId == sender.PersonProfileId ? x.PersonProfileId : x.SecPersonProfileId,
             
            }));
            socialPersonViewModels.ForEach(x => x.DataId = GetDataIdForPerson(x.PersonProfileId));
            return socialPersonViewModels;
        }
        //zle ale na szybko
        public string GetDataIdForPerson(int personId)
        {

            var isProfileData = _repo.Photos.GetAll()
                .SingleOrDefault(x => x.PersonProfile.PersonProfileId == personId && x.IsProfile);
            if (isProfileData != null)
            {
                return isProfileData.DataId;
            }
            var anotherRandomData =
                _repo.Photos.GetAll()
                    .Where(y => y.PersonProfile.PersonProfileId == personId)
                    .OrderBy(c => Guid.NewGuid())
                    .FirstOrDefault();
            return anotherRandomData != null ? anotherRandomData.DataId : "No Image";
        }
    }
}