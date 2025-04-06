using System;
using System.Collections.Generic;

namespace TravelApp.Models
{
    public class BookingViewModel
    {
        public string Destination { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<Review> Reviews { get; set; }
    }

    public class Review
    {
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
    }
}
