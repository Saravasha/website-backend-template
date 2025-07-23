using GoshehArtWebApp.Data;
using GoshehArtWebApp.Models;
using GoshehArtWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoshehArtWebApp.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _context;
        static readonly CategoryViewModel cvm = new();
        static readonly CreateCategoryViewModel ccvm = new();
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            cvm.Categories = _context.Categories.ToList();
            return View(cvm);
        }
        // GET: CategoryController/Create
        public IActionResult Create()
        {
            return View(ccvm);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCategoryViewModel cat)
        {

            //ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                Category catToAdd = new Category()
                {
                    Name = cat.Name
                };

                _context.Categories.Add(catToAdd);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(ccvm);
            }

        }

        // GET: CategoryController/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cvm = new CategoryViewModel();
            var category = _context.Categories.Find(id);
            cvm.Name = category.Name;
            return View(cvm);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryViewModel cat)
        {
            var categoryToEdit = _context.Categories.Find(id);

            if (ModelState.IsValid)
            {
                categoryToEdit.Name = cat.Name;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(cat);
            }
        }

        // GET: Db/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            var cvm = new CategoryViewModel();
            var category = await _context.Categories.FindAsync(id);
            cvm.Name = category.Name;
            cvm.Assets = category.Assets;

            cvm.Categories = _context.Categories
                .Include(c => c.Assets).ToList();

            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var cat = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cat == null)
            {
                return NotFound();
            }

            return View(cvm);

        }

        // POST: Db/Delete/5
        [HttpPost, ActionName("Delete")]
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'ApplicationDbConext.Category'  is null.");
            }
            var cat = await _context.Categories.FindAsync(id);
            if (cat != null)
            {
                _context.Categories.Remove(cat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}