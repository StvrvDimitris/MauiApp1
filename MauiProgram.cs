﻿using Microsoft.Extensions.Logging;


namespace MauiApp1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddHttpClient<VersionService>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddHttpClient<UpdateService>(); // Register UpdateService with HttpClient
            builder.Services.AddTransient<UpdateService>(); // Register UpdateService
            builder.Services.AddTransient<InstallationService>(); // Register InstallationService
            builder.Services.AddTransient<MainPage>();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
