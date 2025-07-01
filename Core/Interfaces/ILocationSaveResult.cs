using Core.Enums;

namespace Core.Interfaces;

public interface ILocationSaveResult
{
    LocationStatus Status { get; set; }

    ILocationResult LocationResult { get; set; }
}
