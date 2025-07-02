using CommunityToolkit.Mvvm.Messaging;
using Core.Notifications;
using MediatR;
using MiniMap.Messages;

namespace MiniMap.NotificationHandlers;

public class DirectionDeltaChangedNotificationHandler : INotificationHandler<DirectionDeltaChangedNotification>
{
    public Task Handle(DirectionDeltaChangedNotification notification, CancellationToken cancellationToken)
    {
        var message = new DirectionDeltaChangedMessage(notification.Delta, notification.CurrentHeading.North, notification.CurrentLocation);
        WeakReferenceMessenger.Default.Send(message);

        return Task.CompletedTask;
    }
}
