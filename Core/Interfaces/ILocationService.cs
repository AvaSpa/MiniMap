namespace Core.Interfaces;

public interface ILocationService
{
    Task<ILocation> GetCurrentLocation();
}
