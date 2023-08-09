using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Chartboost.Core.iOS.Modules;
using Chartboost.Core.iOS.Utilities;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Core.Usercentrics.iOS
{
    internal class ChartboostCoreUsercentricsAdapter : ChartboostCoreNativeIOSModule
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void SetInstance() => Usercentrics.ChartboostCoreUsercentricsAdapter.InstanceType = typeof(ChartboostCoreUsercentricsAdapter);
        
        [Preserve]
        public ChartboostCoreUsercentricsAdapter(ChartboostCoreUsercentricsOptions options) : base(CreateInstance(options)) { }

        [Preserve]
        public ChartboostCoreUsercentricsAdapter(Dictionary<string, object> jsonConfig) : base(CreateInstance(jsonConfig)) { }

        private static IntPtr CreateInstance(ChartboostCoreUsercentricsOptions options) => _chartboostCoreGetUsercentricsAdapter(options.SettingsId);

        private static IntPtr CreateInstance(Dictionary<string, object> jsonConfig) => _chartboostCoreGetUsercentricsAdapterFromConfig(JsonConvert.SerializeObject(jsonConfig));

        [DllImport(ChartboostCoreIOSConstants.DLLImport)] private static extern IntPtr _chartboostCoreGetUsercentricsAdapter(string settingsId);
        [DllImport(ChartboostCoreIOSConstants.DLLImport)] private static extern IntPtr _chartboostCoreGetUsercentricsAdapterFromConfig(string jsonConfig);
    }
}
