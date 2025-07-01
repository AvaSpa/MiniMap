namespace Core.Interfaces;

public interface ILocationService
{
    Task<ILocationResult> GetCurrentLocation();
}
