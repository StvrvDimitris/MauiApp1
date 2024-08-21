namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private readonly VersionService _versionService;
        private readonly UpdateService _updateService;
        private readonly InstallationService _installationService;

        public MainPage(VersionService versionService, UpdateService updateService, InstallationService installationService)
        {
            InitializeComponent();
            _versionService = versionService;
            _updateService = updateService;
            _installationService = installationService;
            CheckForUpdates();
        }
        private async void CheckForUpdates()
        {
            var latestVersion = await _versionService.GetLatestVersionAsync();
            if (_versionService.IsUpdateAvailable(latestVersion))
            {
                try
                {
                    await _updateService.DownloadMsixPackageAsync();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }
    }
}
