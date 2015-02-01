using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BetterLife.WebUi.Models.ViewPersonProfile
{
    public class ImageViewModel
    {
        
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }

    }
}