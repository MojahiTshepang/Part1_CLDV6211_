using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SlaezyBookingEventApp.Models;

namespace SlaezyBookingEventApp.Models
{
    public class Event
    {
        public int EventID
        {
            get;
            set;
        }

        [Required]
        public string? EventName
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EventDate
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan EventTime
        {
            get;
            set;
        }

        public string? Description
        {
            get;
            set;
        }

        public string? ImageURL
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
    }
}