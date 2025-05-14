using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SlaezyBookingEventApp.Models;

namespace SlaezyBookingEventApp.Models
{
    public class Booking
    {
        public int BookingID
        {
            get;
            set;
        }

        [Required]
        public int VenueID
        {
            get;
            set;
        }

        [ForeignKey("VenueID")]
        public Venue? Venue
        {
            get;
            set;
        } // Navigation property

        [Required]
        public int EventID
        {
            get;
            set;
        }

        [ForeignKey("EventID")]
        public Event? Event
        {
            get;
            set;
        } // Navigation property

        public DateTime BookingDate
        {
            get;
            set;
        } = DateTime.Now;
    }
}
