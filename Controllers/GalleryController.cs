using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace TravelApp.Controllers
{
    public class GalleryController : Controller
    {
        private readonly string _galleryPath;

        public GalleryController()
        {
            _galleryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "gallery");

            if (!Directory.Exists(_galleryPath))
            {
                Directory.CreateDirectory(_galleryPath);
            }
        }

        // GET: /Gallery/Gallery
        public IActionResult Gallery()
        {
            var images = Directory.GetFiles(_galleryPath)
                                  .Select(Path.GetFileName)
                                  .ToList();

            return View(images);
        }

        // POST: /Gallery/Upload
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var savePath = Path.Combine(_galleryPath, fileName);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }
            }

            return RedirectToAction("Gallery");
        }
    }
}
