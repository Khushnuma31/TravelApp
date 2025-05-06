namespace TravelApp.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public string Destination { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public int? Distance { get; set; } // Add this
        public int? MaxPeople { get; set; }
        public int Nights { get; set; } // Add to Booking or Tour
    }
}