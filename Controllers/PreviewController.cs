using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppBackend.Models;

namespace WebAppBackend.Controllers
{
    [Authorize]

    public class PreviewController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public PreviewController(IConfiguration configuration, IWebHostEnvironment environment) 
        {
            _configuration = configuration;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var origins = _configuration
                .GetSection("AllowedOrigins")
                .Get<string[]>();

            var basePath = _configuration["BasePath"] ?? "";

            var origin = _environment.IsDevelopment()
                ? origins?.FirstOrDefault(x => x.StartsWith("http://"))
                : origins?.FirstOrDefault();

            var url = $"{origin?.TrimEnd('/')}{basePath.TrimEnd('/')}";

            return View(new Preview
            {
                Url = url
            });
        }
    }
}
