using WebAppBackend.Models;

namespace WebAppBackend.Services
{
    public class AssetTypeProvider
    {
        public AssetType GetAssetType(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return AssetType.Other;

            input = input.ToLowerInvariant();

            // Handle file extensions
            if (input.StartsWith("."))
            {
                return input switch
                {
                    ".jpg" or ".jpeg" or ".png" or ".gif" or ".bmp" or ".webp" => AssetType.Image,
                    ".mp4" or ".mov" or ".avi" or ".wmv" => AssetType.Video,
                    ".mp3" or ".wav" or ".ogg" => AssetType.Audio,
                    ".pdf" or ".doc" or ".docx" => AssetType.Document,
                    ".txt" or ".md" => AssetType.Text,
                    _ => AssetType.Other
                };
            }

            // Handle MIME types
            if (input.StartsWith("image/")) return AssetType.Image;
            if (input.StartsWith("video/")) return AssetType.Video;
            if (input.StartsWith("audio/")) return AssetType.Audio;
            if (input == "application/pdf" ||
               input == "application/msword" ||
               input == "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                return AssetType.Document;
            if (input.StartsWith("text/")) return AssetType.Text;

            return AssetType.Other;
        }
    }
}
