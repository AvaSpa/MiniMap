namespace Core.Interfaces;

public interface INavigationService
{
    void StartMonitoringCompass();

    void StopMonitoringCompass();

    IHeading GetDirectionToLocation(ILocation location, ILocation currentLocation);
}
