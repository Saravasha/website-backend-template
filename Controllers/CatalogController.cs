using GoshehArtWebApp.Data;
using GoshehArtWebApp.Models;
using GoshehArtWebApp.Services;
using GoshehArtWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoshehArtWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CatalogController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly AssetTypeProvider _assetTypeProvider;
        private readonly FilePathProvider _filePathProvider;

        public CatalogController(ApplicationDbContext context, IWebHostEnvironment webHost, AssetTypeProvider assetTypeProvider, FilePathProvider filePathProvider)
        {
            _context = context;
            webHostEnvironment = webHost;
            _assetTypeProvider = assetTypeProvider;
            _filePathProvider = filePathProvider;
        }

        // GET: CatalogController]
        public async Task<IActionResult> Seed()
        {
            List<string> localFolders = new List<string>();
            foreach (var f in Directory.GetDirectories(_filePathProvider.WebAssetsRoot, "*"))
            {
                var dir = new DirectoryInfo(f).Name;
                localFolders.Add(dir);
            }

            foreach (var file in localFolders)
            {
                if (!_context.Categories.Any(x => x.Name == file))
                {
                    _context.Categories.Update(new Category() { Name = file });
                }
            }
            await _context.SaveChangesAsync();

            await FoldersContentGetter(_filePathProvider, localFolders);
            return RedirectToAction("Index", "Asset");
        }
        private async Task FoldersContentGetter(FilePathProvider filePathProvider, List<string> localFolders)
        {
            CatalogViewModel cvm = new CatalogViewModel();
            foreach (var folder in localFolders)
            {
                Category catToAdd = new Category();
                foreach (var item in Directory.GetFiles(Path.Combine(filePathProvider.WebAssetsRoot, folder)))
                {
                    var fileInDirectory = new FileInfo(item).Name;
                    var assetName = _context.Assets.FirstOrDefault(n => n.Name == fileInDirectory);
                    if (!_context.Assets.Any(x => x.Name == fileInDirectory))
                    {

                        var fullPath = Path.Combine(filePathProvider.WebAssetsRoot, folder, fileInDirectory);
                        var webPath = filePathProvider.ToWebPath(fullPath);

                        var assetType = _assetTypeProvider.GetType(Path.GetExtension(fileInDirectory));

                        Models.Asset assetStage = new Models.Asset()
                        {
                            Name = fileInDirectory,
                            Description = folder,
                            Author = User.Identity.Name,
                            FileUrl = webPath,
                            Type = assetType

                        };
                        var category = _context.Categories.FirstOrDefault(c => c.Name == folder);
                        if (category != null)
                        {
                            assetStage.Categories.Add(category);
                        }
                        _context.Assets.Add(assetStage);
                    }
                }

            }
            await _context.SaveChangesAsync();
        }
    }
}