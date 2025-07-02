using Core.Interfaces;
using MediatR;

namespace Core.Notifications;

public class DirectionDeltaChangedNotification(int delta, ILocation currentLocation, IHeading currentHeading) : INotification
{
    public int Delta { get; } = delta;
    public ILocation CurrentLocation { get; } = currentLocation;
    public IHeading CurrentHeading { get; } = currentHeading;
}
