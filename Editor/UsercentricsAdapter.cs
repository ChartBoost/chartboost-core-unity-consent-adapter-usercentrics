using System.Collections.Generic;
using System.Threading.Tasks;
using Chartboost.Core.Error;
using Chartboost.Core.Initialization;
using Chartboost.Json;
using Chartboost.Logging;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Core.Usercentrics.Editor
{
    /// <summary>
    /// Usercentrics Adapter Editor Class
    /// </summary>
    public class UsercentricsAdapter: Module
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void SetInstance()
        {
            if (!Application.isEditor)
                return;
            Chartboost.Core.Usercentrics.UsercentricsAdapter.InstanceType = typeof(UsercentricsAdapter);
        }
        
        public UsercentricsAdapter(UsercentricsOptions options, IDictionary<string, string> templateIdToPartnerId = null)
        {
            LogController.Log($"Creating Usercentrics Adapter.\nOptions:{JsonTools.SerializeObject(options)}\nTemplateIdToPartner: {JsonTools.SerializeObject(templateIdToPartnerId)}", LogLevel.Verbose);
            ModuleId = null;
            ModuleVersion = null;
        }
        
        public override string ModuleId { get; }
        public override string ModuleVersion { get; }

        protected override Task<ChartboostCoreError?> Initialize(ModuleConfiguration configuration) 
            => Task.FromResult<ChartboostCoreError?>(new ChartboostCoreError(999, "Usercentrics Adapter cannot be initialized on the Editor environment"));
    }
}