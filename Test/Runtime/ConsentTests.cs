using System.Collections;
using Chartboost.Core.Consent;
using Chartboost.Core.Initialization;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Chartboost.Core.Usercentrics.Tests
{
    public class ConsentTests 
    {
        private const string DateFormat = "yyyy/MM/dd HH:mm:ss.fff";
        private const float ConstDelayAfterInit = 2f;
        
        private static readonly UsercentricsOptions UsercentricsOptions = new UsercentricsOptions("DZbpqFbm-bHtwC");
        
        private static readonly UsercentricsAdapter UsercentricsAdapter = new UsercentricsAdapter("ChartboostCore", UsercentricsOptions);
        
        private readonly InitializableModule[] _modules = {
            UsercentricsAdapter
        };

        [SetUp]
        public void Setup()
        {
            ChartboostCore.Debug = true;
        }

        [UnityTest, Order(3)]
        public IEnumerator ModuleInitialization()
        {
            ChartboostCore.ModuleInitializationCompleted += AssertModule;

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
                    ChartboostCoreLogger.Log($"Exception: {result.Exception?.Message}");

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

            var sdkConfig = new SDKConfiguration(Application.identifier);
            ChartboostCore.Initialize(sdkConfig, _modules);
            yield return new WaitForSeconds(ConstDelayAfterInit);
            ChartboostCore.ModuleInitializationCompleted -= AssertModule;

            var task = ChartboostCore.Consent.SetConsentStatus(ConsentStatus.Unknown, ConsentStatusSource.Developer);
            yield return new WaitUntil(() => task.IsCompleted);
        }

        [UnityTest, Order(4)]
        public IEnumerator SetConsentStatusGranted()
        {
            yield return TestStatus(ConsentStatus.Granted);
        }

        [UnityTest, Order(5)]
        public IEnumerator SetConsentStatusDenied()
        { 
            yield return TestStatus(ConsentStatus.Denied);
        }
        
        [UnityTest, Order(6)]
        public IEnumerator SetConsentStatusUnknown()
        {
            yield return TestStatus(ConsentStatus.Unknown);
        }
        
        private static IEnumerator TestStatus(ConsentStatus status)
        {
            ChartboostCoreLogger.Log($"Initial - Target Status: {status}, Current Status {ChartboostCore.Consent.ConsentStatus}");
            
            var task = ChartboostCore.Consent.SetConsentStatus(status, ConsentStatusSource.Developer);
            
            yield return new WaitUntil(() => task.IsCompleted);
            
            var currentStatus = ChartboostCore.Consent.ConsentStatus;
            ChartboostCoreLogger.Log($"After - Target Status: {status}, Current Status {currentStatus}");
            Assert.IsTrue(task.Result);
            Assert.AreEqual(status, currentStatus);
        }
    }
}
