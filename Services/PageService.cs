using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppBackend.Data;
using WebAppBackend.Models;
using WebAppBackend.ViewModels.Pages;

namespace WebAppBackend.Services
{
    public class PageService
    {
        private readonly ApplicationDbContext _context;

        public PageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ReIndexPagesAsync(int? parentId)
        {
            var pages = await _context.Pages
                .Where(p => p.ParentId == parentId && !p.IsOrphan)
                .OrderBy(p => p.SortOrder)
                .ToListAsync();

            for (var i = 0; i < pages.Count; i++)
            {
                pages[i].SortOrder = i;
            }
        }

        public PageViewModel BuildPageViewModel(Page page, List<Page> pages, int index = 0, int siblingCount = 1, bool includeOrphans = false)
        {
            var children = pages
                .Where(p =>
                    p.ParentId == page.Id &&
                    (includeOrphans || !p.IsOrphan))
                .OrderBy(p => p.SortOrder)
                .ToList();

            return new PageViewModel
            {
                Id = page.Id,
                Title = page.Title,
                Body = page.Body,
                ParentId = page.ParentId,
                Date = page.Date,
                SortOrder = page.SortOrder,

                IsOrphan = page.IsOrphan,
                Visible = page.Visible,

                CanMoveUp = siblingCount > 1 && index > 0,
                CanMoveDown = siblingCount > 1 && index < siblingCount - 1,

                Children = children
                    .Select((child, childIndex) =>
                        BuildPageViewModel(
                            child,
                            pages,
                            childIndex,
                            children.Count,
                            includeOrphans))
                    .ToList()
            };
        }

        public List<SelectListItem> BuildParentPageList(IEnumerable<Page> pages, int? selectedParentId = null, int? excludedPageId = null)
        {
            var pageList = pages
                .Where(p => !p.IsOrphan && p.Id != excludedPageId)
                .ToList();

            var result = new List<SelectListItem>();

            void AddChildren(int? parentId, int depth)
            {
                var children = pageList
                    .Where(p => p.ParentId == parentId)
                    .OrderBy(p => p.SortOrder);

                foreach (var page in children)
                {
                    result.Add(new SelectListItem
                    {
                        Value = page.Id.ToString(),
                        Text = $"{new string('\u00A0', depth * 4)}{page.Title}",
                        Selected = page.Id == selectedParentId
                    });

                    AddChildren(page.Id, depth + 1);
                }
            }

            AddChildren(null, 0);

            return result;
        }
        public async Task<bool> IsDescendantAsync(int pageId, int potentialDescendantId)
        {
            var current = await _context.Pages
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == potentialDescendantId);

            while (current?.ParentId != null)
            {
                if (current.ParentId == pageId)
                {
                    return true;
                }

                current = await _context.Pages
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == current.ParentId);
            }

            return false;
        }

    }
}
