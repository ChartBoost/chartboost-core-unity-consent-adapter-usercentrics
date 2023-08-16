using Chartboost.Core.Initialization;

namespace Chartboost.Core.Usercentrics
{
    public class UsercentricsAdapter : NativeInitializableModule<UsercentricsAdapter>
    {
        public string DPSName { get; }
        public UsercentricsOptions Options { get; }

        public UsercentricsAdapter(string dpsName, UsercentricsOptions options) : base(dpsName, options)
        {
            DPSName = dpsName;
            Options = options;
        }
    }
}
