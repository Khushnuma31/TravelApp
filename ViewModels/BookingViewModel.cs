using System.ComponentModel.DataAnnotations;
using TravelApp.Models;

namespace TravelApp.ViewModels // Create a ViewModels folder if it doesn't exist
{
    public class BookingViewModel
    {
        public Tour Tour { get; set; }

        public int TourId { get; set; } // Keep TourId for form submission

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; } = DateTime.Today; // Default to today

        [Required]
        [Range(1, 20, ErrorMessage = "Number of guests must be between 1 and 20")]
        [Display(Name = "Guests")]
        public int GuestCount { get; set; } = 1; // Default to 1 guest

        public decimal ServiceCharge { get; set; } = 10.00m; // Fixed service charge for now

        public decimal TotalPrice { get; set; } // Calculated in controller or JS
    }
} 