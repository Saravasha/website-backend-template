using WebAppBackend.Models;

namespace WebAppBackend.Services
{
    public class AssetTypeProvider
    {
        public AssetType GetType(string input)
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
                    _ => AssetType.Other
                };
            }

            // Handle MIME types
            if (input.StartsWith("image/")) return AssetType.Image;
            if (input.StartsWith("video/")) return AssetType.Video;
            if (input.StartsWith("audio/")) return AssetType.Audio;

            return AssetType.Other;
        }
    }
}
