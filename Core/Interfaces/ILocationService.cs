namespace Core.Interfaces;

public interface ILocationService
{
    void StartMonitoringLocation();

    void StopMonitoringLocation();

    Task<ILocationResult> GetCurrentLocation();
}
