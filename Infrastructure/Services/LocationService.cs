using Core.Enums;
using Core.Interfaces;
using Core.Models;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;

namespace Infrastructure.Services;

/// <summary>
/// TODO: implement live location updates with start, stop methods and changed notification
/// </summary>
public class LocationService : ILocationService
{
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
}
