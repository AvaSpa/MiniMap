using Core.Interfaces;
using Core.Models;

namespace Infrastructure;

public class LocationService : ILocationService
{
    public async Task<ILocation> GetCurrentLocation()
    {
        var currentLocation = new Location("current", 2, 3);

        return currentLocation;
    }
}
