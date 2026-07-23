using Ganss.Xss;
using System.Diagnostics;

namespace WebAppBackend.Services
{
    public interface IHtmlSanitizerService
    {
        string Sanitize(string? html);
    }
    public class HtmlSanitizerService : IHtmlSanitizerService
    {
        private readonly HtmlSanitizer _sanitizer;
        public HtmlSanitizerService()
        {
            _sanitizer = new HtmlSanitizer();

            //
            // Allowed tags
            //

            _sanitizer.AllowedTags.Add("figure");
            _sanitizer.AllowedTags.Add("iframe");
            _sanitizer.AllowedTags.Add("video");
            _sanitizer.AllowedTags.Add("audio");
            _sanitizer.AllowedTags.Add("source");

            //
            // Allowed attributes
            //

            _sanitizer.AllowedAttributes.Add("class");
            _sanitizer.AllowedAttributes.Add("src");
            _sanitizer.AllowedAttributes.Add("href");
            _sanitizer.AllowedAttributes.Add("controls");
            _sanitizer.AllowedAttributes.Add("poster");
            _sanitizer.AllowedAttributes.Add("download");
            _sanitizer.AllowedAttributes.Add("target");
            _sanitizer.AllowedAttributes.Add("data-asset-id");
            _sanitizer.AllowedAttributes.Add("contenteditable");
            _sanitizer.AllowedAttributes.Add("frameborder");
            _sanitizer.AllowedAttributes.Add("loading");
            _sanitizer.AllowedAttributes.Add("allowfullscreen");
            _sanitizer.AllowedAttributes.Add("width");
            _sanitizer.AllowedAttributes.Add("height");

            //Allow Schemes
            _sanitizer.AllowedSchemes.Add("http");
            _sanitizer.AllowedSchemes.Add("https");
            _sanitizer.AllowedSchemes.Add("data");
            

            // Allow relative URLs
            _sanitizer.AllowDataAttributes = true;
        }
        public string Sanitize(string? html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return string.Empty;

            Console.WriteLine("===== Sanitize was called =====");

            return _sanitizer.Sanitize(html);
        }
    }
}
