namespace Core.Interfaces;

public interface ILocationController
{
    Task SaveLocation(ILocation location);
    Task SaveCurrentLocation();
}
