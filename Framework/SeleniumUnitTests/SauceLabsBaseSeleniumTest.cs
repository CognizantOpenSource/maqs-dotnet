﻿using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SeleniumUnitTests
{
    [ExcludeFromCodeCoverage]
    public class SauceLabsBaseSeleniumTest : BaseSeleniumTest
    {
        private static readonly string BuildDate = DateTime.Now.ToString("MMddyyyy hhmmss");

        protected override IWebDriver GetBrowser()
        {
            if (string.Equals(Config.GetValueForSection(ConfigSection.SeleniumMaqs, "RunOnSauceLabs"), "YES", StringComparison.OrdinalIgnoreCase))
            {
                var name = this.TestContext.FullyQualifiedTestClassName + "." + this.TestContext.TestName;
                var options = SeleniumConfig.GetRemoteCapabilitiesAsObjects();

                var sauceOptions = options["sauce:options"] as Dictionary<string, object>;
                sauceOptions.Add("screenResolution", "1280x1024");
                sauceOptions.Add("build", string.IsNullOrEmpty(Environment.GetEnvironmentVariable("SAUCE_BUILD_NAME")) ? BuildDate : Environment.GetEnvironmentVariable("SAUCE_BUILD_NAME"));
                sauceOptions.Add("name", name);

                var browserOptions = new ChromeOptions
                {
                    PlatformName = "Windows 10",
                    BrowserVersion = "latest"
                };

                browserOptions.SetDriverOptions(options);

                var remoteCapabilities = browserOptions.ToCapabilities();

                return new RemoteWebDriver(new Uri(Config.GetValueForSection(ConfigSection.SeleniumMaqs, "HubUrl")), remoteCapabilities, SeleniumConfig.GetCommandTimeout());
            }

            return base.GetBrowser();
        }

        [TestCleanup]
        public void Cleanup()
        {
            var passed = this.GetResultType() == CognizantSoftvision.Maqs.Utilities.Logging.TestResultType.PASS;

            if (string.Equals(Config.GetValueForSection(ConfigSection.SeleniumMaqs, "RunOnSauceLabs"), "YES", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    ((IJavaScriptExecutor)this.WebDriver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
                }
                catch (Exception e)
                {
                    this.Log.LogMessage(CognizantSoftvision.Maqs.Utilities.Logging.MessageType.WARNING, "Failed to set Sauce Result because: " + e.Message);
                }
            }
            base.MaqsTeardown();
        }
    }
}
