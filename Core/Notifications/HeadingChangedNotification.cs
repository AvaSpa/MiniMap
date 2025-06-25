using Core.Interfaces;
using MediatR;

namespace Core.Notifications;

public class HeadingChangedNotification(IHeading heading) : INotification
{
    public IHeading Heading { get; set; } = heading;
}
