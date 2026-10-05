using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Chartboost.Core.Usercentrics.Tests
{
    public class UsercentricsAdapterTests
    {
        private static readonly UsercentricsOptions Options = new("settings-id");

        private Type _initialInstanceType;

        // No native adapter: these tests cover the C# template map only.
        [SetUp]
        public void SetUp()
        {
            _initialInstanceType = UsercentricsAdapter.InstanceType;
            UsercentricsAdapter.InstanceType = null;
        }

        [TearDown]
        public void TearDown() => UsercentricsAdapter.InstanceType = _initialInstanceType;

        [Test]
        public void WithoutAMapItUsesTheDefaultTemplates()
        {
            var adapter = new UsercentricsAdapter(Options);

            Assert.AreEqual("settings-id", adapter.Options.SettingsId);
            Assert.AreEqual("admob", adapter.TemplateIdToPartnerId["r7rvuoyDz"]);
            Assert.AreEqual("usercentrics", adapter.ModuleId);
        }

        [Test]
        public void ACustomMapIsMergedWithTheDefaultsAndWins()
        {
            var custom = new Dictionary<string, string> { { "custom-template", "my_partner" }, { "r7rvuoyDz", "my_admob" } };

            var adapter = new UsercentricsAdapter(Options, custom);

            Assert.AreEqual("my_partner", adapter.TemplateIdToPartnerId["custom-template"]);
            Assert.AreEqual("my_admob", adapter.TemplateIdToPartnerId["r7rvuoyDz"]);
            Assert.AreEqual("chartboost", adapter.TemplateIdToPartnerId["IEbRp3saT"]);
        }

        // Native compares template ids ordinally, so the C# view does too, whatever the caller's comparer.
        [Test]
        public void TemplateIdsCompareOrdinallyLikeNative()
        {
            var custom = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) { { "R7RVUOYDZ", "my_admob" } };

            var adapter = new UsercentricsAdapter(Options, custom);

            Assert.AreEqual("my_admob", adapter.TemplateIdToPartnerId["R7RVUOYDZ"]);
            Assert.AreEqual("admob", adapter.TemplateIdToPartnerId["r7rvuoyDz"]);
        }

        [Test]
        public void TheCallersMapIsNotChanged()
        {
            var custom = new Dictionary<string, string> { { "custom-template", "my_partner" } };

            _ = new UsercentricsAdapter(Options, custom);

            CollectionAssert.AreEquivalent(new Dictionary<string, string> { { "custom-template", "my_partner" } }, custom);
        }
    }
}
