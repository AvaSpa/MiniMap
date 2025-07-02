using Core.Interfaces;

namespace Application;

public class NavigationController : INavigationController
{
    private IHeading _heading;
    private ILocation _location;
    private ILocation _destination;

    private readonly INavigationService _navigationService;
    private readonly ILocationService _locationService;

    public ILocation CurrentLocation => _location;

    public IHeading CurrentHeading => _heading;

    public NavigationController(INavigationService navigationService, ILocationService locationService)
    {
        _navigationService = navigationService;
        _locationService = locationService;
    }

    public int GetDirectionDelta()
    {
        if (_heading == null || _location == null || _destination == null)
            return -1;

        var directionToLocation = _navigationService.GetDirectionToLocation(_destination, _location);

        return (directionToLocation.North - _heading.North + 360) % 360;
    }

    public void UpdateHeading(IHeading heading)
    {
        _heading = heading;
    }

    public void UpdateLocation(ILocation location)
    {
        _location = location;
    }

    public void SetDestination(ILocation destination)
    {
        _destination = destination;
    }

    public async Task StartNavigation()
    {
        _navigationService.StartMonitoringCompass();
        await _locationService.StartMonitoringLocation();
    }

    public void StopNavigation()
    {
        _navigationService.StopMonitoringCompass();
        _locationService.StopMonitoringLocation();
    }

    public ILocation GetDestination()
    {
        return _destination;
    }
}
