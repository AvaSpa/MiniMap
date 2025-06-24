using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Interfaces;

namespace MiniMap.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ILocationController _locationController;

    public MainViewModel(ILocationController locationController)
    {
        _locationController = locationController;
    }


    [RelayCommand]
    private async Task SaveCurrentLocation()
    {
        await _locationController.SaveCurrentLocation();
    }
}
