using TravelApp.Models;

namespace TravelApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int TourId { get; set; }
        public string UserId { get; set; } = string.Empty; // Foreign key to User
        public DateTime CreatedAt { get; set; }

        public Tour Tour { get; set; } = null!;
    }
}