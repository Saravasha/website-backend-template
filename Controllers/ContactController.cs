using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppBackend.Data;

namespace WebAppBackend.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var page = _context.Pages.FirstOrDefault(x => x.Title == "Contact");
            return View(page);
        }
    }
}