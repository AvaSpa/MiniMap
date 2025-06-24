using Core.Interfaces;
using System.Diagnostics;

namespace DataAccess;

public class LocationRepository : ILocationRepository
{
    public async Task SaveLocation(ILocation location)
    {
        Debug.WriteLine($"Saving location: {location.Name}, Latitude: {location.Latitude}, Longitude: {location.Longitude}");

        await Task.Delay(1000);

        Debug.WriteLine($"Saved location: {location.Name}, Latitude: {location.Latitude}, Longitude: {location.Longitude}");
        Debug.WriteLine($"Description: {location.Description}");
    }
}
