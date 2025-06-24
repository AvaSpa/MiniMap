using Core.Interfaces;
using Core.Models;
using System.Text.Json;

namespace DataAccess;

public class LocationRepository : ILocationRepository
{
    private const string FileName = "locations.json";

    private readonly string _filePath;

    private List<Location> _locations = [];

    public LocationRepository(string directoryPath)
    {
        _filePath = Path.Combine(directoryPath, FileName);
    }

    public async Task<IEnumerable<ILocation>> GetAllLocations(bool withReload)
    {
        if (withReload)
            await LoadLocations();

        return _locations;
    }

    public async Task SaveLocation(ILocation location)
    {
        _locations.Add(new Location(
            location.Name,
            location.Latitude,
            location.Longitude)
        {
            Description = location.Description
        });

        await using var stream = File.Create(_filePath);
        await JsonSerializer.SerializeAsync(stream, _locations, new JsonSerializerOptions { WriteIndented = true });
    }

    private async Task LoadLocations()
    {
        if (!File.Exists(_filePath))
            return;

        await using var stream = File.OpenRead(_filePath);
        _locations = await JsonSerializer.DeserializeAsync<List<Location>>(stream)
            ?? [];
    }
}
