using System.ComponentModel.DataAnnotations.Schema;
using TravelApp.Models;

public class Booking
{
    public int Id { get; set; }
    public int TourId { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime BookingDate { get; set; }
    public int GuestCount { get; set; }
    public decimal TotalPrice { get; set; }
    public string? PaymentId { get; set; }

    public virtual Tour Tour { get; set; }
    public virtual User User { get; set; }
}
