using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Core.Commands;
using Core.Interfaces;
using Core.Queries;
using MediatR;
using MiniMap.Messages;

namespace MiniMap.ViewModels;

public partial class NavigationViewModel : ObservableObject, IRecipient<DirectionDeltaChangedMessage>
{
    [ObservableProperty]
    private ILocation _destination;

    [ObservableProperty]
    private string _delta;

    [ObservableProperty]
    private ILocation _currentLocation;

    [ObservableProperty]
    private int _currentHeading;

    private readonly IMediator _mediator;

    public NavigationViewModel(IMediator mediator)
    {
        _mediator = mediator;

        WeakReferenceMessenger.Default.Register(this);
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

    public void Receive(DirectionDeltaChangedMessage message)
    {
        //TODO: update the UI with the new direction delta
        Delta = $"{message.Delta}°";
        CurrentLocation = message.Location;
        CurrentHeading = message.Bearing;
    }
}
