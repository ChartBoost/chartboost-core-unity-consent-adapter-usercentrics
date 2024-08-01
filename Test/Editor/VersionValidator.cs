using Chartboost.Editor;
using NUnit.Framework;

namespace Chartboost.Core.Usercentrics.Tests.Editor
{
    public class VersionValidator
    {
        private const string UnityPackageManagerPackageName = "com.chartboost.core.consent.usercentrics";
        private const string NuGetPackageName = "Chartboost.CSharp.Core.Unity.Consent.Usercentrics";
        
        [Test]
        public void ValidateVersion() 
            => VersionCheck.ValidateVersions(UnityPackageManagerPackageName, NuGetPackageName);
    }
}
