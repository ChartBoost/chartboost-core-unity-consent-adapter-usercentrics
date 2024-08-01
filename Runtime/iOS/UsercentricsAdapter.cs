using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Chartboost.Constants;
using Chartboost.Core.iOS.Modules;
using Chartboost.Json;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Core.Usercentrics.iOS
{
    internal sealed class UsercentricsAdapter : NativeModule
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void SetInstance()
        {
            if (Application.isEditor)
                return;
            Usercentrics.UsercentricsAdapter.InstanceType = typeof(UsercentricsAdapter);
        }

        [Preserve]
        public UsercentricsAdapter(UsercentricsOptions options, IDictionary<string, string> templateIdToPartnerId = null) : base(CreateInstance(options, templateIdToPartnerId)) { }

        private static IntPtr CreateInstance(UsercentricsOptions options, IDictionary<string, string> templateIdToPartnerId = null)
        {
            var timeoutMilliseconds = options.TimeoutMilliseconds ?? -1;
            var consentMediation = options.ConsentMediation.HasValue ? options.ConsentMediation.Value ? 1 : 0 : -1;
            var loggerLevel = options.LoggerLevel.HasValue ? (int)options.LoggerLevel.Value : -1;
            var networkMode = options.NetworkMode.HasValue ? (int)options.NetworkMode.Value : -1;
            return _CBCGetUsercentricsAdapter(options.RulesetId, options.SettingsId, options.DefaultLanguage, options.Version, timeoutMilliseconds, consentMediation, loggerLevel, networkMode, JsonTools.SerializeObject(templateIdToPartnerId));
        }

        [DllImport(SharedIOSConstants.DLLImport)] private static extern IntPtr _CBCGetUsercentricsAdapter(string rulesetId, string settingsId, string defaultLanguage, string version, long timeoutMilliseconds, int consentMediation, int loggerLevel, int networkMode, string templateIdToPartnerId);
    }
}
