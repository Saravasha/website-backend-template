using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppBackend.Data;
using WebAppBackend.Models;
using WebAppBackend.ViewModels;

namespace WebAppBackend.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cvm = new CategoryViewModel();
            cvm.Categories = _context.Categories.ToList();
            return View(cvm);
        }
        // GET: CategoryController/Create
        public IActionResult Create()
        {
            var ccvm = new CreateCategoryViewModel();
            return View(ccvm);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryViewModel cat)
        {
            if (ModelState.IsValid)
            {
                if (_context.Categories.Any(c => c.Name == cat.Name))
                {
                    ModelState.AddModelError(nameof(cat.Name), "A category with this name already exists.");
                    return View(cat);
                }

                Category catToAdd = new Category()
                {
                    Name = cat.Name
                };

                _context.Categories.Add(catToAdd);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError(nameof(cat.Name),
                        "A category with this name already exists.");
                    return View(cat);
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(cat);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryViewModel cat)
        {
            var categoryToEdit = await _context.Categories.FindAsync(id);

            if (categoryToEdit == null)
            {
                return NotFound();
            }

            if (_context.Categories.Any(c => c.Name == cat.Name && c.Id != id))
            {
                ModelState.AddModelError(nameof(cat.Name),
                    "A category with this name already exists.");

                return View(cat);
            }

            if (!ModelState.IsValid)
            {
                return View(cat);
            }

            categoryToEdit.Name = cat.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(nameof(cat.Name),
                    "A category with this name already exists.");

                return View(cat);
            }

            return RedirectToAction(nameof(Index));
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