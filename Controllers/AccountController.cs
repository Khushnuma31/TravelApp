using Microsoft.AspNetCore.Mvc;

namespace travelApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            // Simulating a login process (Replace with actual authentication logic)
            if (Email == "admin@gmail.com" && Password == "password")
            {
                return RedirectToAction("Index", "Home"); // Redirect to home if successful
            }

            ViewBag.Error = "Invalid credentials. Please try again.";
            return View();
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View(); // Redirect to Register page
        }

        // GET: /Account/ForgotPassword
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View(); // Redirect to Forgot Password page
        }
    }
}
