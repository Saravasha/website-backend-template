using GoshehArtWebApp.Data;
using GoshehArtWebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoshehArtWebApp.Controllers
{
    public class FontController : Controller

    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AssetTypeProvider _assetTypeProvider;
        private readonly FilePathProvider _filePathProvider;

        public FontController(ApplicationDbContext context, IWebHostEnvironment webHost, AssetTypeProvider assetTypeProvider, FilePathProvider filePathProvider)
        {
            _context = context;
            _webHostEnvironment = webHost;
            _assetTypeProvider = assetTypeProvider;
            _filePathProvider = filePathProvider;
        }

        // GET: FontController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FontController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FontController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FontController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FontController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FontController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FontController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FontController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
