using System.Collections.Generic;
using Chartboost.Core.Android.Modules;
using Chartboost.Logging;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Core.Usercentrics.Android
{
    internal sealed class UsercentricsAdapter : NativeModule
    {
        private const string Tag = nameof(UsercentricsAdapter);
        private const string UsercentricsNamespace = "com.usercentrics.sdk";
        // ReSharper disable InconsistentNaming
        private const string ClassUsercentricsAdapter = "com.chartboost.core.consent.usercentrics.UsercentricsAdapter";
        private static readonly string ClassUsercentricsOptions = $"{UsercentricsNamespace}.UsercentricsOptions";

        private const string FunctionSetRulesetId = "setRuleSetId";
        private const string FunctionSetSettingsId = "setSettingsId";
        private const string FunctionSetDefaultLanguage = "setDefaultLanguage";
        private const string FunctionSetVersion = "setVersion";
        private const string FunctionSetTimeoutMillis = "setTimeoutMillis";
        private const string FunctionSetConsentMediation = "setConsentMediation";

        private const string CommonNamespace = "models.common";
        private static readonly string EnumUsercentricsLoggerLevel = $"{UsercentricsNamespace}.{CommonNamespace}.UsercentricsLoggerLevel";
        private const string UsercentricsLoggerLevelNone = "NONE";
        private const string UsercentricsLoggerLevelError = "ERROR";
        private const string UsercentricsLoggerLevelWarning = "WARNING";
        private const string UsercentricsLoggerLevelDebug = "DEBUG";
        private const string FunctionSetLoggerLevel = "setLoggerLevel";

        private static readonly string EnumUsercentricsNetworkMode = $"{UsercentricsNamespace}.{CommonNamespace}.NetworkMode";
        private const string UsercentricsNetworkModeWorld = "WORLD";
        private const string UsercentricsNetworkModeEU = "EU";
        private const string FunctionSetNetworkMode = "setNetworkMode";
        // ReSharper restore InconsistentNaming


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void SetInstance()
        {
            if (Application.isEditor)
                return;
            
            Usercentrics.UsercentricsAdapter.InstanceType = typeof(UsercentricsAdapter);
        }
        
        [Preserve]
        public UsercentricsAdapter(UsercentricsOptions options, IDictionary<string, string> templateIdToPartnerId = null) : base(CreateInstance(options, templateIdToPartnerId)) { }
        
        private static AndroidJavaObject CreateInstance(UsercentricsOptions options, IDictionary<string, string> templateIdToPartnerId = null)
        {
            var nativeOptions = new AndroidJavaObject(ClassUsercentricsOptions);

            if (!string.IsNullOrEmpty(options.RulesetId))
            {
                nativeOptions.Call(FunctionSetRulesetId, options.RulesetId);;
                LogController.Log($"{Tag} Setting RulesetID: {options.RulesetId}, SettingsId will be ignored", LogLevel.Verbose);
            }

            if (!string.IsNullOrEmpty(options.SettingsId) && string.IsNullOrEmpty(options.RulesetId))
            {
                nativeOptions.Call(FunctionSetSettingsId, options.SettingsId);
                LogController.Log($"{Tag} Setting SettingsId: {options.SettingsId}", LogLevel.Verbose);
            }

            if (!string.IsNullOrEmpty(options.DefaultLanguage))
            {
                nativeOptions.Call(FunctionSetDefaultLanguage, options.DefaultLanguage);
                LogController.Log($"{Tag} Setting DefaultLanguage: {options.DefaultLanguage}", LogLevel.Verbose);
            }

            if (!string.IsNullOrEmpty(options.Version))
            {
                nativeOptions.Call(FunctionSetVersion, options.Version);
                LogController.Log($"{Tag} Setting Version: {options.Version}", LogLevel.Verbose);
            }

            if (options.TimeoutMilliseconds.HasValue)
            {
                nativeOptions.Call(FunctionSetTimeoutMillis, options.TimeoutMilliseconds.Value);
                LogController.Log($"{Tag} Setting TimeoutMilliseconds: {options.TimeoutMilliseconds}", LogLevel.Verbose);
            }

            if (options.ConsentMediation.HasValue)
            {
                nativeOptions.Call(FunctionSetConsentMediation, options.ConsentMediation.Value);
                LogController.Log($"{Tag} Setting ConsentMediation: {options.ConsentMediation}", LogLevel.Verbose);
            }

            if (options.LoggerLevel.HasValue)
            {
                using var enumClass = new AndroidJavaClass(EnumUsercentricsLoggerLevel);
                using var value = options.LoggerLevel.Value switch
                {
                    UsercentricsLoggerLevel.None => enumClass.GetStatic<AndroidJavaObject>(UsercentricsLoggerLevelNone),
                    UsercentricsLoggerLevel.Error => enumClass.GetStatic<AndroidJavaObject>(UsercentricsLoggerLevelError),
                    UsercentricsLoggerLevel.Warning => enumClass.GetStatic<AndroidJavaObject>(UsercentricsLoggerLevelWarning),
                    UsercentricsLoggerLevel.Debug => enumClass.GetStatic<AndroidJavaObject>(UsercentricsLoggerLevelDebug),
                    _ => enumClass.GetStatic<AndroidJavaObject>(UsercentricsLoggerLevelNone),
                };

                nativeOptions.Call(FunctionSetLoggerLevel, value);
                LogController.Log($"{Tag} Setting LoggerLevel: {options.LoggerLevel}", LogLevel.Verbose);
            }

            if (options.NetworkMode.HasValue)
            {
                using var enumClass = new AndroidJavaClass(EnumUsercentricsNetworkMode);
                using var value = options.NetworkMode.Value switch
                {
                    NetworkMode.World => enumClass.GetStatic<AndroidJavaObject>(UsercentricsNetworkModeWorld),
                    NetworkMode.EU => enumClass.GetStatic<AndroidJavaObject>(UsercentricsNetworkModeEU),
                    _ => enumClass.GetStatic<AndroidJavaObject>(UsercentricsNetworkModeWorld),
                };
                
                nativeOptions.Call(FunctionSetNetworkMode, value);
                LogController.Log($"{Tag} Setting LoggerLevel: {options.NetworkMode}", LogLevel.Verbose);
            }

            return new AndroidJavaObject(ClassUsercentricsAdapter, nativeOptions, templateIdToPartnerId.DictionaryToMap());
        }
    }
}
