namespace Core.Interfaces;

public interface ILocationService
{
    Task StartMonitoringLocation();

    void StopMonitoringLocation();

    Task<ILocationResult> GetCurrentLocation();
}
