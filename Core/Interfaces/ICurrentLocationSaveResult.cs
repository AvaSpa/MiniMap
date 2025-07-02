using Core.Enums;

namespace Core.Interfaces;

public interface ICurrentLocationSaveResult
{
    LocationStatus Status { get; set; }

    ILocationResult LocationResult { get; set; }
}
