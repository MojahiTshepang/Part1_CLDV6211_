using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SlaezyBookingEventApp.Data;
using SlaezyBookingEventApp.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System;

namespace SlaezyBookingEventApp.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(m => m.BookingID == id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["EventID"] = new SelectList(_context.Events, "EventID", "EventName");
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "VenueName");
            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingID,VenueID,EventID,BookingDate")] Booking booking)
        {
            // 1. Error Handling and Validation: Double Booking Check
            booking.BookingDate = booking.BookingDate.Date; // Normalize date
            var existingBooking = await _context.Bookings
                .AnyAsync(b => b.VenueID == booking.VenueID &&
                               b.EventID == booking.EventID &&
                               b.BookingDate == booking.BookingDate);

            if (existingBooking)
            {
                ModelState.AddModelError("", "This venue is already booked for this event on this date.");
                ViewData["EventID"] = new SelectList(_context.Events, "EventID", "EventName", booking.EventID);
                ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "VenueName", booking.VenueID);
                return View(booking); // Return to the Create view with the error message
            }

            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventID"] = new SelectList(_context.Events, "EventID", "EventName", booking.EventID);
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "VenueName", booking.VenueID);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["EventID"] = new SelectList(_context.Events, "EventID", "EventName", booking.EventID);
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "VenueName", booking.VenueID);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingID,VenueID,EventID,BookingDate")] Booking booking)
        {
            if (id != booking.BookingID)
            {
                return NotFound();
            }

            // 1. Error Handling and Validation: Double Booking Check (for Edit)
            booking.BookingDate = booking.BookingDate.Date; // Normalize date
            var existingBooking = await _context.Bookings
                .AnyAsync(b => b.VenueID == booking.VenueID &&
                               b.EventID == booking.EventID &&
                               b.BookingDate == booking.BookingDate &&
                               b.BookingID != booking.BookingID); // Exclude the current booking

            if (existingBooking)
            {
                ModelState.AddModelError("", "This venue is already booked for this event on this date.");
                ViewData["EventID"] = new SelectList(_context.Events, "EventID", "EventName", booking.EventID);
                ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "VenueName", booking.VenueID);
                return View(booking); // Return to the Edit view with the error message
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventID"] = new SelectList(_context.Events, "EventID", "EventName", booking.EventID);
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "VenueName", booking.VenueID);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(m => m.BookingID == id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // 1. Error Handling and Validation:  Check for associated bookings before deletion
            var hasAssociatedBookings = await _context.Bookings.AnyAsync(b => b.VenueID == id || b.EventID == id);
            if (hasAssociatedBookings)
            {
                ModelState.AddModelError("", "Cannot delete venue/event because it has associated bookings.");
                // Consider redirecting to a different action or view
                return RedirectToAction(nameof(Index)); // Or another appropriate action
            }

            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingID == id);
        }
    }
}

