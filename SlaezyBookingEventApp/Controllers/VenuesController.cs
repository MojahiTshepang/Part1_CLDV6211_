using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SlaezyBookingEventApp.Data;
using SlaezyBookingEventApp.Models;
using System.Threading.Tasks;
using SlaezyBookingEventApp.Services; // Make sure this is included
using System; // Add this for exceptions
using Microsoft.AspNetCore.Http;

namespace SlaezyBookingEventApp.Controllers
{
    public class VenuesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService; // Inject the image service

        // This should be your ONLY public constructor for dependency injection
        public VenuesController(ApplicationDbContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        // GET: Venues
        public async Task<IActionResult> Index()
        {
            return View(await _context.Venues.ToListAsync());
        }

        // GET: Venues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venues
                .FirstOrDefaultAsync(m => m.VenueID == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // GET: Venues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Venues/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VenueID,VenueName,Location,Capacity,ImageURL,ImageFile")] Venue venue, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    try
                    {
                        // Handle image upload here using your _imageService
                        var imageName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(ImageFile.FileName);
                        using (var stream = ImageFile.OpenReadStream())
                        {
                            var blobUrl = await _imageService.UploadImageAsync(stream, imageName);
                            venue.ImageURL = blobUrl; // Store the Blob URL
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("ImageFile", "Error uploading image: " + ex.Message);
                        return View(venue); // Return to the form with the error
                    }
                }

                _context.Add(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

        // GET: Venues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venues.FindAsync(id);
            if (venue == null)
            {
                return NotFound();
            }
            return View(venue);
        }

        // POST: Venues/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VenueID,VenueName,Location,Capacity,ImageURL,ImageFile")] Venue venue, IFormFile ImageFile)
        {
            if (id != venue.VenueID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        // Delete the old image first
                        if (!string.IsNullOrEmpty(venue.ImageURL))
                        {
                            await _imageService.DeleteImageAsync(venue.ImageURL);
                        }
                        //upload new image.
                        var imageName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(ImageFile.FileName);
                        using (var stream = ImageFile.OpenReadStream())
                        {
                            var blobUrl = await _imageService.UploadImageAsync(stream, imageName);
                            venue.ImageURL = blobUrl; // Store the Blob URL
                        }
                    }
                    _context.Update(venue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueExists(venue.VenueID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ImageFile", "Error uploading image: " + ex.Message);
                    return View(venue);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

        // GET: Venues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venues
                .FirstOrDefaultAsync(m => m.VenueID == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // POST: Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _context.Venues.FindAsync(id);

            if (venue != null && !string.IsNullOrEmpty(venue.ImageURL))
            {
                // Delete the associated image from Blob Storage
                await _imageService.DeleteImageAsync(venue.ImageURL);
            }

            _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenueExists(int id)
        {
            return _context.Venues.Any(e => e.VenueID == id);
        }
    }
}

