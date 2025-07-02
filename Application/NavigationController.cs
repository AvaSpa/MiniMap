using Core.Commands;
using Core.Interfaces;
using Core.Notifications;
using Core.Queries;
using MediatR;

namespace Application;

//TODO: command/query handlers are transient; make separate classes for them and inject the controllers (also readd interfaces for the controllers)

public class NavigationController : INavigationController,
    INotificationHandler<HeadingChangedNotification>,
    INotificationHandler<LocationChangedNotification>,
    IRequestHandler<SetDestinationCommand>,
    IRequestHandler<StartNavigationCommand>,
    IRequestHandler<StopNavigationCommand>,
    IRequestHandler<GetDestinationQuery, ILocation>
{
    private IHeading _heading;
    private ILocation _location;
    private ILocation _destination;

    private readonly IMediator _mediator;
    private readonly INavigationService _navigationService;
    private readonly ILocationService _locationService;

    public NavigationController(IMediator mediator, INavigationService navigationService, ILocationService locationService)
    {
        _mediator = mediator;
        _navigationService = navigationService;
        _locationService = locationService;
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

    public Task Handle(StartNavigationCommand request, CancellationToken cancellationToken)
    {
        _navigationService.StartMonitoringCompass();
        _locationService.StartMonitoringLocation();

        return Task.CompletedTask;
    }

    public Task Handle(StopNavigationCommand request, CancellationToken cancellationToken)
    {
        _navigationService.StopMonitoringCompass();
        _locationService.StopMonitoringLocation();

        return Task.CompletedTask;
    }

    public Task<ILocation> Handle(GetDestinationQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_destination);
    }

    private double GetDirectionDelta()
    {
        if (_heading == null || _location == null || _destination == null)
            return double.NaN;

        var directionToLocation = _navigationService.GetDirectionToLocation(_destination, _location);

        return (directionToLocation.North - _heading.North + 360) % 360;
    }
}
