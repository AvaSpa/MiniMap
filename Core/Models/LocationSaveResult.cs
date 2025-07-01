using Core.Enums;
using Core.Interfaces;

namespace Core.Models;

public class LocationSaveResult(LocationStatus status, ILocationResult locationResult) : ILocationSaveResult
{
    public LocationStatus Status { get; set; } = status;
    public ILocationResult LocationResult { get; set; } = locationResult;
}
