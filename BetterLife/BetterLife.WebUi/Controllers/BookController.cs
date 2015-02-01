using System;
using System.Collections;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetterLife.Domain.Entities;
using BetterLife.WebUi.ControllersLogic.BookController;
using BetterLife.WebUi.Models;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;

namespace BetterLife.WebUi.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _repository = new BookRepository();
        private readonly GetPersonId _getPersonId = new GetPersonId();
        private IFavoriteRepository _favoriteRepository;

        [Authorize]
        public ActionResult GetAllBooks()
        {
            int personId = _getPersonId.GetId(User.Identity.Name);
            //IQueryable<Book> allBooks =
            //    _repository.GetAll().Where(x => x.PersonId != personId);
            //IEnumerable<Book> affterFilter = allBooks.GroupBy(x => x.Title).Select(z => z.First());     
            //var allBooks =
            //    (IQueryable<Book>) _repository.GetAll().DistinctBy(x => x.Title);
            //jezus ... 
            IEnumerable<Book> allBooks =
                _repository.GetAll().DistinctBy(x => x.Created).DistinctBy(y => y.Title);

            ViewBag.Id = personId;
            return View("GetAllBooks",allBooks.Where(x=>x.PersonId != personId));
               
        }

        [Authorize]
        public ActionResult GetAllMyBooks()
        {
            IQueryable<Book> allBooks = _repository.GetAll();
            return View("GetAllMyBooks",allBooks
               );
        }

        [Authorize]
        public ViewResult AddBook()
        {
            return View(new Book());
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddBook(Book item, HttpPostedFileBase file)
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
                    _repository.Add(item, User.Identity.Name);
                }
                catch (Exception)
                {
                    ViewBag.Message = "Upload failed";
                    return View("AddBook");

                }
                ViewBag.Message = "Upload successful";
                return RedirectToAction("GetAllMyBooks", new Book());
            }
            return View(item);


        }

        [Authorize]
        public ActionResult AddToFavorite(int bookId)
        {
            _favoriteRepository = new FavoriteRepository();
            bool succes = _favoriteRepository.AddToFavorite(bookId, User.Identity.Name);
            if (succes)
            {
                return RedirectToAction("GetAllBooks");
            }
            return RedirectToAction("GetAllBooks");
        }

        [Authorize]
        public ActionResult DeleteBook(int bookId)
        {
            bool isdeleted = _repository.Delete(bookId);
            if (isdeleted)
            {
                ViewBag.Message = "Delete successful";
                return RedirectToAction("GetAllMyBooks");
            }
            ViewBag.Message = "Delete failed";
            return View("GetAllMyBooks");
        }

        public ActionResult ShowBook(string dataId)
        {
            var dir = Server.MapPath("~/App_Data/BooksBase");
            var path = Path.Combine(dir, dataId);
            return File(path, "image/jpeg");
        }

    }
}
