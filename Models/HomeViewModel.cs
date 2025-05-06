using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TravelApp.Models
{
    public class HomeViewModel
    {
        public HeroContent HeroContent { get; set; } = new HeroContent();
        public List<Tour> Tours { get; set; } = new List<Tour>();
        public List<Service> Services { get; set; } = new List<Service>();
        public List<Review> Reviews { get; set; } = new List<Review>(); // Use Reviews for testimonials
        public List<GalleryImage> GalleryImages { get; set; } = new List<GalleryImage>();
        public ExperienceStats ExperienceStats { get; set; } = new ExperienceStats();
        public List<SelectListItem> AvailableDestinations { get; set; }
    }
}