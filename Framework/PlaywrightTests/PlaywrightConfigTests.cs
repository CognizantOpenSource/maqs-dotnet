//--------------------------------------------------
// <copyright file="PlaywrightConfigTests.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Test class for config files</summary>
//--------------------------------------------------

using CognizantSoftvision.Maqs.BasePlaywrightTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace PlaywrightTests
{
    /// <summary>
    /// Test class
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    [DoNotParallelize]
    [TestCategory(TestCategories.Playwright)]
    public class PlaywrightConfigTests
    {
        /// <summary>
        /// Clear all configuration overrides
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            Config.ClearOverrides();
        }

        /// <summary>
        /// Get expected WebBase configuration
        /// </summary>
        [TestMethod]
        public void GetBrowser()
        {
            Assert.AreEqual("Chrome", PlaywrightConfig.GetBrowserName());
        }

        [DataTestMethod]
        [DataRow("Chrome")]
        [DataRow("Chromium")]
        [DataRow("Firefox")]
        [DataRow("Edge")]
        [DataRow("Webkit")]
        public void ConfigBrowserName(string browserName)
        {
            Config.AddTestSettingValue("Browser", browserName, ConfigSection.PlaywrightMaqs);
            Assert.AreEqual(browserName, PlaywrightConfig.GetBrowserName());
        }

        [DataTestMethod]
        [DataRow("Chromium", PlaywrightBrowser.Chromium)]
        [DataRow("Firefox", PlaywrightBrowser.Firefox)]
        [DataRow("Edge", PlaywrightBrowser.Edge)]
        [DataRow("Webkit", PlaywrightBrowser.Webkit)]
        [DataRow(null, PlaywrightBrowser.Chrome)]
        [DataRow("Chrome", PlaywrightBrowser.Chrome)]
        public void ConfigBrowserEnum(string browser, PlaywrightBrowser browserEnum)
        {
            Config.AddTestSettingValue("Browser", browser, ConfigSection.PlaywrightMaqs);
            Assert.AreEqual(browserEnum, PlaywrightConfig.GetBrowserType());
        }

        /// <summary>
        /// Make sure error correct error is thrown if we use a bad browser name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConfigBadBrowserName()
        {
            Config.AddTestSettingValue("Browser", "IE", ConfigSection.PlaywrightMaqs);
            var type = PlaywrightConfig.GetBrowserType();
            Assert.Fail($"IE returned type: {type}");
        }

        /// <summary>
        /// Get expected UseProxy configuration
        /// </summary>
        [TestMethod]
        public void GetUseProxy()
        {
            Assert.IsFalse(PlaywrightConfig.GetUseProxy());
        }

        /// <summary>
        /// Get expected proxy address configuration
        /// </summary>
        [TestMethod]
        public void GetProxyAddress()
        {
            Assert.AreEqual("http://localhost:8002", PlaywrightConfig.GetProxyAddress());
        }
    }
}
