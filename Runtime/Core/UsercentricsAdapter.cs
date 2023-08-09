using System.Collections.Generic;
using Chartboost.Core.Initialization;

namespace Chartboost.Core.Usercentrics
{
    public class UsercentricsAdapter : NativeInitializableModule<UsercentricsAdapter>
    {
        public string DPSName { get; } = "ChartboostCore";
        public UsercentricsOptions Options { get; }

        public UsercentricsAdapter(string dpsName, UsercentricsOptions options) : base(dpsName, options)
        {
            DPSName = dpsName;
            Options = options;
        }

        public UsercentricsAdapter(Dictionary<string, object> config) : base(config) { }
    }
}
