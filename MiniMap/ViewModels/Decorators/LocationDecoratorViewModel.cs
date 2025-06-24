using CommunityToolkit.Mvvm.ComponentModel;
using Core.Interfaces;

namespace MiniMap.ViewModels.Decorators;

public class LocationDecoratorViewModel : ObservableObject
{
    private readonly ILocation _model;

    public LocationDecoratorViewModel(ILocation location)
    {
        _model = location;
    }

    public string Name
    {
        get => _model.Name;
        set
        {
            if (_model.Name != value)
            {
                _model.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }

    public double Latitude
    {
        get => _model.Latitude;
        set
        {
            if (_model.Latitude != value)
            {
                _model.Latitude = value;
                OnPropertyChanged(nameof(Latitude));
            }
        }
    }

    public double Longitude
    {
        get => _model.Longitude;
        set
        {
            if (_model.Longitude != value)
            {
                _model.Longitude = value;
                OnPropertyChanged(nameof(Longitude));
            }
        }
    }

    public string? Description
    {
        get => _model.Description;
        set
        {
            if (_model.Description != value)
            {
                _model.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
    }
}
