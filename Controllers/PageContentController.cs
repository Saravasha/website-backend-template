using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using System.IO;
using WebAppBackend.Data;

namespace WebAppBackend.Controllers
{

    [Authorize]
    public class PageContentController : Controller
    {

        private readonly ApplicationDbContext _context;

        public PageContentController(ApplicationDbContext context) { 
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAssets()
        {
            var assets = await _context.Assets.Select(a => new { a.Id, a.FileUrl, a.Name }).ToListAsync();
            return Ok(assets);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {

            string _uploadsFolder = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).ToString(), "Uploads");
            if (!Directory.Exists(_uploadsFolder))
            {
                Directory.CreateDirectory(_uploadsFolder);
            }
            if (file != null && file.Length > 0)
            {

                var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                var filePath = Path.Combine(_uploadsFolder, fileName);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Copy(filePath, "Uploads");
                }
                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

           
                    var fileUrl = $"/Uploads/{fileName}";
                    return Json(new { success = true, url = fileUrl });
            }

            return Json(new { url = "" });
        }
    }
}
