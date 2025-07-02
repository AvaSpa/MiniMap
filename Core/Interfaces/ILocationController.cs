namespace Core.Interfaces;

public interface ILocationController
{
    Task SaveLocation(ILocation location);
    Task<ICurrentLocationSaveResult> SaveCurrentLocation();
    Task<IEnumerable<ILocation>> GetAllLocations(bool withReload);
}
