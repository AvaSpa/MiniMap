using MediatR;

namespace Core.Notifications;

public class DirectionDeltaChangedNotification(double delta) : INotification
{
    public double Delta { get; } = delta;
}
