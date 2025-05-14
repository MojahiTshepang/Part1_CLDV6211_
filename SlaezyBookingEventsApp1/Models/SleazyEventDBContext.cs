using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SlaezyBookingEventsApp1.Models
{
    public partial class SleazyEventDBContext : DbContext
    {
        public SleazyEventDBContext()
            : base("name=SleazyEventDBContext")
        {
        }

        public virtual DbSet<Eventss> Eventsses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Eventss>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Eventss>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<Eventss>()
                .Property(e => e.Email)
                .IsUnicode(false);
        }
    }
}
