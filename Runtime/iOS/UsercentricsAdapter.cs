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
    internal class UsercentricsAdapter : NativeModule
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void SetInstance() => Usercentrics.UsercentricsAdapter.InstanceType = typeof(UsercentricsAdapter);
        
        [Preserve]
        public UsercentricsAdapter(UsercentricsOptions options) : base(CreateInstance(options)) { }

        [Preserve]
        public UsercentricsAdapter(Dictionary<string, object> jsonConfig) : base(CreateInstance(jsonConfig)) { }

        private static IntPtr CreateInstance(UsercentricsOptions options) => _chartboostCoreGetUsercentricsAdapter(options.SettingsId);

        private static IntPtr CreateInstance(Dictionary<string, object> jsonConfig) => _chartboostCoreGetUsercentricsAdapterFromConfig(JsonConvert.SerializeObject(jsonConfig));

        [DllImport(IOSConstants.DLLImport)] private static extern IntPtr _chartboostCoreGetUsercentricsAdapter(string settingsId);
        [DllImport(IOSConstants.DLLImport)] private static extern IntPtr _chartboostCoreGetUsercentricsAdapterFromConfig(string jsonConfig);
    }
}
