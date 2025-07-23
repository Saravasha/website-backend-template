public class FilePathProvider
{
    private readonly IWebHostEnvironment _env;

    public FilePathProvider(IWebHostEnvironment env)
    {
        _env = env;
    }
    
    //for staging 
    public string RobotsTxtPath => Path.Combine(_env.WebRootPath ?? "wwwroot", "robots.txt");
    public string UploadsRoot => Path.Combine(Directory.GetParent(Environment.CurrentDirectory)!.FullName, "Uploads");

    public string ThumbnailsRoot => Path.Combine(UploadsRoot, "Thumbnails");

    public string WebAssetsRoot => Path.Combine(_env.WebRootPath, "Assets");

    public string ToWebPath(string fullPath)
    {
        var uploadsRoot = Path.GetFullPath(UploadsRoot) + Path.DirectorySeparatorChar;
        var webRoot = Path.GetFullPath(_env.WebRootPath) + Path.DirectorySeparatorChar;

        if (fullPath.StartsWith(uploadsRoot, StringComparison.OrdinalIgnoreCase))
        {
            var relativePath = fullPath.Substring(uploadsRoot.Length);
            return "/Uploads/" + relativePath.Replace("\\", "/");
        }

        if (fullPath.StartsWith(webRoot, StringComparison.OrdinalIgnoreCase))
        {
            var relativePath = fullPath.Substring(webRoot.Length);
            return "/" + relativePath.Replace("\\", "/");
        }

        // Debug fallback
        return "/unmapped/" + fullPath.Replace("\\", "/");
    }

    public string GetFullPath(string webPath)
    {
        if (string.IsNullOrEmpty(webPath))
            throw new ArgumentException("Path cannot be null or empty", nameof(webPath));

        // Normalize slashes
        webPath = webPath.Replace('\\', '/');

        // Expecting webPath like "/Uploads/filename.ext"
        const string uploadsPrefix = "/Uploads/";

        if (!webPath.StartsWith(uploadsPrefix, StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException($"Invalid path: must start with {uploadsPrefix}", nameof(webPath));

        // Strip "/Uploads/" prefix to get relative path
        var relativePath = webPath.Substring(uploadsPrefix.Length);

        // Combine with UploadsRoot, making sure to use system directory separators
        var fullPath = Path.Combine(UploadsRoot, relativePath.Replace('/', Path.DirectorySeparatorChar));

        return fullPath;
    }
}
