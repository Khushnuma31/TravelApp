using Microsoft.AspNetCore.Mvc;

namespace TravelApp.Controllers
{
    public class ToursController : Controller
    {
        [Route("Tours")]
        public IActionResult AllTours()
        {
            return View("Tours"); // This will render Views/Tours/Tours.cshtml
        }
    }
}
