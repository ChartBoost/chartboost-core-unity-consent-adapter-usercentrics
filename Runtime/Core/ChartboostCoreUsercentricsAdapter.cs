using System.Collections.Generic;
using Chartboost.Core.Initialization;

namespace Chartboost.Core.Usercentrics
{
    public class ChartboostCoreUsercentricsAdapter : ChartboostCoreNativeInitializableModule<ChartboostCoreUsercentricsAdapter>
    {
        public ChartboostCoreUsercentricsOptions Options { get; }

        public ChartboostCoreUsercentricsAdapter(ChartboostCoreUsercentricsOptions options) : base(options)
        {
            Options = options;
        }

        public ChartboostCoreUsercentricsAdapter(Dictionary<string, object> config) : base(config) { }
    }
}
