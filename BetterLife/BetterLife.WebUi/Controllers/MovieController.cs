using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetterLife.Domain.Entities;
using BetterLife.WebUi.ControllersLogic.MovieController;

namespace BetterLife.WebUi.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepository _repository = new MovieRepository();

        [Authorize]
        public ActionResult GetAllMovies()
        {
            IQueryable<GlobalMovie> globalMovies =
                _repository.GetAll().Where(x => x.GlobalMovieLikes.All(z => z.PersonProfile.Login != User.Identity.Name));
            return View("GetAllMovies", globalMovies);

        }
        [Authorize]
        public ActionResult GetAllMyMovies()
        {
            IQueryable<GlobalMovie> globalMovies =
                _repository.GetAll().Where(x => x.GlobalMovieLikes.Any(z => z.PersonProfile.Login == User.Identity.Name));
            return View("GetAllMyMovies", globalMovies.ToList());// bardzo wazne zeby tutaj byla lista w innym przypadku pieknie sypie błedami .... ;) 
        }
        public ActionResult GetAllPersonMovies(int personProfileId)
        {
            ViewBag.personProfileId = personProfileId;
            IQueryable<GlobalMovie> globalMovies =
                _repository.GetAll()
                    .Where(x => x.GlobalMovieLikes.Any(z => z.PersonProfile.PersonProfileId == personProfileId));
            return View("GetAllPersonMovies", globalMovies.ToList());// bardzo wazne zeby tutaj byla lista w innym przypadku pieknie sypie błedami .... ;) 
        }

        [Authorize]
        public ViewResult AddMovie()
        {
            return View(new GlobalMovie());
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddMovie(GlobalMovie item, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fileName = Guid.NewGuid().ToString();
                    var path = Path.Combine(Server.MapPath("~/App_Data/MoviesBase/"), fileName);
                    file.SaveAs(path);
                    item.Created = DateTime.UtcNow;
                    item.DataId = fileName;
                    var result = _repository.Add(item, User.Identity.Name);
                    if (result == null)
                    {
                        ViewBag.Message = "We have this position in Book List";
                        return View("AddMovie");
                    }

                }
                catch (Exception)
                {
                    ViewBag.Message = "Upload failed";
                    return View("AddMovie");

                }
                ViewBag.Message = "Upload successful";
                return RedirectToAction("GetAllMyMovies", new GlobalMovie());
            }
            return View(item);


        }

        [Authorize]
        public ActionResult AddToFavorite(int globalMovieId)
        {

            bool succes = _repository.AddToFavorite(globalMovieId, User.Identity.Name);
            return RedirectToAction(succes ? "GetAllMovies" : "GetAllMyMovies"); //tutaj ewentualnie mozna by na jakas inna strone i wyswietlic bledy ...  kurde duzo bledow tutaj sie tworzy a to przez to ze zasob juz nie jest dostepny ...
        }

        [Authorize]
        public ActionResult DeleteMovie(int globalMovieId)
        {
            bool isdeleted = _repository.Delete(globalMovieId, User.Identity.Name);
            if (isdeleted)
            {
                ViewBag.Message = "Delete successful";
                return RedirectToAction("GetAllMyMovies");
            }
            ViewBag.Message = "Delete failed";
            return RedirectToAction("GetAllMyMovies");//tutaj ewentualnie mozna by na jakas inna strone i wyswietlic bledy ... 
        }

        public ActionResult ShowMovie(string dataId)
        {
            var dir = Server.MapPath("~/App_Data/MoviesBase");
            var path = Path.Combine(dir, dataId);
            return File(path, "image/jpeg");
        }

    }
}
