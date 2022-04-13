//-----------------------------------------------------
// <copyright file="PlaywrightDriverManagerTests.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Test Playwright driver manager</summary>
//-----------------------------------------------------
using CognizantSoftvision.Maqs.BasePlaywrightTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace FrameworkUnitTests
{
    /// <summary>
    /// Test driver manager
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    [TestCategory(TestCategories.Playwright)]
    public class PlaywrightDriverManagerTests : BasePlaywrightTest
    {
        /// <summary>
        /// Make we can update the store with a new manager using an IPage
        /// </summary>
        [TestMethod]
        public void RespectsNewIPageViaManager()
        {
            var newPage = GetNewPage();
            this.ManagerStore.AddOrOverride(new PlaywrightDriverManager(() => newPage, this.TestObject));
            Assert.AreEqual(newPage, this.PageDriver.AsyncPage);
        }

        /// <summary>
        /// Make we can update the drive with a IPage object
        /// </summary>
        [TestMethod]
        public void RespectsNewIPageViaOverride()
        {
            var newPage = GetNewPage();
            this.TestObject.OverridePageDriver(newPage);
            Assert.AreEqual(newPage, this.PageDriver.AsyncPage);
        }

        /// <summary>
        /// Make we can update the drive with a IPage function
        /// </summary>
        [TestMethod]
        public void RespectsNewIPageViaOverrideFunc()
        {
            var newPage = GetNewPage();
            this.TestObject.OverridePageDriver(() => newPage);
            Assert.AreEqual(newPage, this.PageDriver.AsyncPage);
        }

        /// <summary>
        /// Get a new IPage
        /// </summary>
        /// <returns>A new IPAge</returns>
        private static IPage GetNewPage()
        {
            return PageDriverFactory.GetPageDriverForBrowserWithDefaults(PlaywrightBrowser.Webkit).AsyncPage;
        }
    }
}
