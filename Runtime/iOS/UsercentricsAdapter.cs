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
        public UsercentricsAdapter(string dpsName, UsercentricsOptions options) : base(CreateInstance(dpsName, options)) { }

        private static IntPtr CreateInstance(string dpsName, UsercentricsOptions options) => _chartboostCoreGetUsercentricsAdapter(dpsName, options.SettingsId);
        
        [DllImport(IOSConstants.DLLImport)] private static extern IntPtr _chartboostCoreGetUsercentricsAdapter(string dpsName, string settingsId);
    }
}
