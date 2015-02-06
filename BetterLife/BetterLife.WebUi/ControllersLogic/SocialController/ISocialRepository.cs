using System.Collections.Generic;
using System.Linq;
using BetterLife.WebUi.Models.SocialController;

namespace BetterLife.WebUi.ControllersLogic.SocialController
{
    public interface ISocialRepository
    {
        List<SocialPersonViewModel> GetAll();
        string GetDataIdForPerson(int personId);
    }
}