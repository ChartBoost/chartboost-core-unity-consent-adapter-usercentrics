using System;
using System.Collections;
using System.Threading.Tasks;
using Chartboost.Core.Consent;
using Chartboost.Core.Initialization;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Chartboost.Core.Usercentrics.Tests
{
    public class ConsentTests 
    {
        private const string DateFormat = "yyyy/MM/dd HH:mm:ss.fff";
        private const float ConstDelayAfterInit = 2f;
        
        private static readonly UsercentricsOptions UsercentricsOptions = new UsercentricsOptions("");
        
        private static readonly UsercentricsAdapter UsercentricsAdapter = new UsercentricsAdapter("ChartboostCore", UsercentricsOptions);
        
        private readonly InitializableModule[] _modules = {
            UsercentricsAdapter
        };

        private static bool ShouldSucceedInitialization => !string.IsNullOrEmpty(UsercentricsOptions.SettingsId);

        [SetUp]
        public void Setup()
        {
            ChartboostCore.Debug = true;
        }
        
        
        [Test, Order(5)]
        public void GetConsentStatus()
        {
            var status = ChartboostCore.Consent.ConsentStatus;
            ChartboostCoreLogger.Log($"ConsentStatus: {Enum.GetName(typeof(ConsentStatus), status)}");
            Assert.IsNotNull(status);
        }

        [Test, Order(5)]
        public void GetConsents()
        {
            var contents = ChartboostCore.Consent.Consents;
            Assert.IsNotNull(contents);
            var asJson = JsonConvert.SerializeObject(contents);
            ChartboostCoreLogger.Log($"Consents as Json: {asJson}");
            Assert.IsNotNull(asJson);
            Assert.IsNotEmpty(asJson);
        }

        [UnityTest, Order(4)]
        public IEnumerator ModuleInitialization()
        {
            
            ChartboostCore.ModuleInitializationCompleted += AssertModule;
            ChartboostCore.Consent.ConsentModuleReady += AssertCallback;

            var fired = false;
            void AssertCallback() => fired = true;

            void AssertModule(ModuleInitializationResult result)
            {
                if (result == null)
                    return;
                
                if (result.Module != UsercentricsAdapter)
                    return;
                
                Assert.IsNotNull(result.ToJson());
                Assert.IsNotEmpty(result.ToJson());

                Assert.AreEqual(result.Module.ModuleId, UsercentricsAdapter.ModuleId);
                Assert.AreEqual(result.Module.ModuleVersion, UsercentricsAdapter.ModuleVersion);

                var settingsId = UsercentricsOptions.SettingsId;

                if (string.IsNullOrEmpty(settingsId))
                    ChartboostCoreLogger.Log($"Exception: {JsonConvert.SerializeObject(result.Error)}");

                Assert.IsNotNull(result.Start);
                Assert.IsNotNull(result.End);
                Assert.GreaterOrEqual(result.Duration, 0);
                ChartboostCoreLogger.Log($"Start: {result.Start.ToString(DateFormat)}");
                ChartboostCoreLogger.Log($"End: {result.End.ToString(DateFormat)}");
                ChartboostCoreLogger.Log($"Duration: {result.Duration}");
                ChartboostCoreLogger.Log($"Module Id: {result.Module.ModuleId}");
                ChartboostCoreLogger.Log($"Module Version: {result.Module.ModuleVersion}");
                ChartboostCoreLogger.Log($"--------");
            }

            Assert.AreEqual(ConsentStatus.Unknown, ChartboostCore.Consent.ConsentStatus);
            
            var sdkConfig = new SDKConfiguration(Application.identifier);
            ChartboostCore.Initialize(sdkConfig, _modules);
            yield return new WaitForSeconds(ConstDelayAfterInit);
            ChartboostCore.ModuleInitializationCompleted -= AssertModule;
            
            if (ShouldSucceedInitialization)
                yield return new WaitUntil(() => fired);
            ChartboostCore.Consent.ConsentModuleReady -= AssertCallback;
            ChartboostCoreLogger.Log($"Consent After Initialization: {ChartboostCore.Consent.ConsentStatus}");

            var resetConsent = ChartboostCore.Consent.ResetConsent();
            yield return new WaitUntil(() => resetConsent.IsCompleted);
            ChartboostCoreLogger.Log($"Consent After Reset: {ChartboostCore.Consent.ConsentStatus}");
        }

        [UnityTest, Order(5)]
        public IEnumerator GrantDeveloper()
        {
            yield return TestConsent(ChartboostCore.Consent.GrantConsent, ConsentStatus.Granted, ConsentStatusSource.Developer);
        }
        
        [UnityTest, Order(6)]
        public IEnumerator DenyDeveloper()
        { 
            yield return TestConsent(ChartboostCore.Consent.DenyConsent, ConsentStatus.Denied, ConsentStatusSource.Developer);
        }
        
        [UnityTest, Order(7)]
        public IEnumerator GrantUser()
        {
            yield return TestConsent(ChartboostCore.Consent.GrantConsent, ConsentStatus.Granted, ConsentStatusSource.User);
        }
        
        [UnityTest, Order(8)]
        public IEnumerator DenyUser()
        {
            yield return TestConsent(ChartboostCore.Consent.DenyConsent, ConsentStatus.Denied, ConsentStatusSource.User);
        }

        private IEnumerator TestConsent(Func<ConsentStatusSource, Task<bool>> func, ConsentStatus target, ConsentStatusSource source)
        {
            var task = func(source);
            yield return new WaitUntil(() => task.IsCompleted);
            var result = task.Result;
            if (ShouldSucceedInitialization)
                Assert.IsTrue(result);
            else
                Assert.IsFalse(result);
            ChartboostCoreLogger.Log($"Target: {target} Actual: {ChartboostCore.Consent.ConsentStatus}");
        }
    }
}
