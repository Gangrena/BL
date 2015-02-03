using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetterLife.Domain.Entities;
using BetterLife.WebUi.ControllersLogic.BookController;

namespace BetterLife.WebUi.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _repository = new BookRepository();

        [Authorize]
        public ActionResult GetAllBooks()
        {
            IQueryable<GlobalBook> globalBooks =
                _repository.GetAll().Where(x => x.GlobalBookLikes.All(z => z.PersonProfile.Login != User.Identity.Name));
            return View("GetAllBooks", globalBooks);

        }
        [Authorize]
        public ActionResult GetAllMyBooks()
        {
            IQueryable<GlobalBook> globalBooks =
                _repository.GetAll().Where(x => x.GlobalBookLikes.Any(z => z.PersonProfile.Login == User.Identity.Name));
            return View("GetAllMyBooks", globalBooks.ToList());// bardzo wazne zeby tutaj byla lista w innym przypadku pieknie sypie błedami .... ;) 
        }

        [Authorize]
        public ViewResult AddBook()
        {
            return View(new GlobalBook());
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddBook(GlobalBook item, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fileName = Guid.NewGuid().ToString();
                    var path = Path.Combine(Server.MapPath("~/App_Data/BooksBase/"), fileName);
                    file.SaveAs(path);
                    item.Created = DateTime.UtcNow;
                    item.DataId = fileName;
                    var result = _repository.Add(item, User.Identity.Name);
                    if (result == null)
                    { 
                        ViewBag.Message = "We have this position in Book List";
                        return View("AddBook");
                    }

                }
                catch (Exception)
                {
                    ViewBag.Message = "Upload failed";
                    return View("AddBook");

                }
                ViewBag.Message = "Upload successful";
                return RedirectToAction("GetAllMyBooks", new GlobalBook());
            }
            return View(item);


        }

        [Authorize]
        public ActionResult AddToFavorite(int globalBookId)
        {

            bool succes = _repository.AddToFavorite(globalBookId, User.Identity.Name);
            return RedirectToAction(succes ? "GetAllBooks" : "GetAllMyBooks");//tutaj ewentualnie mozna by na jakas inna strone i wyswietlic bledy ... 
        }

        [Authorize]
        public ActionResult DeleteBook(int globalBookId)
        {
            bool isdeleted = _repository.Delete(globalBookId, User.Identity.Name);
            if (isdeleted)
            {
                ViewBag.Message = "Delete successful";
                return RedirectToAction("GetAllMyBooks");
            }
            ViewBag.Message = "Delete failed";
            return RedirectToAction("GetAllMyBooks");//tutaj ewentualnie mozna by na jakas inna strone i wyswietlic bledy ... 
        }

        public ActionResult ShowBook(string dataId)
        {
            var dir = Server.MapPath("~/App_Data/BooksBase");
            var path = Path.Combine(dir, dataId);
            return File(path, "image/jpeg");
        }

    }
}
