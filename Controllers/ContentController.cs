using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppBackend.Data;
using WebAppBackend.Models;
using WebAppBackend.Services;
using WebAppBackend.ViewModels;


namespace WebAppBackend.Controllers
{
    [Authorize]
    public class ContentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHtmlSanitizerService _htmlSanitizer;

        public ContentController(ApplicationDbContext context, IHtmlSanitizerService htmlSanitizer)
        {
            _context = context;
            _htmlSanitizer = htmlSanitizer;

        }

        public async Task<IActionResult> Index()
        {
            var model = new List<ContentViewModel>();

            var contents = await _context.Contents
                .Include(c => c.Page)
                .ToListAsync();

            foreach (var content in contents)
            {
                model.Add(new ContentViewModel
                {
                    Id = content.Id,
                    Title = content.Title,
                    Container = content.Container,
                    Date = content.Date,
                    Page = content.Page
                });
            }

            return View(model);
        }

        // GET: PageController/Details/5
        public IActionResult Details(int id)
        {
            Content? content = _context.Contents
                .Include(c => c.Page)
                .FirstOrDefault(p => p.Id == id);

            return View(content);
        }

        //GET: PageController/Create
        public IActionResult Create()
        {
            CreateContentViewModel ccvm = new CreateContentViewModel();
            var pages = _context.Pages;

            ViewBag.PageList = new SelectList(pages, "Id", "Title");

            return View(ccvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateContentViewModel content)
        {
            if (!ModelState.IsValid || content.PageId == 0)
            {
                if (content.PageId == 0)
                {
                    ViewBag.PageError = "Page is required";
                }

                ViewBag.PageList = new SelectList(await _context.Pages.ToListAsync(), "Id", "Title", content.PageId);
                return View(content);
            }

            //Sanitize the Container content before saving to the database
            var sanitizedContainer = _htmlSanitizer.Sanitize(content.Container);

            var contentToAdd = new Content
            {
                Title = content.Title,
                Date = content.Date,
                Container = sanitizedContainer,
                PageId = content.PageId
            };

            _context.Contents.Add(contentToAdd);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {

            CreateContentViewModel ccvm = new CreateContentViewModel();
            var content = _context.Contents
                .Include(c => c.Page)
                .FirstOrDefault(p => p.Id == id);

            if (content != null)
            {
                ccvm.Title = content.Title;
                ccvm.Date = content.Date;
                ccvm.Container = content.Container;
                ccvm.PageId = content.PageId;


                var pages = _context.Pages;

                ViewBag.PageList = new SelectList(pages, "Id", "Title");
            }

            return View(ccvm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateContentViewModel content)
        {
            
            if (!ModelState.IsValid)
            {
                ViewBag.PageList = new SelectList(await _context.Pages.ToListAsync(), "Id", "Title", content.PageId);
                return View(content);
            }

            var contentToEdit = await _context.Contents.FindAsync(id);

            if (contentToEdit == null)
            {
                return NotFound();
            }
            //Sanitize the Container content before saving to the database
            var sanitizedContainer = _htmlSanitizer.Sanitize(content.Container);

            contentToEdit.Title = content.Title;
            contentToEdit.Date = content.Date;
            contentToEdit.Container = sanitizedContainer;
            contentToEdit.PageId = content.PageId;

            _context.Contents.Update(contentToEdit);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            

            if (id == null || _context.Contents == null)
            {
                return NotFound();
            }

            var cont = await _context.Contents.Include(p => p.Page)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cont == null)
            {
                return NotFound();
            }

            return View(cont);
        }

        // POST: AssetController/Delete/5

        // POST: Db/Delete/5
        [HttpPost, ActionName("Delete")]
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, IFormCollection collection)
        {
            if (_context.Contents == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Contents' is null.");
            }
            var content = await _context.Contents.FindAsync(id);
            if (content != null)
            {
                _context.Contents.Remove(content);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
