using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Commands;
using Core.Enums;
using Core.Queries;
using MediatR;
using MiniMap.Utils;
using MiniMap.ViewModels.Decorators;
using System.Collections.ObjectModel;

namespace MiniMap.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IMediator _mediator;
    private readonly ILocationFeatureManager _locationFeatureManager;

    [ObservableProperty]
    private LocationDecoratorViewModel? _selectedLocation;

    public ObservableCollection<LocationDecoratorViewModel> Locations { get; set; } = [];

    public MainViewModel(IMediator mediator, ILocationFeatureManager locationFeatureManager)
    {
        _mediator = mediator;
        _locationFeatureManager = locationFeatureManager;
    }

    [RelayCommand]
    private async Task SaveCurrentLocation()
    {
        _locationFeatureManager.EnsureLocationFeatureIsEnabled();

        //TODO: add prompt for location name and description

        var locationSaveResult = await _mediator.Send(new SaveCurrentLocationCommand());

        switch (locationSaveResult.Status)
        {
            case LocationStatus.Success:
                Locations.Add(new LocationDecoratorViewModel(locationSaveResult.LocationResult.Location));
                await Toast.Make("Location saved successfully.").Show();
                break;
            case LocationStatus.PermissionDenied:
                await Toast.Make("Location permission denied.").Show();
                break;
            case LocationStatus.LocationDisabled:
                _locationFeatureManager.EnsureLocationFeatureIsEnabled();
                break;
            case LocationStatus.LocationUnavailable:
                await Toast.Make("Location unavailable.").Show();
                break;
            case LocationStatus.UnknownError:
                await Toast.Make("An unknown error occurred.").Show();
                break;
        }
    }

    [RelayCommand]
    private async Task NavigateToLocation()
    {
        if (SelectedLocation == null)
            return;

        _locationFeatureManager.EnsureLocationFeatureIsEnabled();

        await _mediator.Send(new SetDestinationCommand(SelectedLocation.Model));

        await Shell.Current.GoToAsync(Routes.NavigationRoute);
    }

    [RelayCommand]
    public async Task Appearing()
    {
        var locations = await _mediator.Send(new GetLocationsQuery(true));

        Locations.Clear();

        foreach (var location in locations)
            Locations.Add(new LocationDecoratorViewModel(location));
    }
}
