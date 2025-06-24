using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Commands;
using MediatR;

namespace MiniMap.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IMediator _mediator;

    [ObservableProperty]
    private string _progressText = "Ready to save location";

    public MainViewModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    [RelayCommand]
    private async Task SaveCurrentLocation()
    {
        ProgressText = "Saving current location...";

        await _mediator.Send(new SaveCurrentLocationCommand());

        ProgressText = "Current location saved successfully!";
    }
}
