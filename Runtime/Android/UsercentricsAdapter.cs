using System.Collections.Generic;
using Chartboost.Core.Android.Modules;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Core.Usercentrics.Android
{
    internal class UsercentricsAdapter : NativeModule
    {
        private const string ClassUsercentricsAdapter = "com.chartboost.core.consent.usercentrics.UsercentricsAdapter";
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void SetInstance() => Usercentrics.UsercentricsAdapter.InstanceType = typeof(UsercentricsAdapter);
        
        [Preserve]
        public UsercentricsAdapter(UsercentricsOptions options) : base(CreateInstance(options)) { }

        [Preserve]
        public UsercentricsAdapter(Dictionary<string, object> jsonConfig) : base(CreateInstance(jsonConfig)) { }

        private static AndroidJavaObject CreateInstance(UsercentricsOptions options)
        {
            var usercentricsOptions = new AndroidJavaObject("com.usercentrics.sdk.UsercentricsOptions", options.SettingsId);
            return new AndroidJavaObject(ClassUsercentricsAdapter, "ChartboostCore", usercentricsOptions, null);
        }

        private static AndroidJavaObject CreateInstance(Dictionary<string, object> jsonConfig)
        {
            var usercentricsAdapter = new AndroidJavaObject(ClassUsercentricsAdapter);
            usercentricsAdapter.Call("updateProperties");
            return usercentricsAdapter;
        }
    }
}
