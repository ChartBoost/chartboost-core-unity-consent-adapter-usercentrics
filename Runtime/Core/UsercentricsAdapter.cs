using System.Collections.Generic;
using Chartboost.Core.Initialization;

namespace Chartboost.Core.Usercentrics
{
    public class UsercentricsAdapter : NativeInitializableModule<UsercentricsAdapter>
    {
        public UsercentricsOptions Options { get; }

        public UsercentricsAdapter(UsercentricsOptions options) : base(options)
        {
            Options = options;
        }

        public UsercentricsAdapter(Dictionary<string, object> config) : base(config) { }
    }
}
