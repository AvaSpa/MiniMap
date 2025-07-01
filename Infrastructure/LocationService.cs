using Core.Interfaces;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;

namespace Infrastructure;

/// <summary>
/// TODO: implement live location updates with start, stop methods and changed notification
/// </summary>
public class LocationService : ILocationService
{
    public async Task<ILocation> GetCurrentLocation()
    {
        var errorLocation = new Core.Models.Location("Unknown", 0, 0)
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

                return locationModel;
            }

            return errorLocation;
        }
        catch (FeatureNotEnabledException fnee)
        {
            AppInfo.ShowSettingsUI();

            return errorLocation; //TODO: return other result type; also for the other cases
        }
        catch (Exception e)
        {
            return errorLocation;
        }
    }
}
