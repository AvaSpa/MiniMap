using Core.Interfaces;
using Core.Models;
using Core.Notifications;
using MediatR;
using Microsoft.Maui.Devices.Sensors;

namespace Infrastructure.Services;

public class NavigationService : INavigationService
{
    private readonly IMediator _mediator;

    private double _currentHeading;

    public NavigationService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void StartMonitoringCompass()
    {
        if (Compass.IsMonitoring)
            return;

        Compass.Start(SensorSpeed.Default);

        Compass.ReadingChanged += OnCompassReadingChanged;
    }

    public void StopMonitoringCompass()
    {
        if (!Compass.IsMonitoring)
            return;

        Compass.ReadingChanged -= OnCompassReadingChanged;
        Compass.Stop();
    }

    public IHeading GetDirectionToLocation(ILocation location, ILocation currentLocation)
    {
        double lat1 = DegreesToRadians(currentLocation.Latitude);
        double lon1 = DegreesToRadians(currentLocation.Longitude);
        double lat2 = DegreesToRadians(location.Latitude);
        double lon2 = DegreesToRadians(location.Longitude);

        double dLon = lon2 - lon1;

        double y = Math.Sin(dLon) * Math.Cos(lat2);
        double x = Math.Cos(lat1) * Math.Sin(lat2) -
                   Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(dLon);

        double bearingRad = Math.Atan2(y, x);
        double bearingDeg = (RadiansToDegrees(bearingRad) + 360) % 360;

        return new Heading(bearingDeg);
    }

    private double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }

    private double RadiansToDegrees(double radians)
    {
        return radians * 180.0 / Math.PI;
    }

    private void OnCompassReadingChanged(object? sender, CompassChangedEventArgs e)
    {
        _currentHeading = e.Reading.HeadingMagneticNorth;
        _mediator.Publish(new HeadingChangedNotification(new Heading(_currentHeading)));
    }
}
