using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlaezyBookingEventApp.Models
{
    public class Venue
    {
        public int VenueID
        {
            get;
            set;
        }

        [Required]
        public string? VenueName
        {
            get;
            set;
        }

        [Required]
        public string? Location
        {
            get;
            set;
        }

        [Required]
        public int Capacity
        {
            get;
            set;
        }

        public string? ImageURL
        {
            get;
            set;
        }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}