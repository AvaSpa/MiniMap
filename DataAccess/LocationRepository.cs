using Core.Interfaces;

namespace DataAccess;

public class LocationRepository : ILocationRepository
{
    public async Task SaveLocation(ILocation location)
    {
        await Task.Delay(1000);
    }
}
