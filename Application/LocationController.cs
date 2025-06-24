using Core.Interfaces;

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

    public async Task SaveCurrentLocation()
    {
        var location = await _locationService.GetCurrentLocation();

        await _locationRepository.SaveLocation(location);
    }
}
