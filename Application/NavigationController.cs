using Core.Interfaces;

namespace Application;

/// <summary>
/// TODO: implement command handler for start navigation and stop navigation and for getting the direction to a location
/// </summary>
public class NavigationController
{
    private readonly INavigationService _navigationService;
    private readonly ILocationService _locationService;

    public NavigationController(INavigationService navigationService, ILocationService locationService)
    {
        _navigationService = navigationService;
        _locationService = locationService;
    }

    /// <summary>
    /// TODO: turn this into the appropriate handler method
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    private async Task<IHeading> GetDirectionToLocation(ILocation location)
    {
        var currentLocation = await _locationService.GetCurrentLocation();
        return _navigationService.GetDirectionToLocation(location, currentLocation);
    }
}
