using System.Collections.Generic;
using Chartboost.Core.Android.Modules;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Core.Usercentrics.Android
{
    internal class ChartboostCoreUsercentricsAdapter : ChartboostCoreNativeAndroidModule
    {
        private const string ClassChartboostCoreUsercentricsAdapter = "com.chartboost.core.consent.UsercentricsAdapter";
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void SetInstance() => Usercentrics.ChartboostCoreUsercentricsAdapter.InstanceType = typeof(ChartboostCoreUsercentricsAdapter);
        
        [Preserve]
        public ChartboostCoreUsercentricsAdapter(ChartboostCoreUsercentricsOptions options) : base(CreateInstance(options)) { }

        [Preserve]
        public ChartboostCoreUsercentricsAdapter(Dictionary<string, object> jsonConfig) : base(CreateInstance(jsonConfig)) { }

        private static AndroidJavaObject CreateInstance(ChartboostCoreUsercentricsOptions options)
        {
            var usercentricsOptions = new AndroidJavaObject("com.usercentrics.sdk.UsercentricsOptions", options.SettingsId);
            return new AndroidJavaObject(ClassChartboostCoreUsercentricsAdapter, "ChartboostCore", usercentricsOptions, null);
        }

        private static AndroidJavaObject CreateInstance(Dictionary<string, object> jsonConfig)
        {
            var usercentricsAdapter = new AndroidJavaObject(ClassChartboostCoreUsercentricsAdapter);
            usercentricsAdapter.Call("updateProperties");
            return usercentricsAdapter;
        }
    }
}
