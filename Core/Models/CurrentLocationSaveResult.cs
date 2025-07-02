using Core.Enums;
using Core.Interfaces;

namespace Core.Models;

public class CurrentLocationSaveResult(LocationStatus status, ILocationResult locationResult) : ICurrentLocationSaveResult
{
    public LocationStatus Status { get; set; } = status;
    public ILocationResult LocationResult { get; set; } = locationResult;
}
