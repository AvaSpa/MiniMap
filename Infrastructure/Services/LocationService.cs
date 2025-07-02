using Core.Enums;
using Core.Interfaces;
using Core.Models;
using Core.Notifications;
using MediatR;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;

namespace Infrastructure.Services;

public class LocationService : ILocationService
{
    private readonly IMediator _mediator;

    public LocationService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task StartMonitoringLocation()
    {
        if (Geolocation.Default.IsListeningForeground)
            return;

        var request = new GeolocationListeningRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(10));

        Geolocation.Default.LocationChanged += OnLocationChanged;

        await Geolocation.Default.StartListeningForegroundAsync(request);
    }

    public void StopMonitoringLocation()
    {
        if (!Geolocation.Default.IsListeningForeground)
            return;

        Geolocation.Default.LocationChanged -= OnLocationChanged;
        Geolocation.Default.StopListeningForeground();
    }

    public async Task<ILocationResult> GetCurrentLocation()
    {
        var errorLocation = new Core.Models.Location("Error", 0, 0)
        {
            Description = "Unable to retrieve current location."
        };

        try
        {
            var request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(2));

            var location = await Geolocation.Default.GetLocationAsync(request);

            if (location != null)
            {
                var locationModel = new Core.Models.Location("Current Location", location.Latitude, location.Longitude)
                {
                    Description = $"Current location at {DateTime.Now:yyyy-MM-dd HH:mm:ss}"
                };

                return new LocationResult(LocationStatus.Success, locationModel);
            }

            return new LocationResult(LocationStatus.UnknownError, errorLocation);
        }
        catch (FeatureNotEnabledException)
        {
            return new LocationResult(LocationStatus.LocationDisabled, errorLocation);
        }
        catch (PermissionException)
        {
            return new LocationResult(LocationStatus.PermissionDenied, errorLocation);
        }
        catch (FeatureNotSupportedException)
        {
            return new LocationResult(LocationStatus.LocationUnavailable, errorLocation);
        }
        catch (Exception)
        {
            return new LocationResult(LocationStatus.UnknownError, errorLocation);
        }
    }

    private void OnLocationChanged(object? sender, GeolocationLocationChangedEventArgs e)
    {
        var location = new Core.Models.Location("Moving Location", e.Location.Latitude, e.Location.Longitude);
        _mediator.Publish(new LocationChangedNotification(location));
    }
}
