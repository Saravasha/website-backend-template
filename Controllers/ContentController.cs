using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using WebAppBackend.Data;
using WebAppBackend.Models;
using WebAppBackend.ViewModels;


namespace WebAppBackend.Controllers
{
    [Authorize]
    public class ContentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContentController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {

            var contentList = _context.Contents.Include(c => c.Page).ToList();

            return View(contentList);
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

            var contentToAdd = new Content
            {
                Title = content.Title,
                Date = content.Date,
                Container = content.Container,
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

            contentToEdit.Title = content.Title;
            contentToEdit.Date = content.Date;
            contentToEdit.Container = content.Container;
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
