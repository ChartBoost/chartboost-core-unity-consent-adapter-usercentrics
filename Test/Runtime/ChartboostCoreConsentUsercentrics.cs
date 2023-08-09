using System.Collections;
using System.Collections.Generic;
using Chartboost.Core.Consent;
using Chartboost.Core.Initialization;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Chartboost.Core.Usercentrics.Tests
{
    public class ChartboostCoreConsentUsercentrics 
    {
        private const string DateFormat = "yyyy/MM/dd HH:mm:ss.fff";
        private const float ConstDelayAfterInit = 0.5f;
        
        private static ChartboostCoreUsercentricsOptions usercentricsOptions = new ChartboostCoreUsercentricsOptions("DZbpqFbm-bHtwC");
        
        private static readonly ChartboostCoreUsercentricsAdapter UsercentricsAdapter = new ChartboostCoreUsercentricsAdapter(usercentricsOptions);
        
        private readonly ChartboostCoreInitializableModule[] _modules = {
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

            void AssertModule(ChartboostCoreModuleInitializationResult result)
            {
                if (result == null)
                    return;
                
                if (result.Module != UsercentricsAdapter)
                    return;
                
                Assert.IsNotNull(result.ToJson());
                Assert.IsNotEmpty(result.ToJson());

                Assert.AreEqual(result.Module.ModuleId, UsercentricsAdapter.ModuleId);
                Assert.AreEqual(result.Module.ModuleVersion, UsercentricsAdapter.ModuleVersion);

                var settingsId = usercentricsOptions.SettingsId;

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

            var sdkConfig = new ChartboostCoreSDKConfiguration(Application.identifier);
            ChartboostCore.Initialize(sdkConfig, _modules);
            yield return new WaitForSeconds(ConstDelayAfterInit);
            var resetConsent = ChartboostCore.Consent.SetConsentStatus(ChartboostCoreConsentStatus.Unknown, ChartboostCoreConsentStatusSource.User);
            yield return new WaitUntil(() => resetConsent.IsCompleted);
            ChartboostCore.ModuleInitializationCompleted -= AssertModule;
        }
        
        [UnityTest, Order(4)]
        public IEnumerator SetConsentStatusGranted()
        {
            yield return TestStatus(ChartboostCoreConsentStatus.Granted);
        }

        [UnityTest, Order(5)]
        public IEnumerator SetConsentStatusDenied()
        { 
            yield return TestStatus(ChartboostCoreConsentStatus.Denied);
        }
        
        [UnityTest, Order(6)]
        public IEnumerator SetConsentStatusUnknown()
        {
            yield return TestStatus(ChartboostCoreConsentStatus.Unknown);
        }
        
        private static IEnumerator TestStatus(ChartboostCoreConsentStatus testStatus)
        {
            // var callback = false;
            // ChartboostCore.Consent.ConsentStatusChange += AssertStatus;
            ChartboostCoreLogger.Log($"Target Status: {testStatus}, Current Status {ChartboostCore.Consent.ConsentStatus}");
            
            var task = ChartboostCore.Consent.SetConsentStatus(testStatus, ChartboostCoreConsentStatusSource.User);
            
            yield return new WaitUntil(() => task.IsCompleted);
            // yield return new WaitUntil(() => callback);
            
            var consentStatus = ChartboostCore.Consent.ConsentStatus;
            ChartboostCoreLogger.Log($"After - Target Status: {testStatus}, Current Status {ChartboostCore.Consent.ConsentStatus}");
            Assert.IsTrue(task.Result);
            Assert.AreEqual(testStatus, consentStatus);

            void AssertStatus(ChartboostCoreConsentStatus status)
            {
                Assert.AreEqual(status, testStatus);
                var stat = ChartboostCore.Consent.ConsentStatus;
                Assert.AreEqual(status, stat);
                ChartboostCoreLogger.Log($"Target Status: {testStatus}, New Status: {status}, ConsentStatus: {stat}");
                ChartboostCore.Consent.ConsentStatusChange -= AssertStatus;
                // callback = true;
            }
        }
    }
}
