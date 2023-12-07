using System.Collections.Generic;
using Chartboost.Core.Android.Modules;
using Chartboost.Core.Android.Utilities;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Core.Usercentrics.Android
{
    internal class UsercentricsAdapter : NativeModule
    {
        private const string ClassUsercentricsAdapter = "com.chartboost.core.consent.usercentrics.UsercentricsAdapter";
        private const string ClassUsercentricsOptions = "com.usercentrics.sdk.UsercentricsOptions";
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void SetInstance()
        {
            if (Application.isEditor)
                return;
            Usercentrics.UsercentricsAdapter.InstanceType = typeof(UsercentricsAdapter);
        }

        [Preserve]
        public UsercentricsAdapter(UsercentricsOptions options, IDictionary<string, string> templateIdToPartnerId) : base(CreateInstance(options, templateIdToPartnerId)) { }

        [Preserve]
        public UsercentricsAdapter(string dpsName, UsercentricsOptions options, IDictionary<string, string> templateIdToPartnerId) : base(CreateInstance(dpsName, options, templateIdToPartnerId)) { }

        private static AndroidJavaObject CreateInstance(UsercentricsOptions options, IDictionary<string, string> templateIdToPartnerId)
        {
            var usercentricsOptions = new AndroidJavaObject(ClassUsercentricsOptions, options.SettingsId);
            return new AndroidJavaObject(ClassUsercentricsAdapter, usercentricsOptions, templateIdToPartnerId.DictionaryToMap());
        }
        
        private static AndroidJavaObject CreateInstance(string dpsName, UsercentricsOptions options, IDictionary<string, string> templateIdToPartnerId)
        {
            var usercentricsOptions = new AndroidJavaObject(ClassUsercentricsOptions, options.SettingsId);
            return new AndroidJavaObject(ClassUsercentricsAdapter, usercentricsOptions, templateIdToPartnerId.DictionaryToMap(), dpsName);
        }
    }
}
