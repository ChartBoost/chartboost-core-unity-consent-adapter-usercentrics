using System.Collections.Generic;
using System.Collections.ObjectModel;
using Chartboost.Core.Initialization;
using Chartboost.Logging;
using UnityEngine.Scripting;

namespace Chartboost.Core.Usercentrics
{
    /// <summary>
    /// 
    /// </summary>
    public class UsercentricsAdapter : NativeModuleWrapper<UsercentricsAdapter>
    {
        protected override string DefaultModuleId => "usercentrics";
        protected override string DefaultModuleVersion => "1.0.8";

        private readonly Dictionary<string, string> _defaultTemplateIdToPartnerId = new()
        {
            { "J64M6DKwx", "adcolony" },
            { "r7rvuoyDz", "admob" },
            { "IUyljv4X5", "amazon_aps" },
            { "fHczTMzX8", "applovin" },
            { "IEbRp3saT", "chartboost" },
            { "H17alcVo_iZ7", "fyber" },
            { "S1_9Vsuj-Q", "google_googlebidding" },
            { "ROCBK21nx", "hyprmx" },
            { "ykdq8J5a9MExGT", "inmobi" },
            { "VPSyZyTbYPSHpF", "mobilefuse" },
            { "9dchbL797", "ironsource" },
            { "ax0Nljnj2szF_r", "facebook" },
            { "E6AgqirYV", "mintegral" },
            { "HWSNU_Ll1", "pangle" },
            { "B1DLe54jui-X", "tapjoy" },
            { "hpb62D82I", "unity" },
            { "5bv4OvSwoXKh-G", "verve" },
            { "jk3jF2tpw", "vungle" },
            { "EMD3qUMa8", "vungle" }
        };

        /// <summary>
        /// Usercentrics CMP configuration.
        /// </summary>
        public UsercentricsOptions Options { get; }

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyDictionary<string, string> TemplateIdToPartnerId { get; }

        [Preserve]
        public UsercentricsAdapter(UsercentricsOptions options, IDictionary<string, string> templateIdToPartnerId = null) : base(options, templateIdToPartnerId)
        {
            Options = options;
            if (templateIdToPartnerId == null)
                TemplateIdToPartnerId = _defaultTemplateIdToPartnerId;
            else
            {
                foreach (var defaultTemplateId in _defaultTemplateIdToPartnerId)
                {
                    var key = defaultTemplateId.Key;
                    var value = defaultTemplateId.Value;
                    if (!templateIdToPartnerId.TryAdd(key, value)) 
                        LogController.Log($"Key: {key} is part of default template ids", LogLevel.Debug);
                }
                TemplateIdToPartnerId = new ReadOnlyDictionary<string, string>(templateIdToPartnerId);
            }
        }
    }
}
