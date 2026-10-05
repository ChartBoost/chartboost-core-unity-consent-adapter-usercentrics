using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Chartboost.Core.Consent;
using Chartboost.Core.Initialization;
using Chartboost.Json;
using Chartboost.Logging;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Chartboost.Core.Usercentrics.Tests
{
    public class ConsentTests 
    {
        private const string DateFormat = "yyyy/MM/dd HH:mm:ss.fff";

        private const float TimeoutSeconds = 10f;

        private const string InvalidSettingsId = "abcdefg12345";
        
        private static readonly UsercentricsOptions UsercentricsOptions = new(InvalidSettingsId);
        
        private static readonly UsercentricsAdapter UsercentricsAdapter = new(UsercentricsOptions, new Dictionary<string, string>());
        
        private readonly List<Module> _modules = new() {
            UsercentricsAdapter
        };

        private static bool UsercentricsShouldWork => UsercentricsAdapter.Options.SettingsId != InvalidSettingsId;

        private LogLevel _initialLogLevel;

        [SetUp]
        public void Setup()
        {
            _initialLogLevel = LogController.LoggingLevel;
            LogController.LoggingLevel = LogLevel.Debug;
        }

        [TearDown]
        public void TearDown() => LogController.LoggingLevel = _initialLogLevel;

        [Test, Order(5)]
        public void GetConsents()
        {
            var contents = ChartboostCore.Consent.Consents;
            Assert.IsNotNull(contents);
            var asJson = JsonTools.SerializeObject(contents);
            LogController.Log($"Consents as Json: {asJson}", LogLevel.Debug);
            Assert.IsNotNull(asJson);
            Assert.IsNotEmpty(asJson);
        }

        [UnityTest, Order(4)]
        public IEnumerator ModuleInitialization()
        {
            ChartboostCore.ModuleInitializationCompleted += AssertModule;
            ChartboostCore.Consent.ConsentModuleReadyWithInitialConsents += AssertCallback;
            var moduleReady = false;
            void AssertCallback(IReadOnlyDictionary<ConsentKey, ConsentValue> initialConsents) => moduleReady = true;

            var assertedModule = false;
            void AssertModule(ModuleInitializationResult result)
            {
                if (result.ModuleId != UsercentricsAdapter.ModuleId)
                    return;
                
                Assert.IsNotNull(result.ToJson());
                Assert.IsNotEmpty(result.ToJson());

                Assert.AreEqual(result.ModuleId, UsercentricsAdapter.ModuleId);
                Assert.AreEqual(result.ModuleVersion, UsercentricsAdapter.ModuleVersion);

                var settingsId = UsercentricsOptions.SettingsId;

                if (string.IsNullOrEmpty(settingsId))
                    LogController.Log($"Exception: {JsonTools.SerializeObject(result.Error)}", LogLevel.Debug);

                Assert.IsNotNull(result.Start);
                Assert.IsNotNull(result.End);
                Assert.GreaterOrEqual(result.Duration, 0);
                LogController.Log($"Start: {result.Start.ToString(DateFormat)}", LogLevel.Debug);
                LogController.Log($"End: {result.End.ToString(DateFormat)}", LogLevel.Debug);
                LogController.Log($"Duration: {result.Duration}", LogLevel.Debug);
                LogController.Log($"Module Id: {result.ModuleId}", LogLevel.Debug);
                LogController.Log($"Module Version: {result.ModuleVersion}", LogLevel.Debug);
                LogController.Log($"--------", LogLevel.Debug);
                assertedModule = true;
            }
            
            try
            {
                var sdkConfig = new SDKConfiguration(Application.identifier, _modules, new HashSet<string> { "chartboost_mediation"});
                ChartboostCore.Initialize(sdkConfig);
                yield return WaitUntil(() => assertedModule);
                if (UsercentricsShouldWork)
                    yield return WaitUntil(() => moduleReady);
            }
            finally
            {
                ChartboostCore.ModuleInitializationCompleted -= AssertModule;
                ChartboostCore.Consent.ConsentModuleReadyWithInitialConsents -= AssertCallback;
            }

            var resetConsent = ChartboostCore.Consent.ResetConsent();
            yield return WaitUntil(() => resetConsent.IsCompleted);
        }

        [UnityTest, Order(5)]
        public IEnumerator GrantDeveloper()
        {
            yield return TestConsent(ChartboostCore.Consent.GrantConsent, ConsentSource.Developer);
        }
        
        [UnityTest, Order(6)]
        public IEnumerator DenyDeveloper()
        { 
            yield return TestConsent(ChartboostCore.Consent.DenyConsent, ConsentSource.Developer);
        }
        
        [UnityTest, Order(7)]
        public IEnumerator GrantUser()
        {
            yield return TestConsent(ChartboostCore.Consent.GrantConsent, ConsentSource.User);
        }
        
        [UnityTest, Order(8)]
        public IEnumerator DenyUser()
        {
            yield return TestConsent(ChartboostCore.Consent.DenyConsent, ConsentSource.User);
        }

        private static IEnumerator TestConsent(Func<ConsentSource, Task<bool>> func, ConsentSource source)
        {
            var task = func(source);
            yield return WaitUntil(() => task.IsCompleted);
            var result = task.Result;
            if (UsercentricsShouldWork)
                Assert.IsTrue(result);
            else
                Assert.IsFalse(result);
        }

        private static IEnumerator WaitUntil(Func<bool> condition)
        {
            var deadline = Time.realtimeSinceStartup + TimeoutSeconds;
            while (!condition() && Time.realtimeSinceStartup < deadline)
                yield return null;
            Assert.IsTrue(condition(), $"timed out after {TimeoutSeconds}s");
        }
    }
}
