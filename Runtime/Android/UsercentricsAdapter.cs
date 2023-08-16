using System.Collections.Generic;
using Chartboost.Core.Android.Modules;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Core.Usercentrics.Android
{
    internal class UsercentricsAdapter : NativeModule
    {
        private const string ClassUsercentricsAdapter = "com.chartboost.core.consent.usercentrics.UsercentricsAdapter";
        private const string ClassUsercentricsOptions = "com.usercentrics.sdk.UsercentricsOptions";
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void SetInstance() => Usercentrics.UsercentricsAdapter.InstanceType = typeof(UsercentricsAdapter);
        
        [Preserve]
        public UsercentricsAdapter(string dpsName, UsercentricsOptions options) : base(CreateInstance(dpsName, options)) { }

        [Preserve]
        public UsercentricsAdapter(Dictionary<string, object> jsonConfig) : base(CreateInstance(jsonConfig)) { }

        private static AndroidJavaObject CreateInstance(string dpsName, UsercentricsOptions options)
        {
            var usercentricsOptions = new AndroidJavaObject(ClassUsercentricsOptions, options.SettingsId);
            return new AndroidJavaObject(ClassUsercentricsAdapter, dpsName, usercentricsOptions);
        }

        private static AndroidJavaObject CreateInstance(Dictionary<string, object> jsonConfig)
        {
            var usercentricsAdapter = new AndroidJavaObject(ClassUsercentricsAdapter);
            usercentricsAdapter.Call("updateProperties");
            return usercentricsAdapter;
        }
    }
}
