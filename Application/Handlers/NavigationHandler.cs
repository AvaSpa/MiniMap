using Core.Commands;
using Core.Interfaces;
using Core.Notifications;
using Core.Queries;
using MediatR;

namespace Application.Handlers;

public class NavigationHandler : INotificationHandler<HeadingChangedNotification>,
    INotificationHandler<LocationChangedNotification>,
    IRequestHandler<SetDestinationCommand>,
    IRequestHandler<StartNavigationCommand>,
    IRequestHandler<StopNavigationCommand>,
    IRequestHandler<GetDestinationQuery, ILocation>
{
    private readonly IMediator _mediator;
    private readonly INavigationController _navigationController;

    public NavigationHandler(IMediator mediator, INavigationController navigationController)
    {
        _mediator = mediator;
        _navigationController = navigationController;
    }

    public async Task Handle(HeadingChangedNotification notification, CancellationToken cancellationToken)
    {
        _navigationController.UpdateHeading(notification.Heading);

        var directionDelta = _navigationController.GetDirectionDelta();

        await _mediator.Publish(new DirectionDeltaChangedNotification(directionDelta, _navigationController.CurrentLocation, _navigationController.CurrentHeading), cancellationToken);
    }

    public async Task Handle(LocationChangedNotification notification, CancellationToken cancellationToken)
    {
        _navigationController.UpdateLocation(notification.Location);

        var directionDelta = _navigationController.GetDirectionDelta();

        await _mediator.Publish(new DirectionDeltaChangedNotification(directionDelta, _navigationController.CurrentLocation, _navigationController.CurrentHeading), cancellationToken);
    }

    public Task Handle(SetDestinationCommand request, CancellationToken cancellationToken)
    {
        _navigationController.SetDestination(request.Destination);

        return Task.CompletedTask;
    }

    public async Task Handle(StartNavigationCommand request, CancellationToken cancellationToken)
    {
        await _navigationController.StartNavigation();
    }

    public Task Handle(StopNavigationCommand request, CancellationToken cancellationToken)
    {
        _navigationController.StopNavigation();

        return Task.CompletedTask;
    }

    public Task<ILocation> Handle(GetDestinationQuery request, CancellationToken cancellationToken)
    {
        var destination = _navigationController.GetDestination();

        return Task.FromResult(destination);
    }
}
