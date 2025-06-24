using Application;
using CommunityToolkit.Maui;
using Core.Interfaces;
using DataAccess;
using Infrastructure;
using Microsoft.Extensions.Logging;
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
                typeof(ILocation).Assembly
            ));

            builder.Services.AddSingleton<MainView, MainViewModel>();

            builder.Services.AddSingleton<ILocationService, LocationService>();
            builder.Services.AddSingleton<ILocationRepository, LocationRepository>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
