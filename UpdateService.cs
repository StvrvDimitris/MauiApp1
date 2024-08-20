using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class UpdateService
    {
        private readonly HttpClient _httpClient;
        private const string MsixDownloadUrl = "https://stvrvdimitris.github.io/MauiApp1/installer.msix";
        private const string DownloadPath = @"C:\Users\admin\\Downloads\installer.msix"; // Change the path if needed

        public UpdateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DownloadMsixPackageAsync()
        {
            var response = await _httpClient.GetAsync(MsixDownloadUrl);

            if (response.IsSuccessStatusCode)
            {
                var fileBytes = await response.Content.ReadAsByteArrayAsync();
                await File.WriteAllBytesAsync(DownloadPath, fileBytes);
            }
            else
            {
                // Handle error
                throw new Exception("Failed to download .msix package.");
            }
        }
    }
}
