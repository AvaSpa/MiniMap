using Core.Interfaces;

namespace Core.Models;

public class Location : ILocation
{
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Description { get; set; }

    public Location(string name, double latitude, double longitude)
    {
        Name = name;
        Latitude = latitude;
        Longitude = longitude;
    }

    public override string ToString()
    {
        return $"{Name} ({Latitude}, {Longitude})";
    }
}
