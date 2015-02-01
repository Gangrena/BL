using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetterLife.Domain.Concrete;
using BetterLife.Domain.Entities;

namespace BetterLife.WebUi.Controllers
{
    public class SocialController : Controller
    {
        private AggregateRepositories _repo;

        public SocialController()
        {
            _repo = new AggregateRepositories();
        }

        public ActionResult Index()
        {
            IQueryable<Photo> photos = _repo.Photos.GetAll(); // do przebudowy puki co moze być 
            return View(photos);
        }

        //public ActionResult RenderPhoto(string dataId)
        //{
        //  if (dataId != null)
        //        {
        //            var dir = Server.MapPath("~/App_Data/ImagesBase/");
        //            var path = Path.Combine(dir, dataId);         
        //            byte[] imageArray = File(path, "image/jpeg");
        //            if (imageArray == null)
        //                return File(noPhotoArray, "image/jpg");

        //            #region Validate that the uploaded picture is an image - temporary code

        //            // Get Mime Type
        //            byte[] buffer = new byte[256];
        //            buffer = imageArray.Take(imageArray.Length >= 256 ? 256 : imageArray.Length).ToArray();
        //            var mimeType = UrlmonMimeType.GetMimeType(buffer);

        //            if (String.IsNullOrEmpty(mimeType) || mimeType.IndexOf("image") == -1)
        //                return File(noPhotoArray, "image/jpg");

        //            #endregion

        //            return File(imageArray, "image/jpg");
        //        }
        //    }
        //    catch
        //    {
        //        return File(noPhotoArray, "image/jpg");
        //    }
        //}

    }
}
