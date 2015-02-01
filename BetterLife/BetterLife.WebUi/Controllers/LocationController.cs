using System.Linq;
using System.Web.Mvc;
using BetterLife.WebUi.ControllersLogic.LocationController;
using BetterLife.Domain.Entities;

namespace BetterLife.WebUi.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationRepository _repository = new LocationRepository();
        [Authorize]
        public ActionResult GetAllLocations()
        {
            IQueryable<Location> allLocations = _repository.GetAll();
            return View("GetAllLocations", allLocations.Where(x => x.PersonProfile.Login == User.Identity.Name).OrderByDescending(
                x => new
                {
                    x.IsCurrentLocation,
                    x.IsHomeTown
                }));
        }

        [Authorize]
        public ActionResult UpdateLocation(int locationId)
        {
            IQueryable<Location> allLocations = _repository.GetAll();
            return View("UpdateLocation", allLocations.SingleOrDefault(x => x.LocationId == locationId));
        }
        [HttpPost]
        [Authorize]
        public ActionResult UpdateLocation(Location item)
        {
            if (ModelState.IsValid)
            {
                bool updateValid = _repository.Update(item, User.Identity.Name);
                if (updateValid)
                {
                    ViewBag.Message = "Update successful";
                    return RedirectToAction("GetAllLocations");
                }
                ViewBag.Message = "Update failed";
                return View(item);
            }
            ViewBag.Message = "Update failed";
            return View(item);

        }

        [Authorize]
        public ViewResult AddLocation()
        {
            return View(new Location());
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddLocation(Location item)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(item, User.Identity.Name);
                ViewBag.Message = "Upload successful";
                return RedirectToAction("GetAllLocations");
            }
            ViewBag.Message = "Upload failed";
            return View(item);

        }

        [Authorize]
        public ActionResult DeleteLocation(int locationId)
        {
            bool isdeleted = _repository.Delete(locationId);
            if (isdeleted)
            {
                ViewBag.Message = "Delete successful";
                return RedirectToAction("GetAllLocations");
            }
            ViewBag.Message = "Delete failed";
            return View("UpdateLocation");
        }

    }
}
