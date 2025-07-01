using Android.Content;
using Android.Provider;

namespace MiniMap.Utils;

public class LocationFeatureManager : ILocationFeatureManager
{
    public void EnsureLocationFeatureIsEnabled()
    {
        var context = Platform.CurrentActivity ?? Platform.AppContext;
        var intent = new Intent(Settings.ActionLocationSourceSettings);
        intent.SetFlags(ActivityFlags.NewTask);
        context.StartActivity(intent);
    }
}