using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelApp.Data;
using TravelApp.Models;

namespace TravelApp.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager; // Updated to UserManager<User>

        public BookingController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Booking/Booking/5
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Booking(int tourId)
        {
            var tour = await _context.Tours.FindAsync(tourId);
            if (tour == null)
            {
                return NotFound();
            }

            ViewBag.Tour = tour;
            var model = new BookingViewModel
            {
                TourId = tour.Id
            };
            return View(model);
        }

        // POST: Booking/ConfirmBooking
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmBooking(BookingViewModel model)
        {
            var tour = await _context.Tours.FindAsync(model.TourId);
            if (tour == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User); // Works with UserManager<User>
                if (string.IsNullOrEmpty(userId))
                {
                    ModelState.AddModelError("", "User not found. Please log in again.");
                    ViewBag.Tour = tour;
                    return View("Booking", model);
                }

                var booking = new Booking
                {
                    TourId = model.TourId,
                    UserId = userId,
                    FullName = model.FullName,
                    Phone = model.Phone,
                    BookingDate = model.BookingDate,
                    GuestCount = model.GuestCount,
                    TotalPrice = tour.Price * model.GuestCount
                };

                _context.Add(booking);
                var saved = await _context.SaveChangesAsync();
                if (saved > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"Booking saved with ID: {booking.Id}");
                    ViewBag.Booking = booking;
                    return View("Payment", model);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Failed to save booking.");
                    ModelState.AddModelError("", "Failed to save booking. Please try again.");
                }
            }

            ViewBag.Tour = tour;
            return View("Booking", model);
        }

        // GET: Booking/Payment
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Payment(int bookingId)
        {
            System.Diagnostics.Debug.WriteLine($"Attempting to fetch booking with ID: {bookingId}");
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null)
            {
                System.Diagnostics.Debug.WriteLine($"Booking not found for ID: {bookingId}");
                return NotFound("Booking not found for ID: " + bookingId);
            }

            var userId = _userManager.GetUserId(User);
            if (booking.UserId != userId)
            {
                return Unauthorized("You are not authorized to view this booking.");
            }

            ViewBag.Booking = booking;
            ViewBag.UserId = userId;

            System.Diagnostics.Debug.WriteLine($"Fetched Booking: {booking.Id}, UserId: {userId}");
            return View(new BookingViewModel
            {
                TourId = booking.TourId,
                FullName = booking.FullName,
                Phone = booking.Phone,
                BookingDate = booking.BookingDate,
                GuestCount = booking.GuestCount
            });
        }

        // POST: Booking/CompleteBooking
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteBooking(BookingViewModel model, string razorpayPaymentId)
        {
            System.Diagnostics.Debug.WriteLine($"CompleteBooking called with TourId: {model.TourId}, PaymentId: {razorpayPaymentId}");
            var tour = await _context.Tours.FindAsync(model.TourId);
            if (tour == null || string.IsNullOrEmpty(razorpayPaymentId))
            {
                System.Diagnostics.Debug.WriteLine("Tour or PaymentId not found.");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingBooking = await _context.Bookings
                    .FirstOrDefaultAsync(b => b.TourId == model.TourId && b.FullName == model.FullName && b.BookingDate == model.BookingDate);
                if (existingBooking != null)
                {
                    var userId = _userManager.GetUserId(User);
                    if (existingBooking.UserId != userId)
                    {
                        return Unauthorized("You are not authorized to update this booking.");
                    }

                    existingBooking.PaymentId = razorpayPaymentId;
                    var saved = await _context.SaveChangesAsync();
                    if (saved > 0)
                    {
                        System.Diagnostics.Debug.WriteLine($"Payment updated for booking ID: {existingBooking.Id}");
                        return RedirectToAction("Payment", new { bookingId = existingBooking.Id });
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Failed to update payment.");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No existing booking found for TourId: " + model.TourId);
                }

                ModelState.AddModelError("", "Failed to update booking with payment. Please try again.");
            }

            ViewBag.Tour = tour;
            return View("Payment", model);
        }
    }
}