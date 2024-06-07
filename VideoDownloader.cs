using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WallpaperApp
{
    public class VideoDownloader
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> DownloadRandomVideoAsync(List<string> videoUrls, string downloadFolder)
        {
            var random = new Random();
            var randomIndex = random.Next(videoUrls.Count);
            var videoUrl = videoUrls[randomIndex];

            var videoData = await client.GetByteArrayAsync(videoUrl);  // Asegúrate de que esto devuelve un Task<byte[]>
            var videoPath = Path.Combine(downloadFolder, $"video_{randomIndex}.mp4");

            await File.WriteAllBytesAsync(videoPath, videoData);  // Usa File.WriteAllBytesAsync para operaciones asincrónicas

            return videoPath;
        }
    }
}
