namespace Chartboost.Core.Usercentrics
{
    public class ChartboostCoreUsercentricsOptions
    {
        public string SettingsId { get; } = string.Empty;

        public ChartboostCoreUsercentricsOptions(string settingsId)
        {
            if (!string.IsNullOrEmpty(settingsId))
                SettingsId = settingsId;
        }
    }
}
