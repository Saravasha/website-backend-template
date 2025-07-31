using Microsoft.EntityFrameworkCore;
using WebAppBackend.Data;

public class OrphanAnnihilator
{
    private readonly ApplicationDbContext _context;
    private readonly FilePathProvider _filePathProvider;

    public OrphanAnnihilator(ApplicationDbContext context, FilePathProvider filePathProvider)
    {
        _context = context;
        _filePathProvider = filePathProvider;
    }

    public async Task RemoveUnusedFilesAsync()
    {
        Console.WriteLine("Starting cleanup of unused uploaded files...");

        var uploadsRoot = _filePathProvider.UploadsRoot;

        // Continue with your cleanup logic, using `uploadsRoot`...

        // 1. Gather all referenced file names
        var assetFiles = await _context.Assets
            .Where(a => !string.IsNullOrWhiteSpace(a.FileUrl))
            .Select(a => Path.GetFileName(a.FileUrl))
            .ToListAsync();

        var assetThumbnails = await _context.Assets
            .Where(a => !string.IsNullOrWhiteSpace(a.ThumbnailUrl))
            .Select(a => Path.GetFileName(a.ThumbnailUrl))
            .ToListAsync();

        var pageContainers = await _context.Pages
            .Where(p => !string.IsNullOrWhiteSpace(p.Container))
            .Select(p => p.Container!)
            .ToListAsync();

        var contentContainers = await _context.Contents
            .Where(c => !string.IsNullOrWhiteSpace(c.Container))
            .Select(c => c.Container!)
            .ToListAsync();

        var containerFiles = pageContainers
            .Concat(contentContainers)
            .SelectMany(ExtractFileNamesFromHtml)
            .ToList();

        var referencedFileNames = assetFiles
            .Concat(assetThumbnails)
            .Concat(containerFiles)
            .Where(fn => !string.IsNullOrWhiteSpace(fn))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        // 2. Clean all under UploadsRoot (recursively)
        var files = Directory.GetFiles(_filePathProvider.UploadsRoot, "*.*", SearchOption.AllDirectories);

        foreach (var filePath in files)
        {
            var fileName = Path.GetFileName(filePath);
            if (!referencedFileNames.Contains(fileName))
            {
                try
                {
                    File.Delete(filePath);
                    Console.WriteLine($"[Deleted] {filePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Error] Failed to delete {filePath}: {ex.Message}");
                }
            }
        }

        Console.WriteLine("Cleanup complete.");
    }

    private List<string> ExtractFileNamesFromHtml(string html)
    {
        var fileNames = new List<string>();
        if (string.IsNullOrEmpty(html)) return fileNames;

        var doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(html);

        var imgNodes = doc.DocumentNode.SelectNodes("//img[@src]");
        if (imgNodes != null)
        {
            foreach (var img in imgNodes)
            {
                var src = img.GetAttributeValue("src", null);
                if (!string.IsNullOrEmpty(src))
                    fileNames.Add(Path.GetFileName(src));
            }
        }

        var aNodes = doc.DocumentNode.SelectNodes("//a[@href]");
        if (aNodes != null)
        {
            foreach (var a in aNodes)
            {
                var href = a.GetAttributeValue("href", null);
                if (!string.IsNullOrEmpty(href))
                    fileNames.Add(Path.GetFileName(href));
            }
        }

        return fileNames;
    }

    //Implementation 
    

    //var fileCleaner = new OrphanAnnihilator(_context, _filePathProvider);
    //await fileCleaner.RemoveUnusedFilesAsync();
}

