using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppBackend.Data;
using WebAppBackend.Models;
using WebAppBackend.Services;
using WebAppBackend.ViewModels.Pages;


namespace WebAppBackend.Controllers
{
    [Authorize]
    public class PageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHtmlSanitizerService _htmlSanitizer;
        private readonly PageService _pageService;

        public PageController(ApplicationDbContext context, IHtmlSanitizerService htmlSanitizer, PageService pageService)
        {
            _context = context;
            _htmlSanitizer = htmlSanitizer;
            _pageService = pageService;
        }

        private async Task PopulateParentPageListAsync(int pageId, int? selectedParentId)
        {
            var pages = await _context.Pages
        .Where(p => !p.IsOrphan)
        .ToListAsync();

            ViewBag.ParentPageList =
                _pageService.BuildParentPageList(
                    pages,
                    selectedParentId,
                    pageId);
        }


        public async Task<IActionResult> Index()
        {
            var pages = await _context.Pages
                .AsNoTracking()
                .ToListAsync();

            var rootPages = pages
                .Where(p =>
                    p.ParentId == null &&
                    !p.IsOrphan)
                .OrderBy(p => p.SortOrder)
                .ToList();

            var orphanedPages = pages
                .Where(p => p.IsOrphan)
                .OrderBy(p => p.SortOrder)
                .ToList();

            var model = new PageIndexViewModel
            {
                Pages = rootPages
                    .Select((page, index) => _pageService.BuildPageViewModel(
                        page,
                        pages,
                        index,
                        rootPages.Count))
                    .ToList(),

                OrphanedPages = orphanedPages
                    .Select((page, index) => _pageService.BuildPageViewModel(
                        page,
                        pages,
                        index,
                        orphanedPages.Count,
                        true))
                    .ToList(),

                InvisiblePageCount = pages.Count(p => !p.Visible),
                OrphanPageCount = pages.Count(p => p.IsOrphan)
            };

            return View(model);
        }

        // GET: PageController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var pages = await _context.Pages
                .AsNoTracking()
                .ToListAsync();

            var page = pages.FirstOrDefault(p => p.Id == id);

            if (page == null)
            {
                return NotFound();
            }

            var siblingPages = pages
                .Where(p =>
                    p.ParentId == page.ParentId &&
                    !p.IsOrphan)
                .OrderBy(p => p.SortOrder)
                .ToList();

            var index = siblingPages.FindIndex(p => p.Id == page.Id);

            var vm = _pageService.BuildPageViewModel(
                page,
                pages,
                index,
                siblingPages.Count);

