using Core.Enums;
using Core.Interfaces;
using Core.Models;

namespace Application;

public class LocationController : ILocationController
{
    private readonly ILocationService _locationService;
    private readonly ILocationRepository _locationRepository;

    public LocationController(ILocationService locationService, ILocationRepository locationRepository)
    {
        _locationService = locationService;
        _locationRepository = locationRepository;
    }

    public async Task SaveLocation(ILocation location)
    {
        await _locationRepository.SaveLocation(location);
    }

    public async Task<ICurrentLocationSaveResult> SaveCurrentLocation()
    {
        var locationResult = await _locationService.GetCurrentLocation();

        if (locationResult.Status == LocationStatus.Success)
            await _locationRepository.SaveLocation(locationResult.Location);

        return new CurrentLocationSaveResult(locationResult.Status, locationResult);
    }

    public async Task<IEnumerable<ILocation>> GetAllLocations(bool withReload)
    {
        return await _locationRepository.GetAllLocations(withReload);
    }
}
