using System.Collections.Generic;
using Chartboost.Core.Initialization;

namespace Chartboost.Core.Usercentrics
{
    public class UsercentricsAdapter : NativeInitializableModule<UsercentricsAdapter>
    {
        protected override string DefaultModuleId => "usercentrics";
        protected override string DefaultModuleVersion => "0.2.8-4";

        public string DPSName { get; }
        public UsercentricsOptions Options { get; }

        public IDictionary<string, string> TemplateIdToPartnerId { get; }

        public UsercentricsAdapter(UsercentricsOptions options, IDictionary<string, string> templateIdToPartnerId) : base(options, templateIdToPartnerId)
        {
            DPSName = "ChartboostCore";
            Options = options;
            TemplateIdToPartnerId = templateIdToPartnerId;
        }

        public UsercentricsAdapter(string dpsName, UsercentricsOptions options, IDictionary<string, string> templateIdToPartnerId) : base(dpsName, options, templateIdToPartnerId)
        {
            DPSName = dpsName;
            Options = options;
            TemplateIdToPartnerId = templateIdToPartnerId;
        }
    }
}
