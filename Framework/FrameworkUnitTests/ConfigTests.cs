﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigTests.cs" company="Cognizant">
//   Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>
//   Class for config unit tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace FrameworkUnitTests
{
    /// <summary>
    /// The config unit tests.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ConfigTests
    {
        /// <summary>
        /// Open page test
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.UtilitiesCore)]
        public void Testconfig()
        {
            Config.DoesGeneralKeyExist("Log");
            Assert.AreEqual(true, Config.DoesGeneralKeyExist("Log"));
            Assert.AreEqual(false, Config.DoesGeneralKeyExist("Browser"));
            Assert.AreEqual(true, Config.DoesKeyExist("SeleniumMaqs", "Browser"));
            Assert.AreEqual("OnFail", Config.GetGeneralValue("Log", "NO"));
            Assert.AreEqual("HeadlessChrome", Config.GetValueForSection("SeleniumMaqs", "Browser", "NO"));
        }

        /// <summary>
        /// Gets a value from a string
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.UtilitiesCore)]
        public void GetValueWithString()
        {
            string value = Config.GetGeneralValue("WaitTime");
            Assert.AreEqual("100", value);
        }

        /// <summary>
        /// Gets a value with a string or default
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.UtilitiesCore)]
        public void GetValueWithStringAndDefault()
        {
            string value = Config.GetGeneralValue("DoesNotExist", "Default");
            Assert.AreEqual("Default", value);
        }

        /// <summary>
        /// Checks if a key exists
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.UtilitiesCore)]
        public void DoesKeyExist()
        {
            bool value = Config.DoesGeneralKeyExist("DoesNotExist");
            Assert.AreEqual(false, value);
        }

        /// <summary>
        ///  Verify simple override of an existing configuration
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.UtilitiesCore)]
        public void SimpleOverrideConfig()
        {
            // Simple override data
            string key = "SimpleOverride";
            string baseValue = Config.GetGeneralValue(key);
            string overrideValue = baseValue + "_Override";

            // Override the configuration
            var overrides = new Dictionary<string, string>
            {
                { key, overrideValue }
            };

            Config.AddTestSettingValues(overrides);

            // Make sure it worked
            Assert.AreEqual(overrideValue, Config.GetGeneralValue(key));
        }

        /// <summary>
        ///  Verify simple override of a new configuration
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.UtilitiesCore)]
        public void OverrideNewConfig()
        {
            string key = "AddNewKey";
            string value = "TestValue";

            // Make sure the new key is not present
            Assert.AreEqual(string.Empty, Config.GetGeneralValue(key));

            // Set the override
            var overrides = new Dictionary<string, string>
            {
                { key, value }
            };

            Config.AddTestSettingValues(overrides);

            // Make sure the override worked
            Assert.AreEqual(value, Config.GetGeneralValue(key));
        }

        /// <summary>
        /// Verify the remote Selenium section
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.UtilitiesCore)]
        public void ConfigSection()
        {
            Dictionary<string, string> remoteCapabilitySection = Config.GetSectionDictionary("RemoteSeleniumCapsMaqs");

            Assert.AreEqual("someName", remoteCapabilitySection["userName2"]);
            Assert.AreEqual("Some_Accesskey", remoteCapabilitySection["accessKey2"]);
        }

        /// <summary>
        ///  Verify complex configuration overrides
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.UtilitiesCore)]
        public void ComplexOverrideConfig()
        {
            // Define keys
            string key = "Override";
            string key2 = "Override2";

            // Get base key values
            string baseValue = Config.GetGeneralValue(key);
            string baseValue2 = Config.GetGeneralValue(key2);

            // Set override value
            string overrideValue = baseValue + "_Override";

            // Override first key value
            var overrides = new Dictionary<string, string>
            {
                { key, overrideValue }
            };

            Config.AddTestSettingValues(overrides);

            // The secondary override should fail as we already overrode it once
            Assert.AreEqual(overrideValue, Config.GetGeneralValue(key));

            // Try the override again, but this time tell the override to allow itself to be overrode
            overrideValue += "_SecondOverride";
            overrides = new Dictionary<string, string>
            {
                { key, overrideValue }
            };

            Config.AddGeneralTestSettingValues(overrides);

            // Make sure the force override worked
            Assert.AreEqual(overrideValue, Config.GetGeneralValue(key));

            // Make sure the value we didn't override was not affected
            Assert.AreEqual(baseValue2, Config.GetGeneralValue(key2));
        }
    }
}