using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class InstallationService
    {
        private const string MsixPath = @"C:\Users\admin\Downloads\installer.msix"; // Must match the path used in DownloadMsixPackageAsync

        public async Task InstallMsixPackageAsync()
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = $"-Command \"Add-AppxPackage -Path '{MsixPath}'\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(processStartInfo);
            if (process != null)
            {
                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();

                if (process.ExitCode != 0)
                {
                    // Handle error
                    throw new Exception($"Failed to install .msix package. Error: {error}");
                }
            }
        }
    }
}
