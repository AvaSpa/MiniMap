using Core.Commands;
using Core.Interfaces;
using Core.Notifications;
using MediatR;

namespace Application;

/// <summary>
/// TODO: implement command handler for start navigation and stop navigation
/// in start stop navigation call navigation and location services start and stop methods
/// </summary>
public class NavigationController : INotificationHandler<HeadingChangedNotification>,
    INotificationHandler<LocationChangedNotification>,
    IRequestHandler<SetDestinationCommand>
{
    private IHeading _heading;
    private ILocation _location;
    private ILocation _destination;

    private readonly IMediator _mediator;
    private readonly INavigationService _navigationService;

    public NavigationController(IMediator mediator, INavigationService navigationService)
    {
        _mediator = mediator;
        _navigationService = navigationService;
    }

    public async Task Handle(HeadingChangedNotification notification, CancellationToken cancellationToken)
    {
        _heading = notification.Heading;

        var directionDelta = GetDirectionDelta();
        await _mediator.Publish(new DirectionDeltaChangedNotification(directionDelta), cancellationToken);
    }

    public async Task Handle(LocationChangedNotification notification, CancellationToken cancellationToken)
    {
        _location = notification.Location;

        var directionDelta = GetDirectionDelta();
        await _mediator.Publish(new DirectionDeltaChangedNotification(directionDelta), cancellationToken);
    }

    public Task Handle(SetDestinationCommand request, CancellationToken cancellationToken)
    {
        _destination = request.Destination;

        return Task.CompletedTask;
    }

    private double GetDirectionDelta()
    {
        if (_heading == null || _location == null || _destination == null)
            return double.NaN;

        var directionToLocation = _navigationService.GetDirectionToLocation(_destination, _location);

        return (directionToLocation.North - _heading.North + 360) % 360;
    }
}
