using System.Diagnostics;

namespace GoshehArtWebApp.Services
{
    public class VideoThumbnailProvider
    {

        private readonly FilePathProvider _filePathProvider;

        public VideoThumbnailProvider(FilePathProvider filePathProvider)
        {
            _filePathProvider = filePathProvider;
        }

        public async Task<string?> GenerateAsync(string videoPath, string videoFileName)
        {
            if (!Directory.Exists(_filePathProvider.ThumbnailsRoot))
            {
                Directory.CreateDirectory(_filePathProvider.ThumbnailsRoot);
            }

            string thumbnailFileName = Path.GetFileNameWithoutExtension(videoFileName) + "_thumb.jpg";
            string thumbnailPath = Path.Combine(_filePathProvider.ThumbnailsRoot, thumbnailFileName);

            var ffmpegArgs = $"-i \"{videoPath}\" -ss 00:00:01 -vframes 1 -q:v 2 \"{thumbnailPath}\"";

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "ffmpeg",
                    Arguments = ffmpegArgs,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            try
            {
                process.Start();
                string stderr = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();

                if (process.ExitCode != 0 || !System.IO.File.Exists(thumbnailPath))
                {
                    // Log error somewhere persistent in production
                    Console.WriteLine("FFmpeg Error:\n" + stderr);
                    return null;
                }

                return _filePathProvider.ToWebPath(thumbnailPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("FFmpeg failed: " + ex.Message);
                return null;
            }
        }
    }
}
