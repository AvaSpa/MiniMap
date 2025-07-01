using Core.Enums;
using Core.Interfaces;

namespace Core.Models;

public class LocationResult(LocationStatus status, ILocation location) : ILocationResult
{
    public LocationStatus Status { get; set; } = status;
    public ILocation Location { get; set; } = location;
}
