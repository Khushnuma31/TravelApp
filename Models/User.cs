using Microsoft.AspNetCore.Identity;

namespace TravelApp.Models
{
    public class User : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
