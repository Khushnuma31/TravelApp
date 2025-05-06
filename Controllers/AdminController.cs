using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TravelApp.Models;
using TravelApp.Data;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TravelApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager; // Add this line to declare _userManager

        // Inject UserManager and ApplicationDbContext into the constructor
        public AdminController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager; // Initialize _userManager
        }

        private const string AdminUsername = "admin";
        private const string AdminPassword = "admin123";

        // Admin Login
        [Route("Admin/AdminLogin")]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View("AdminLogin", new LoginViewModel());
        }

        // Handle Admin Login POST request
        [HttpPost]
        [Route("Admin/AdminLogin")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View("AdminLogin", model);
            }

            if (model.UsernameOrEmail == AdminUsername && model.Password == AdminPassword)
            {
                HttpContext.Session.SetString("AdminLoggedIn", "true");
                returnUrl = returnUrl ?? Url.Action("Index", "Admin");
                return LocalRedirect(returnUrl);
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View("AdminLogin", model);
        }

        // Admin Dashboard
        [Route("Admin/Index")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") != "true")
            {
                return RedirectToAction("Login");
            }

            var usersCount = _context.Users.Count();
            var bookingsCount = _context.Bookings.Count();
            var toursCount = _context.Tours.Count();
            var servicesCount = _context.Services.Count();

            var dashboardData = new
            {
                UsersCount = usersCount,
                BookingsCount = bookingsCount,
                ToursCount = toursCount,
                ServicesCount = servicesCount
            };

            return View("Dashboard", dashboardData);
        }

        // Admin Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AdminLoggedIn");
            return RedirectToAction("Login", "Admin");
        }

        // GET: Admin/Users (View Users)
        [Route("Admin/Users")]
        public IActionResult Users()
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") != "true")
            {
                return RedirectToAction("Login");
            }

            var users = _context.Users.ToList();
            return View(users);
        }

        // GET: Admin/Bookings (View Bookings)
        [Route("Admin/Bookings")]
        public IActionResult Bookings()
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") != "true")
            {
                return RedirectToAction("Login");
            }

            var bookings = _context.Bookings.Include(b => b.Tour).ToList();
            return View(bookings);
        }

        // GET: Admin/Tours (View Tours)
        [Route("Admin/Tours")]
        public IActionResult Tours()
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") != "true")
            {
                return RedirectToAction("Login");
            }

            var tours = _context.Tours.ToList();
            return View(tours);
        }

        // GET: Admin/Services (View Services)
        [Route("Admin/Services")]
        public IActionResult Services()
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") != "true")
            {
                return RedirectToAction("Login");
            }

            var services = _context.Services.ToList();
            return View(services);
        }

        //----------- ADMIN USERS TABLE CRUD -----------

        // GET: All Users
        public IActionResult DashboardIndex()
        {
            var users = _userManager.Users.ToList(); // Fetch users using UserManager
            return View(users);
        }

        // POST: Create a new User
        [HttpPost]
        public async Task<IActionResult> Create(string FullName, string Email, string Password)
        {
            var user = new User
            {
                UserName = FullName,
                Email = Email
            };

            var result = await _userManager.CreateAsync(user, Password); // Create the user

            if (result.Succeeded)
            {
                return RedirectToAction("Index"); // Redirect to Users index
            }

            return View("Index"); // Return to the same view if creation failed
        }

        // POST: Edit an existing User
        [HttpPost]
        public async Task<IActionResult> Edit(string Id, string FullName, string Email, string Password)
        {
            var user = await _userManager.FindByIdAsync(Id); // Find the user by ID

            if (user != null)
            {
                user.UserName = FullName;
                user.Email = Email;

                // Update password if provided
                if (!string.IsNullOrEmpty(Password))
                {
                    await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user, Password);
                }

                var result = await _userManager.UpdateAsync(user); // Update the user

                if (result.Succeeded)
                {
                    return RedirectToAction("Index"); // Redirect to Users index
                }
            }

            return View("Index"); // Return to the same view if update failed
        }

        // GET: Delete User
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id); // Find user by ID
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user); // Delete the user
                if (result.Succeeded)
                {
                    return RedirectToAction("Index"); // Redirect to Users index
                }
            }

            return RedirectToAction("Index"); // Redirect to Users index if deletion fails
        }

        //----------- END OF ADMIN USERS TABLE CRUD -----------

    }
}
