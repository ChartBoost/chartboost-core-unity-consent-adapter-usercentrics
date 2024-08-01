namespace Chartboost.Core.Usercentrics
{
    /// <summary>
    /// Utilize to configure Usercentrics initialization options.
    /// </summary>
    public struct UsercentricsOptions
    {
        /// <summary>
        /// A Usercentrics generated ID, used to identify a bundle of CMP configurations to be used depending on the user's location.
        /// </summary>
        public readonly string RulesetId;

        /// <summary>
        /// A Usercentrics generated ID, used to identify a unique CMP configuration.
        /// </summary>
        public readonly string SettingsId;

        /// <summary>
        /// Selected based on Usercentrics <a href="https://usercentrics.com/docs/apps/integration/configure/#language-selection-hierarchy">Language Selection Hierarchy</a>. This property defines the language used to render the banner. e.g. "en", "de", "fr".
        /// </summary>
        public readonly string DefaultLanguage;

        /// <summary>
        /// To freeze the configuration version shown to your users, you may pass a specific version here.
        /// </summary>
        public readonly string Version;

        /// <summary>
        /// Timeout for network requests in milliseconds. We do NOT recommend overwriting this field unless absolutely necessary or for debugging reasons, as well as using any values under 5,000 ms. Default is 10,000 ms (10s).
        /// </summary>
        public readonly long? TimeoutMilliseconds;

        /// <summary>
        /// Enable <a href="https://usercentrics.com/docs/apps/features/consent-mediation/">Consent Mediation</a>, an automated way to pass consent to 3rd party frameworks.
        /// </summary>
        public readonly bool? ConsentMediation;

        /// <summary>
        /// Provides a set of logs for operations being executed in the SDK.
        /// </summary>
        public readonly UsercentricsLoggerLevel? LoggerLevel;

        /// <summary>
        /// Sets the network operation mode. Be careful, use this option only if we have confirmed that it is ready to use because it has a significant impact on the whole system's performance. The default value is "world".
        /// </summary>
        public readonly NetworkMode? NetworkMode;

        public UsercentricsOptions(string settingsId = null, string rulesetId = null, string defaultLanguage = null,
            string version = null, long? timeoutMilliseconds = null, bool? consentMediation = null,
            UsercentricsLoggerLevel? loggerLevel = null, NetworkMode? networkMode = null)
        {
            SettingsId = settingsId;
            RulesetId = rulesetId;
            DefaultLanguage = defaultLanguage;
            Version = version;
            TimeoutMilliseconds = timeoutMilliseconds;
            ConsentMediation = consentMediation;
            LoggerLevel = loggerLevel;
            NetworkMode = networkMode;
        }
    }
}
