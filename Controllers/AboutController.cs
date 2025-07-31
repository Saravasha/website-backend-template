using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppBackend.Data;

namespace WebAppBackend.Controllers
{
    [Authorize]
    public class AboutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AboutController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var page = _context.Pages.FirstOrDefault(x => x.Title == "About");
            return View(page);
        }
    }
}
