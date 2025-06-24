namespace Core.Interfaces;

public interface ILocationRepository
{
    Task SaveLocation(ILocation location);
}
