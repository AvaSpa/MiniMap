namespace Core.Interfaces;

public interface ILocationRepository
{
    Task SaveLocation(ILocation location);

    Task<IEnumerable<ILocation>> GetAllLocations(bool withReload);
}
