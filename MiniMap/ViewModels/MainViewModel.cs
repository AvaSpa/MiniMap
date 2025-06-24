using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Commands;
using Core.Queries;
using MediatR;
using MiniMap.ViewModels.Decorators;
using System.Collections.ObjectModel;

namespace MiniMap.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IMediator _mediator;

    public ObservableCollection<LocationDecoratorViewModel> Locations { get; set; } = [];

    public MainViewModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    [RelayCommand]
    private async Task SaveCurrentLocation()
    {
        //TODO: add prompt for location name and description

        var savedLocation = await _mediator.Send(new SaveCurrentLocationCommand());

        Locations.Add(new LocationDecoratorViewModel(savedLocation));
    }

    public async Task OnAppearing()
    {
        var locations = await _mediator.Send(new GetLocationsQuery(true));

        Locations.Clear();

        foreach (var location in locations)
            Locations.Add(new LocationDecoratorViewModel(location));
    }
}
