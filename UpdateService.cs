using System;
using System.Collections.Generic;
using System.Diagnostics;
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

                StartInstallerUtility(DownloadPath);
            }
            else
            {
                // Handle error
                throw new Exception("Failed to download .msix package.");
            }
        }
        private void StartInstallerUtility(string packagePath)
        {
            var installerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "publish\\InstallerUtiliry.exe");

            var processStartInfo = new ProcessStartInfo
            {
                FileName = installerPath,
                Arguments = $"\"{packagePath}\"",
                UseShellExecute = true,
                Verb="runas",
                WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory // Set the working directory explicitly
            };

            Process.Start(processStartInfo);

            // Exit the application
            Environment.Exit(0);
        }
    }
}
