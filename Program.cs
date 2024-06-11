using System;
using System.IO;
using System.Threading.Tasks;
using WallpaperApp;

class Program
{
    static async Task Main(string[] args)
    {
        var videoFetcher = new VideoFetcher();
        var videoDownloader = new VideoDownloader();
        var wallpaperSetter = new WallpaperSetter();

        var videoUrls = await videoFetcher.FetchVideoUrlsAsync("https://www.desktophut.com/category/anime-live-wallpapers");

        if (videoUrls.Count > 0)
        {
            string downloadFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "DownloadedVideos");
            //delete files in folder
            if (Directory.Exists(downloadFolder))
            {
                Directory.Delete(downloadFolder, true);
            }

            Directory.CreateDirectory(downloadFolder);

            var videoPath = await videoDownloader.DownloadRandomVideoAsync(videoUrls, downloadFolder);
            wallpaperSetter.SetVideoAsWallpaper(videoPath);

            Console.WriteLine("Video set as wallpaper.");
        }
        else
        {
            Console.WriteLine("No videos found.");
        }
    }
}
