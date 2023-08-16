using Chartboost.Core.Initialization;
using UnityEngine.Scripting;

namespace Chartboost.Core.Usercentrics
{
    public class UsercentricsAdapter : NativeInitializableModule<UsercentricsAdapter>
    {
        public string DPSName { get; }
        public UsercentricsOptions Options { get; }

        public UsercentricsAdapter(UsercentricsOptions options) : base(options)
        {
            DPSName = "ChartboostCore";
            Options = options;
        }

        public UsercentricsAdapter(string dpsName, UsercentricsOptions options) : base(dpsName, options)
        {
            DPSName = dpsName;
            Options = options;
        }
    }
}