            return View(vm);
        }

        // GET: PageController/Create
        public async Task<IActionResult> Create()
        {
            var cpvm = new CreatePageViewModel();

            var pages = await _context.Pages
      .Where(p => !p.IsOrphan)
      .ToListAsync();

            ViewBag.ParentPageList = _pageService.BuildParentPageList(pages);

            return View(cpvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePageViewModel page)
        {
            if (!ModelState.IsValid)
            {
                if (page.ParentId.HasValue)
                {
                    var parentExists = await _context.Pages
                        .AnyAsync(p =>
                            p.Id == page.ParentId.Value &&
                            !p.IsOrphan);

                    if (!parentExists)
                    {
                        ModelState.AddModelError(
                            nameof(page.ParentId),
                            "The selected parent page is invalid."
                        );

                        ViewBag.ParentPageList = new SelectList(
                            await _context.Pages
                                .Where(p => !p.IsOrphan)
                                .OrderBy(p => p.SortOrder)
                                .ToListAsync(),
                            "Id",
                            "Title",
                            page.ParentId
                        );

                        return View(page);
                    }
                }
            }

            var highestOrder = await _context.Pages
                .Where(p => p.ParentId == page.ParentId && !p.IsOrphan)
                .Select(p => (int?)p.SortOrder)
                .MaxAsync() ?? -1;

            var pageToAdd = new Page
            {
                Title = page.Title,
                Body = _htmlSanitizer.Sanitize(page.Body),
                Date = page.Date,
                ParentId = page.ParentId,
                SortOrder = highestOrder + 1,
                Visible = true,
                IsOrphan = false
            };

            _context.Pages.Add(pageToAdd);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var page = await _context.Pages
                .FirstOrDefaultAsync(p => p.Id == id);

            if (page == null)
            {
                return NotFound();
            }

            var cpvm = new CreatePageViewModel
            {
                Title = page.Title,
                Body = page.Body,
                Date = page.Date,
                ParentId = page.ParentId,
                Visible = page.Visible
            };


            await PopulateParentPageListAsync(id, cpvm.ParentId);

            return View(cpvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreatePageViewModel page)
        {
            var pageToEdit = await _context.Pages
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pageToEdit == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await PopulateParentPageListAsync(id, page.ParentId);

                return View(page);
            }

            var oldParentId = pageToEdit.ParentId;
            var newParentId = page.ParentId;

            // Validate the new parent before making any changes.
            if (newParentId.HasValue)
            {
                if (newParentId.Value == pageToEdit.Id)
                {
                    ModelState.AddModelError(
                        nameof(page.ParentId),
                        "A page cannot be its own parent."
                    );
                }
                else if (await _pageService.IsDescendantAsync(
                    pageToEdit.Id,
                    newParentId.Value))
                {
                    ModelState.AddModelError(
                        nameof(page.ParentId),
                        "A page cannot be moved under one of its descendants."
                    );
                }
                else
                {
                    var parentExists = await _context.Pages
                        .AnyAsync(p =>
                            p.Id == newParentId.Value &&
                            !p.IsOrphan);

                    if (!parentExists)
                    {
                        ModelState.AddModelError(
                            nameof(page.ParentId),
                            "The selected parent page is invalid."
                        );
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                await PopulateParentPageListAsync(id, page.ParentId);

                return View(page);
            }

            // Update normal page properties.
            pageToEdit.Title = page.Title;
            pageToEdit.Body = _htmlSanitizer.Sanitize(page.Body);
            pageToEdit.Date = page.Date;
            pageToEdit.Visible = page.Visible;

            // Parent has changed.
            if (oldParentId != newParentId)
            {
                var highestOrder = await _context.Pages
                    .Where(p =>
                        p.ParentId == newParentId &&
                        !p.IsOrphan &&
                        p.Id != pageToEdit.Id)
                    .Select(p => (int?)p.SortOrder)
                    .MaxAsync() ?? -1;

                pageToEdit.ParentId = newParentId;
                pageToEdit.SortOrder = highestOrder + 1;

                // Assigning a valid parent makes the page non-orphaned.
                pageToEdit.IsOrphan = false;

                await _pageService.ReIndexPagesAsync(oldParentId);
                await _pageService.ReIndexPagesAsync(newParentId);
            }
            else if (pageToEdit.IsOrphan && newParentId.HasValue)
            {
                // Safety net for an orphan whose ParentId is somehow
                // already populated.
                pageToEdit.IsOrphan = false;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Pages
                .AsNoTracking()
                .Include(p => p.Children)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (page == null)
            {
                return NotFound();
            }

            var vm = new PageViewModel
            {
                Id = page.Id,
                Title = page.Title,
                Body = page.Body,
                ParentId = page.ParentId,
                SortOrder = page.SortOrder,

                Children = page.Children
                    .Where(child => !child.IsOrphan)
                    .OrderBy(child => child.SortOrder)
                    .Select(child => new PageViewModel
                    {
                        Id = child.Id,
                        Title = child.Title,
                        Body = child.Body,
                        ParentId = child.ParentId,
                        SortOrder = child.SortOrder
                    })
                    .ToList()
            };

            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var page = await _context.Pages
                .Include(p => p.Children)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (page == null)
            {
                return NotFound();
            }

            var parentId = page.ParentId;

            foreach (var child in page.Children)
            {
                child.ParentId = null;
                child.IsOrphan = true;
            }

            _context.Pages.Remove(page);

            await _context.SaveChangesAsync();

            await _pageService.ReIndexPagesAsync(parentId);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MoveUp(int id)
        {
            var page = await _context.Pages
                .FirstOrDefaultAsync(p => p.Id == id);

            if (page == null)
            {
                return NotFound();
            }

            var previousPage = await _context.Pages
                .Where(p =>
                    p.ParentId == page.ParentId &&
                    !p.IsOrphan &&
                    p.SortOrder < page.SortOrder)
                .OrderByDescending(p => p.SortOrder)
                .FirstOrDefaultAsync();

            if (previousPage != null)
            {
                var tempOrder = page.SortOrder;

                page.SortOrder = previousPage.SortOrder;
                previousPage.SortOrder = tempOrder;

                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MoveDown(int id)
        {
            var page = await _context.Pages
                .FirstOrDefaultAsync(p => p.Id == id);

            if (page == null)
            {
                return NotFound();
            }

            var nextPage = await _context.Pages
                .Where(p =>
                    p.ParentId == page.ParentId &&
                    !p.IsOrphan &&
                    p.SortOrder > page.SortOrder)
                .OrderBy(p => p.SortOrder)
                .FirstOrDefaultAsync();

            if (nextPage != null)
            {
                var tempOrder = page.SortOrder;

                page.SortOrder = nextPage.SortOrder;
                nextPage.SortOrder = tempOrder;

                await _context.SaveChangesAsync();
            }

            return Ok();
        }
    }
}