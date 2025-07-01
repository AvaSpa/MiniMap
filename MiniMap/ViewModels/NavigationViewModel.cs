using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Commands;
using Core.Interfaces;
using Core.Notifications;
using Core.Queries;
using MediatR;

namespace MiniMap.ViewModels;

public partial class NavigationViewModel : ObservableObject, INotificationHandler<DirectionDeltaChangedNotification>
{
    [ObservableProperty]
    private ILocation _destination;

    [ObservableProperty]
    private string _delta;

    private readonly IMediator _mediator;

    public NavigationViewModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    [RelayCommand]
    private async Task Appearing()
    {
        Destination = await _mediator.Send(new GetDestinationQuery());

        _mediator.Send(new StartNavigationCommand()).Wait();
    }

    [RelayCommand]
    private void Disappearing()
    {
        _mediator.Send(new StopNavigationCommand()).Wait();
    }

    public async Task Handle(DirectionDeltaChangedNotification notification, CancellationToken cancellationToken)
    {
        //TODO: update the UI with the new direction delta
        Delta = $"{notification.Delta}°";
    }
}
