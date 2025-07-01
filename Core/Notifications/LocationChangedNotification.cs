using Core.Interfaces;
using MediatR;

namespace Core.Notifications;

public class LocationChangedNotification(ILocation location) : INotification
{
    public ILocation Location { get; set; } = location;
}
