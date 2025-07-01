using Application;
using CommunityToolkit.Maui;
using Core.Interfaces;
using DataAccess;
using Infrastructure.Services;
using Microsoft.Extensions.Logging;
using MiniMap.Utils;
using MiniMap.ViewModels;
using MiniMap.Views;

namespace MiniMap
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
               typeof(LocationController).Assembly,
               typeof(ILocation).Assembly));

            //views and VMs
            builder.Services.AddSingleton<MainView, MainViewModel>();
            builder.Services.AddSingletonWithShellRoute<NavigationView, NavigationViewModel>(Routes.NavigationRoute);

            //simple dependencies
            builder.Services.AddSingleton<ILocationService, LocationService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<ILocationFeatureManager, LocationFeatureManager>();

            //initialized dependencies
            var dataDirectory = FileSystem.AppDataDirectory;
            builder.Services.AddSingleton<ILocationRepository>(sp => new LocationRepository(FileSystem.AppDataDirectory));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
