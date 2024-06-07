using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WallpaperApp
{
    public class VideoFetcher
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<List<string>> FetchVideoUrlsAsync(string url)
        {
            var videoUrls = new List<string>();
            var html = await client.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var videoPageLinks = htmlDocument.DocumentNode.SelectNodes("//div[@class='masonry-item']//a[@href]");

            if (videoPageLinks != null)
            {
                var videoPageUrls = videoPageLinks.Select(node => node.GetAttributeValue("href", string.Empty)).ToList();

                for (int i = 0; i < videoPageUrls.Count; i++)
                {
                    if (!videoPageUrls[i].StartsWith("http"))
                    {
                        videoPageUrls[i] = new Uri(new Uri(url), videoPageUrls[i]).ToString();
                    }

                    // Obtener la URL del video desde la página individual
                    var videoUrl = await GetVideoUrlFromPageAsync(videoPageUrls[i]);
                    if (!string.IsNullOrEmpty(videoUrl))
                    {
                        videoUrls.Add(videoUrl);
                    }
                }
            }

            return videoUrls;
        }

        private async Task<string> GetVideoUrlFromPageAsync(string pageUrl)
        {
            var html = await client.GetStringAsync(pageUrl);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var videoNode = htmlDocument.DocumentNode.SelectSingleNode("//video[@class='video-background']//source[@type='video/mp4']");
            if (videoNode != null)
            {
                return videoNode.GetAttributeValue("src", string.Empty);
            }

            return string.Empty;
        }
    }
}
