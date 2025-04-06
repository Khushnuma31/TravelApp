using Microsoft.AspNetCore.Mvc;
using TravelApp.Models;
using System;
using System.Collections.Generic;

namespace TravelApp.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Book(string destination)
        {
            var model = new BookingViewModel
            {
                Destination = destination,
                Description = $"Experience the beauty and culture of {destination}.",
                Price = 150.00m,
                Reviews = new List<Review>
                {
                    new Review { UserName = "Alex", Rating = 4, Comment = "Great trip!", Date = DateTime.Now.AddDays(-3) },
                    new Review { UserName = "Mia", Rating = 5, Comment = "Loved every moment!", Date = DateTime.Now.AddDays(-1) }
                }
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult ConfirmBooking(string Destination, string UserName, string Phone, string Date, int Guests, string ImageUrl, decimal PricePerPerson)
        {
            ViewBag.Message = "Booking Confirmed!";
            ViewBag.Destination = Destination;
            ViewBag.UserName = UserName;
            ViewBag.Phone = Phone;
            ViewBag.Date = Date;
            ViewBag.Guests = Guests;
            ViewBag.Total = PricePerPerson * Guests + 10;

            return View("Confirmation");
        }
    }
}
