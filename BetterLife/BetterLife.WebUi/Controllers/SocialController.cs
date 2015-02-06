using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetterLife.Domain.Concrete;
using BetterLife.Domain.Entities;
using BetterLife.WebUi.ControllersLogic.SocialController;

namespace BetterLife.WebUi.Controllers
{
    public class SocialController : Controller
    {
        private ISocialRepository _repository = new SocialRepository();

        public ActionResult Index()
        {

            return View(_repository.GetAll());
        }
    }
}
