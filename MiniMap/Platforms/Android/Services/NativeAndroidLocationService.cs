using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Core.Enums;
using Core.Interfaces;
using Core.Models;
using Core.Notifications;
using Java.Util.Concurrent;
using Java.Util.Functions;
using MediatR;

namespace Infrastructure.Services;

public class NativeAndroidLocationService : ILocationService
{
    private readonly IMediator _mediator;
    private readonly Context _context;
    private LocationManager? _locationManager;
    private AndroidLocationListener? _listener;

    public NativeAndroidLocationService(IMediator mediator)
    {
        _mediator = mediator;
        _context = Android.App.Application.Context;
    }

    public Task StartMonitoringLocation()
    {
        _locationManager ??= (LocationManager?)_context.GetSystemService(Context.LocationService);
        _listener ??= new AndroidLocationListener(location =>
        {
            var loc = new Core.Models.Location("Moving Location", location.Latitude, location.Longitude);
            _mediator.Publish(new LocationChangedNotification(loc));
        });

        _locationManager?.RequestLocationUpdates(LocationManager.GpsProvider, 10000, 1, _listener!);

        return Task.CompletedTask;
    }

    public void StopMonitoringLocation()
    {
        if (_locationManager != null && _listener != null)
        {
            _locationManager.RemoveUpdates(_listener);
        }
    }

    public async Task<ILocationResult> GetCurrentLocation()
    {
        _locationManager ??= (LocationManager?)_context.GetSystemService(Context.LocationService);
        var tcs = new TaskCompletionSource<Android.Locations.Location>();

        if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
        {
            var cancellationSignal = new CancellationSignal();
            var executor = Executors.NewSingleThreadExecutor();
            var locationCallback = new LocationListenerCallback(location =>
            {
                tcs.TrySetResult(location);
            });

#pragma warning disable CA1416 
            _locationManager!.GetCurrentLocation(
                LocationManager.GpsProvider,
                cancellationSignal,
                executor!,
                locationCallback
            );
#pragma warning restore CA1416

            var completedTask = await Task.WhenAny(tcs.Task, Task.Delay(10000));
            if (completedTask == tcs.Task && tcs.Task.Result != null)
            {
                var location = tcs.Task.Result;
                var locationModel = new Core.Models.Location("Current Location", location.Latitude, location.Longitude)
                {
                    Description = $"Current location at {DateTime.Now:yyyy-MM-dd HH:mm:ss}"
                };
                return new LocationResult(LocationStatus.Success, locationModel);
            }
        }
        else
        {
#pragma warning disable CA1422 
            var singleUpdateListener = new AndroidLocationListener(location =>
            {
                tcs.TrySetResult(location);
            });

            _locationManager?.RequestSingleUpdate(LocationManager.GpsProvider, singleUpdateListener, null);
#pragma warning restore CA1422

            var completedTask = await Task.WhenAny(tcs.Task, Task.Delay(10000));
            if (completedTask == tcs.Task && tcs.Task.Result != null)
            {
                var location = tcs.Task.Result;
                var locationModel = new Core.Models.Location("Current Location", location.Latitude, location.Longitude)
                {
                    Description = $"Current location at {DateTime.Now:yyyy-MM-dd HH:mm:ss}"
                };
                return new LocationResult(LocationStatus.Success, locationModel);
            }
        }

        return new LocationResult(LocationStatus.UnknownError, new Core.Models.Location("Error", 0, 0) { Description = "Unable to retrieve current location." });
    }

    private class AndroidLocationListener : Java.Lang.Object, ILocationListener
    {
        private readonly Action<Android.Locations.Location> _onLocationChanged;

        public AndroidLocationListener(Action<Android.Locations.Location> onLocationChanged)
        {
            _onLocationChanged = onLocationChanged;
        }

        public void OnLocationChanged(Android.Locations.Location location)
        {
            _onLocationChanged(location);
        }

        public void OnProviderDisabled(string provider) { }
        public void OnProviderEnabled(string provider) { }
        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras) { }
    }

    private class LocationListenerCallback : Java.Lang.Object, IConsumer
    {
        private readonly Action<Android.Locations.Location> _onLocationReceived;

        public LocationListenerCallback(Action<Android.Locations.Location> onLocationReceived)
        {
            _onLocationReceived = onLocationReceived;
        }

        public void Accept(Java.Lang.Object? value)
        {
            if (value is Android.Locations.Location location)
            {
                _onLocationReceived(location);
            }
        }
    }
}