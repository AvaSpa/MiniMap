using Core.Enums;

namespace Core.Interfaces;

public interface ILocationResult
{
    LocationStatus Status { get; set; }

    ILocation Location { get; set; }
}
