using GoshehArtWebApp.Data;
using GoshehArtWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoshehArtWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CleanupController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly FilePathProvider _filePathProvider;

        public CleanupController(ApplicationDbContext context, FilePathProvider filePathProvider)
        {
            _context = context;
            _filePathProvider = filePathProvider;
        }

        [HttpPost("cleanup-orphans")]
        public async Task<IActionResult> CleanupOrphanFiles()
        {
            var cleanup = new OrphanAnnihilator(_context, _filePathProvider);
            await cleanup.RemoveUnusedFilesAsync();
            TempData["CleanupSuccess"] = "Unused files successfully cleaned up.";
            var referer = Request.Headers["Referer"].ToString();
            if (Uri.TryCreate(referer, UriKind.Absolute, out var refererUri) &&
    refererUri.Host == Request.Host.Host)
            {
                return Redirect(referer);
            }

            // Fallback if no referer is available
            return RedirectToAction("Index", "Home");
        }

    }
}
