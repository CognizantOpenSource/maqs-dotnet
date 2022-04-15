//-----------------------------------------------------
// <copyright file="WebDriverFactoryUnitTests.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Test the WebDriverFactory</summary>
//-----------------------------------------------------
using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SeleniumUnitTests
{
    /// <summary>
    /// Test the WebDriverFactory class
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class WebDriverFactoryUnitTests
    {
        /// <summary>
        /// Verify SetProxySettings sets the proxy in the config
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void SetProxySettings()
        {
            ChromeOptions options = new ChromeOptions();
            options.SetProxySettings(Config.GetValueForSection(ConfigSection.SeleniumMaqs, "ProxyAddress"));

            Assert.IsNotNull(options.Proxy);
            Assert.AreEqual("http://localhost:8002", options.Proxy.HttpProxy);
            Assert.AreEqual("http://localhost:8002", options.Proxy.SslProxy);
        }

        /// <summary>
        /// Validating we can get the default IE options
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void GetDefaultIEOptions()
        {
            var options = WebDriverFactory.GetDefaultIEOptions();
            Assert.IsNotNull(options, "Was unable to retrieve options for IE Options");
        }

        /// <summary>
        /// Validating we can get the default Edge options
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void GetDefaultEdgeOptions()
        {
            var options = WebDriverFactory.GetDefaultEdgeOptions();
            Assert.IsNotNull(options, "Was unable to retrieve options for Edge Options");
        }

        /// <summary>
        /// Validating we can get the default Chrome options
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void GetDefaultChromeOptions()
        {
            var options = WebDriverFactory.GetDefaultChromeOptions();
            Assert.IsNotNull(options, "Was unable to retrieve options for Chrome Options");
        }

        /// <summary>
        /// Validating we can get the default remote options
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void GetDefaultRemoteOptions()
        {
            var options = WebDriverFactory.GetRemoteOptions(RemoteBrowserType.Chrome);
            Assert.IsNotNull(options, "Was unable to retrieve remote options");
        }

        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void GetRemoteOptionsDictStringString()
        {
            var result = WebDriverFactory.GetRemoteOptions(RemoteBrowserType.Edge, new Dictionary<string, string>() { { "OS", "Windows" } });
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void GetRemoteOptionsDictStringObject()
        {
            var result = WebDriverFactory.GetRemoteOptions(RemoteBrowserType.Edge, new Dictionary<string, object>() { { "OS", "Windows" } });
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void GetRemoteOptionsEmptyObjectDictionary()
        {
            Dictionary<string, object> test = null;
            var result = WebDriverFactory.GetRemoteOptions(RemoteBrowserType.Safari, string.Empty, string.Empty, test);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void GetRemoteOptionsEmptyStringDictionary()
        {
            Dictionary<string, string> test = null;
            var result = WebDriverFactory.GetRemoteOptions(RemoteBrowserType.Safari, string.Empty, string.Empty, test);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Check that retry works and will not get stuck in an infinite loop
        /// </summary>
        [TestMethod]
        [DynamicData(nameof(BrowserOptions), DynamicDataSourceType.Property)]
        public void AddOptionsToChromeDriver(DriverOptions options)
        {
            Dictionary<string, object> addOptions = new Dictionary<string, object>();
            addOptions.Add("TEST:COLON", "TEST");
            addOptions.Add("NoCOLON", "TEST");
            addOptions.Add("TESTJSON:COLON", "{\"KEY\":\"VALUE\"}'");
            addOptions.Add("NoCOLONJson", "{\"KEY\":\"VALUE\"}'");

            options.SetDriverOptions(addOptions);
            Assert.IsNotNull(options);
        }

        /// <summary>
        /// Gen options for each browser type
        /// </summary>
        public static IEnumerable<object[]> BrowserOptions
        {
            get
            {
                yield return new object[] { WebDriverFactory.GetDefaultChromeOptions() };
                yield return new object[] { WebDriverFactory.GetDefaultEdgeOptions() };
                yield return new object[] { WebDriverFactory.GetDefaultFirefoxOptions() };
                yield return new object[] { WebDriverFactory.GetDefaultIEOptions() };
                yield return new object[] { WebDriverFactory.GetDefaultRemoteOptions() };
            }
        }
    }
}
