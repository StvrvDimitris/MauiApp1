using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class VersionService
    {
        private readonly HttpClient _httpClient;
        private const string VersionUrl = "https://stvrvdimitris.github.io/MauiApp1/TextFile1.txt";
        private const string CurrentVersion = "0.9.0.0"; // Update this to your app's current version

        public VersionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetLatestVersionAsync()
        {
            var response = await _httpClient.GetStringAsync(VersionUrl);
            return response.Trim(); // Return the version string without extra whitespace
        }

        public bool IsUpdateAvailable(string latestVersion)
        {
            // Simple version comparison (e.g., "1.0.0" < "1.0.1")
            return string.Compare(CurrentVersion, latestVersion) < 0;
        }
    }
}
