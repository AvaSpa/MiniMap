namespace Core.Interfaces;

public interface ILocation
{
    string Name { get; set; }
    double Latitude { get; set; }
    double Longitude { get; set; }
    string? Description { get; set; }
}
