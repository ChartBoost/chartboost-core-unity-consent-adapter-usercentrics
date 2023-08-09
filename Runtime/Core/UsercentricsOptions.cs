namespace Chartboost.Core.Usercentrics
{
    public class UsercentricsOptions
    {
        public string SettingsId { get; } = string.Empty;

        public UsercentricsOptions(string settingsId)
        {
            if (!string.IsNullOrEmpty(settingsId))
                SettingsId = settingsId;
        }
    }
}
