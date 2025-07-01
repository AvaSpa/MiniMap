namespace MiniMap.Utils;

public class LocationFeatureManager : ILocationFeatureManager
{
    public void EnsureLocationFeatureIsEnabled()
    {
        AppInfo.ShowSettingsUI();
    }
}
