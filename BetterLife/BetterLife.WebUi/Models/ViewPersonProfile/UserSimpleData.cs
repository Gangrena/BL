using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BetterLife.WebUi.Models.ViewPersonProfile
{
    public class UserSimpleData
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Urodziny { get; set; }
    }
}