using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Chartboost.Core.iOS.Modules;
using Chartboost.Core.iOS.Utilities;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Core.Usercentrics.iOS
{
    internal class UsercentricsAdapter : NativeModule
    {
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

        private static IntPtr CreateInstance(UsercentricsOptions options, IDictionary<string, string> templateIdToPartnerId) => _chartboostCoreGetUsercentricsAdapter(options.SettingsId);
        private static IntPtr CreateInstance(string dpsName, UsercentricsOptions options, IDictionary<string, string> templateIdToPartnerId) => _chartboostCoreGetUsercentricsAdapterWithDPS(dpsName, options.SettingsId);
        
        [DllImport(IOSConstants.DLLImport)] private static extern IntPtr _chartboostCoreGetUsercentricsAdapter(string settingsId);
        [DllImport(IOSConstants.DLLImport)] private static extern IntPtr _chartboostCoreGetUsercentricsAdapterWithDPS(string dpsName, string settingsId);
    }
}
