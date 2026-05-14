using System;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SalesManagementSystem.Services
{
    public interface IImageService
    {
        Task<Image> GetImageAsync(string imageUrl);
    }

    public class ImageService : IImageService
    {
        private static readonly HttpClient _httpClient;

        static ImageService()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            _httpClient = new HttpClient(handler);
            _httpClient.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/125.0.0.0 Safari/537.36");
            _httpClient.DefaultRequestHeaders.Add("Accept",
                "image/avif,image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9,ro;q=0.8");
            _httpClient.DefaultRequestHeaders.Add("Referer", "https://www.setandglow.ro/");
            _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "image");
            _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "no-cors");
            _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "cross-site");
        }

        public async Task<Image> GetImageAsync(string imageUrl)
        {
            string fullImageUrl = ResolveImageUrl(imageUrl);
            if (string.IsNullOrEmpty(fullImageUrl))
                return null;

            try
            {
                var response = await _httpClient.GetAsync(fullImageUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
                using (var ms = new System.IO.MemoryStream(imageBytes))
                {
                    return new Bitmap(Image.FromStream(ms));
                }
            }
            catch
            {
                return null;
            }
        }

        private string ResolveImageUrl(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return "";

            int semiIdx = imageUrl.IndexOf(';');
            if (semiIdx >= 0) imageUrl = imageUrl.Substring(0, semiIdx);

            string trimmed = imageUrl.Trim();

            if (trimmed.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                trimmed.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                return trimmed;
            }

            if (trimmed.StartsWith("wix:image://", StringComparison.OrdinalIgnoreCase))
            {
                string path = trimmed.Substring("wix:image://".Length);
                if (path.StartsWith("v1/", StringComparison.OrdinalIgnoreCase))
                    path = path.Substring(3);

                int hashIdx = path.IndexOf('#');
                if (hashIdx >= 0)
                    path = path.Substring(0, hashIdx);

                string[] parts = path.Split('/');
                string hash = parts[0];

                return "https://static.wixstatic.com/media/" + hash;
            }

            int fragIdx = trimmed.IndexOf('#');
            if (fragIdx >= 0)
                trimmed = trimmed.Substring(0, fragIdx);

            return "https://static.wixstatic.com/media/" + trimmed;
        }
    }
}
