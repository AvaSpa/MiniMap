using Android.Content;
using Android.Locations;
using Android.Provider;

namespace MiniMap.Utils;

public class LocationFeatureManager : ILocationFeatureManager
{
    public void EnsureLocationFeatureIsEnabled()
    {
        var context = Platform.CurrentActivity ?? Platform.AppContext;

        var locationManager = (LocationManager?)context.GetSystemService(Context.LocationService);
        bool isGpsEnabled = locationManager?.IsProviderEnabled(LocationManager.GpsProvider) ?? false;

        if (isGpsEnabled)
            return;

        var intent = new Intent(Settings.ActionLocationSourceSettings);
        intent.SetFlags(ActivityFlags.NewTask);
        context.StartActivity(intent);
    }
}