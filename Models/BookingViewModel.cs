using System.ComponentModel.DataAnnotations;

namespace TravelApp.Models
{
    public class BookingViewModel
    {
        public int TourId { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Booking date is required.")]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }

        [Required(ErrorMessage = "Number of guests is required.")]
        [Range(1, 100, ErrorMessage = "Guest count must be between 1 and 100.")]
        public int GuestCount { get; set; }
    }
}