using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BetterLife.Domain.Entities;
using BetterLife.WebUi.ControllersLogic.PersonController;


namespace BetterLife.WebUi.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _repository = new PersonRepository();

        [Authorize]
        public ActionResult Person()
        {
            IEnumerable<PersonProfile> allPerson = _repository.GetAll();
            return View("Person", allPerson.SingleOrDefault(x => x.Login == User.Identity.Name));
        }
        public ActionResult OtherPerson(int personProfileId)
        {
            IEnumerable<PersonProfile> allPerson = _repository.GetAll();
            return View("OtherPerson", allPerson.SingleOrDefault(x => x.PersonProfileId == personProfileId));
        }
        [HttpPost]
        public ActionResult Person(PersonProfile item)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(item);
                ViewBag.Message = "Update successful";
                return View(item);
            }
            ViewBag.Message = "Update failed";
            return View(item);
        }
    }
}
