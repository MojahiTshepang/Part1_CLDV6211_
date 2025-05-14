using Microsoft.EntityFrameworkCore;
using SlaezyBookingEventApp.Models; // Make sure to include your Models namespace

namespace SlaezyBookingEventApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Venue> Venues
        {
            get;
            set;
        }

        public DbSet<Event> Events
        {
            get;
            set;
        }

        public DbSet<Booking> Bookings
        {
            get;
            set;
        }
    }
}
