using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelApp.Data;
using TravelApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace TravelApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public HomeController(ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeViewModel
            {
                HeroContent = await _context.HeroContents.FirstOrDefaultAsync() ?? new HeroContent(),
                Tours = await _context.Tours.ToListAsync(),
                Services = await _context.Services.ToListAsync(),
                Reviews = await _context.Reviews.Take(3).ToListAsync(),
                GalleryImages = await _context.GalleryImages.Take(8).ToListAsync(),
                ExperienceStats = new ExperienceStats
                {
                    SuccessfulTours = 50,
                    YearsExperience = 10,
                    TotalCustomers = 1000
                },
                AvailableDestinations = (await _context.Tours.Select(t => t.Destination).Distinct().ToListAsync())
    .Select(d => new SelectListItem { Value = d, Text = d }).ToList()
            };

            return View(viewModel);
        }

        //=========TOURS=======
        public async Task<IActionResult> Tours()
        {
            var viewModel = new HomeViewModel
            {
                Tours = await _context.Tours.ToListAsync(),
                AvailableDestinations = (await _context.Tours.Select(t => t.Destination).Distinct().ToListAsync())
                    .Select(d => new SelectListItem { Value = d, Text = d }).ToList()
                // Only include Tours and AvailableDestinations for the Tours page
            };

            return View(viewModel);
        }

        //=========ABOUT=======
        public IActionResult About()
        {
            var viewModel = new HomeViewModel
            {
                // Add about-specific data if needed (e.g., Team, Services from database)
                // For now, using static content in the view
            };
            return View(viewModel);
        }

        // Other actions (About, Privacy, etc.) can go here

        [HttpPost]
        public async Task<IActionResult> Subscribe(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                TempData["Message"] = "Thank you for subscribing!";
            }
            return RedirectToAction("Index");
        }
        public IActionResult Newsletter()
        {
            var model = new NewsletterViewModel
            {
                Heading = "Subscribe now to get useful traveling information.",
                Description = "Get weekly tips, destination guides, and travel offers.",
                ImageUrl = "/images/male-tourist.png"
            };

            return View(model);
        }

    }
}
