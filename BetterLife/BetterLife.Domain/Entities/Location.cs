
using System.ComponentModel.DataAnnotations;

namespace BetterLife.Domain.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; }
        public double HouseNumber { get; set; }
        public string ZipCode { get; set; }
        public bool IsHomeTown { get; set; }
        public bool IsCurrentLocation { get; set; }
        public int PersonProfileId { get; set; }
        public virtual PersonProfile PersonProfile { get; set; }
    }
}
