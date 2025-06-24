using Core.Interfaces;
using Core.Models;
using System.Diagnostics;

namespace Infrastructure;

public class LocationService : ILocationService
{
    public async Task<ILocation> GetCurrentLocation()
    {
        Debug.WriteLine("Getting current location...");

        await Task.Delay(500);

        var currentLocation = CreateLocation("current", 2, 3);

        Debug.WriteLine($"Current location: {currentLocation.Name}, Latitude: {currentLocation.Latitude}, Longitude: {currentLocation.Longitude}");

        return currentLocation;
    }

    /// <summary>
    /// Mock. Remove.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    /// <returns></returns>
    private ILocation CreateLocation(string name, double latitude, double longitude)
    {
        return new Location(name, latitude, longitude)
        {
            Description = "This is a sample location description."
        };
    }
}
