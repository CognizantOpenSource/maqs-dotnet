//--------------------------------------------------
// <copyright file="PageDriverFactoryTests.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Test page driver factory</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BasePlaywrightTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace PlaywrightTests
{
    /// <summary>
    /// Page driver factory tests
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    [TestCategory(TestCategories.Playwright)]
    public class PageDriverFactoryTests
    {
        /// <summary>
        /// Check that we can connect to all browser types
        /// *Hold off on Edge as it is not natively on build server
        /// </summary>
        /// <param name="browserType"></param>
        [DataTestMethod]
        [DataRow(PlaywrightBrowser.Chrome)]
        [DataRow(PlaywrightBrowser.Chromium)]
        [DataRow(PlaywrightBrowser.Firefox)]
        [DataRow(PlaywrightBrowser.Webkit)]
        public void CanMakeAllBrowsers(PlaywrightBrowser browserType)
        {
            var browser = PageDriverFactory.GetBrowserWithDefaults(browserType);
            Assert.IsTrue(browser.IsConnected);
        }

        /// <summary>
        /// Test set check works as expected
        /// </summary>
        [TestMethod]
        public void BrowserWithNoContext()
        {
            var browser = PageDriverFactory.GetBrowserWithDefaults(PlaywrightBrowser.Chromium);
            Assert.AreEqual(0, browser.Contexts.Count);

            var pageDriver = PageDriverFactory.GetPageDriverFromBrowser(browser);
            Assert.IsFalse(pageDriver.AsyncPage.IsClosed);

        }
    }
}
