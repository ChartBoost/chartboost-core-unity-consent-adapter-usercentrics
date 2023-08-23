using Chartboost.Core.Initialization;

namespace Chartboost.Core.Usercentrics
{
    public class UsercentricsAdapter : NativeInitializableModule<UsercentricsAdapter>
    {
        protected override string DefaultModuleId => "usercentrics";
        protected override string DefaultModuleVersion => "0.1.0";

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
