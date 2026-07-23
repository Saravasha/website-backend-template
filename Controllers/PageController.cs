using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using WebAppBackend.Data;
using WebAppBackend.Models;
using WebAppBackend.Services;
using WebAppBackend.ViewModels;


namespace WebAppBackend.Controllers
{
    [Authorize]
    public class PageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHtmlSanitizerService _htmlSanitizer;

        public PageController(ApplicationDbContext context, IHtmlSanitizerService htmlSanitizer)
        {
            _context = context;
            _htmlSanitizer = htmlSanitizer;
        }


        public async Task<IActionResult> Index()
        {
            var model = new List<PageViewModel>();

            var pages = await _context.Pages
                .Include(p => p.Contents)
                .ToListAsync();

            foreach (var page in pages)
            {
                model.Add(new PageViewModel
                {
                    Id = page.Id,
                    Title = page.Title,
                    Container = page.Container,
                    Contents = page.Contents.ToList(),
                });
            }

            return View(model);
        }


        // GET: PageController/Details/5
        public IActionResult Details(int id)
        {
            Page? page = _context.Pages
                .Include(c => c.Contents)
                .FirstOrDefault(p => p.Id == id);

            return View(page);
        }

        //GET: PageController/Create
        public IActionResult Create()
        {
            CreatePageViewModel cpvm = new CreatePageViewModel();
            var contents = _context.Contents;

            ViewBag.ContentList = new MultiSelectList(contents, "Id", "Title");

            return View(cpvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePageViewModel page)
        {

            if (ModelState.IsValid)
            {

                //Sanitize the Container content before saving to the database
                var sanitizedContainer = _htmlSanitizer.Sanitize(page.Container);

                var pageToAdd = new Page
                {
                    Title = page.Title,
                    Container = sanitizedContainer,
                    Contents = new List<Content>()
                };

                if (page.ContentIds != null && page.ContentIds.Any())
                {
                    var selectedContents = await _context.Contents
                        .Where(c => page.ContentIds.Contains(c.Id))
                        .ToListAsync();

                    pageToAdd.Contents.AddRange(selectedContents);
                }


                _context.Pages.Add(pageToAdd);;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Repopulate ContentList if validation fails
            ViewBag.ContentList = new MultiSelectList(await _context.Contents.ToListAsync(), "Id", "Title", page.ContentIds);

            return View(page);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {

            // Try to get the page with its related contents
            var page = _context.Pages
                .Include(p => p.Contents)
                .FirstOrDefault(p => p.Id == id);

            foreach (var content in page.Contents)
            {
                Console.WriteLine($"Content ID: {content.Id}, Title: {content.Title}, Date: {(content.Date.HasValue ? content.Date.Value.ToString() : "NULL")}");
            }

            if (page == null)
            {
                return NotFound();
            }

            // Initialize ViewModel
            var cpvm = new CreatePageViewModel
            {
                Title = page.Title,
                Container = page.Container,
                ContentIds = page.Contents.Select(c => c.Id).ToList()
            };

            // Populate ViewBag with content options for multiselect
            ViewBag.ContentList = new MultiSelectList(_context.Contents, "Id", "Title");

            return View(cpvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreatePageViewModel page)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ContentList = new MultiSelectList(await _context.Contents.ToListAsync(), "Id", "Title", page.ContentIds);
                return View(page);
            }

            //Sanitize the Container content before saving to the database
            var sanitizedContainer = _htmlSanitizer.Sanitize(page.Container);

            var pageToEdit = await _context.Pages.Include(p => p.Contents).FirstOrDefaultAsync(p => p.Id == id);

            if (pageToEdit == null)
            {
                return NotFound();
            }

            pageToEdit.Title = page.Title;
            pageToEdit.Container = sanitizedContainer;

            // Clear old contents
            pageToEdit.Contents.Clear();

            if (page.ContentIds != null && page.ContentIds.Any())
            {
                var selectedContents = await _context.Contents
                    .Where(c => page.ContentIds.Contains(c.Id))
                    .ToListAsync();

                foreach (var content in selectedContents)
                {
                    pageToEdit.Contents.Add(content);
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null || _context.Pages == null)
            {
                return NotFound();
            }

            var page = await _context.Pages.Include(c => c.Contents)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // POST: AssetController/Delete/5

        // POST: Db/Delete/5
        [HttpPost, ActionName("Delete")]
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, IFormCollection collection)
        {
            if (_context.Pages == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pages' is null.");
            }
            var content = await _context.Pages.FindAsync(id);

            var page = await _context.Pages.OrderBy(e => e.Title).Include(e => e.Contents).FirstAsync();

            foreach (var cont in page.Contents)
            {
                cont.Page = null;
            }

            if (content != null)
            {
                _context.Pages.Remove(content);
            }


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
