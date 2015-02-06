using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetterLife.Domain.Entities;
using BetterLife.WebUi.ControllersLogic.PhotoController;

namespace BetterLife.WebUi.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoRepository _repository = new PhotoRepository();

        [Authorize]
        public ActionResult GetAllPhotos()
        {
            //i juz nie problem gdy uzywam iqueryable to dziala ale nie mozna edytować photo natomiast przy ienumerable nie mam dostepu do personprofile.login ... czyli musze miec id... 
            IQueryable<Photo> allPhotos = _repository.GetAll().OrderByDescending(x => x.Created);
            return View("GetAllPhotos", allPhotos.Where(x => x.PersonProfile.Login == User.Identity.Name));
        }
        public ActionResult GetAllPersonPhotos(int personProfileId)
        {
            ViewBag.personProfileId = personProfileId;
            //i juz nie problem gdy uzywam iqueryable to dziala ale nie mozna edytować photo natomiast przy ienumerable nie mam dostepu do personprofile.login ... czyli musze miec id... 
            IQueryable<Photo> allPhotos = _repository.GetAll().OrderByDescending(x => x.Created);
            return View("GetAllPersonPhotos", allPhotos.Where(x => x.PersonProfile.PersonProfileId == personProfileId));
        }
        [Authorize]
        public ActionResult UpdatePhoto(int photoId)
        {
            IQueryable<Photo> allPhotos = _repository.GetAll();
            return View("UpdatePhoto", allPhotos.SingleOrDefault(x=>x.PhotoId==photoId));
        }
        [HttpPost]
        [Authorize]
        public ActionResult UpdatePhoto(Photo item)
        {
            if (ModelState.IsValid)
            {
                bool updateValid = _repository.Update(item, User.Identity.Name);
                if (updateValid)
                {
                    ViewBag.Message = "Update successful";
                    return RedirectToAction("GetAllPhotos");
                }
                ViewBag.Message = "Update failed";
                return View(item);
            }
            ViewBag.Message = "Update failed";
            return View(item);

        }

        [Authorize]
        public ViewResult AddPhoto()
        {
            return View(new Photo());
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddPhoto(Photo item, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fileName = Guid.NewGuid().ToString();
                    var path = Path.Combine(Server.MapPath("~/App_Data/ImagesBase/"), fileName);
                    file.SaveAs(path);
                    item.Created = DateTime.UtcNow;
                    item.DataId = fileName;
                    _repository.Add(item, User.Identity.Name);
                }
                catch (Exception)
                {
                    ViewBag.Message = "Upload failed";
                    return View("AddPhoto");

                }
                ViewBag.Message = "Upload successful";
                return View("AddPhoto", new Photo());
            }
            return View(item);


        }

        [Authorize]
        public ActionResult DeletePhoto(int photoId)
        {
            bool isdeleted = _repository.Delete(photoId);
            if (isdeleted)
            {
                ViewBag.Message = "Delete successful";
                return RedirectToAction("GetAllPhotos");
            }
            ViewBag.Message = "Delete failed";
            return View("UpdatePhoto");
        }

        public ActionResult ShowPhoto(string dataId)
        {
            var dir = Server.MapPath("~/App_Data/ImagesBase/");
            var path = Path.Combine(dir, dataId);
            return File(path, "image/jpeg");
        }


    }
}
