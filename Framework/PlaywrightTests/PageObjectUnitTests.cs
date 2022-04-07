﻿//-----------------------------------------------------
// <copyright file="PageObjectUnitTests.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Test the base Playwright page object model</summary>
//-----------------------------------------------------

using CognizantSoftvision.Maqs.BasePlaywrightTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace PlaywrightTests
{
    /// <summary>
    /// Test the base Playwright page object model
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    [TestCategory(TestCategories.Playwright)]
    public class PageObjectUnitTests : BasePlaywrightTest
    {
        /// <summary>
        /// Setup test Playwright page model
        /// </summary>
        [TestInitialize]
        public void CreatePlaywrightPageModel()
        {
            this.PageDriver.Goto(PageModel.Url);
            this.TestObject.SetObject("pom", new PageModel(this.TestObject));
        }

        /// <summary>
        /// Verify test object is the same
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Playwright)]
        public void PageModelTestObject()
        {
            Assert.AreEqual(this.TestObject, this.getPageModel().GetTestObject());
        }

        /// <summary>
        /// Verify web driver is the same
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Playwright)]
        public void PageModelWebDriver()
        {
            Assert.AreEqual(this.PageDriver, this.getPageModel().GetPageDriver());
        }

        /// <summary>
        /// Verify logger is the same
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Playwright)]
        public void PageModelLogger()
        {
            Assert.AreEqual(this.Log, this.getPageModel().GetLogger());
        }

        /// <summary>
        /// Verify perf timer collection is the same
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Playwright)]
        public void PageModelPerfTimerCollection()
        {
            Assert.AreEqual(this.PerfTimerCollection, this.getPageModel().GetPerfTimerCollection());
        }

        /// <summary>
        /// Verify we can override the page object webdriver
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Playwright)]
        public void OverridePageObjectWebdriver()
        {
            try
            {
                var oldWebDriver = this.getPageModel().GetPageDriver();
                this.getPageModel().OverridePageDriver(PageDriverFactory.GetDefaultPageDriver());

                Assert.AreNotEqual(oldWebDriver, this.getPageModel().GetPageDriver(), "The page driver was not updated");
            }
            finally
            {
                this.getPageModel().GetPageDriver()?.Close();
            }
        }

        /// <summary>
        /// Do lazy elements respect overrides
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Playwright)]
        public void LazyRespectOverride()
        {
            // Define new named driver
            this.ManagerStore.AddOrOverride("OtherDriver", new PlaywrightDriverManager(() =>
                 PageDriverFactory.GetDefaultPageDriver(), this.TestObject));
            var otherDriver = this.ManagerStore.GetDriver<PageDriver>("OtherDriver");

            var model1 = this.getPageModel();
            var model2 = new PageModelOther(this.TestObject, otherDriver);
            model2.OpenPage();

            // Make sure the page are properly loading using the different web drivers
            Assert.IsTrue(model1.FlowerTablePlaywrightElement.IsVisible(), "Model one may not be on the right page");
            Assert.IsTrue(model2.LoadedPlaywrightElement.IsEventualyVisible(), "Model two may not be on the right page");

            // Swap the drivers
            model1.OverridePageDriver(otherDriver);
            model2.OverridePageDriver(this.PageDriver);

            // Make sure the page are properly loading using the different web drivers
            Assert.IsFalse(model1.FlowerTablePlaywrightElement.IsVisible(), "Model one should have changed pages");
            Assert.IsFalse(model2.LoadedPlaywrightElement.IsVisible(), "Model two should have changed pages");

            // Now reload the pages
            model1.OpenPage();
            model2.OpenPage();

            // Make sure the page are properly loading using the different web drivers
            Assert.IsTrue(model1.FlowerTablePlaywrightElement.IsVisible(), "Model one may not be on the right page");
            Assert.IsTrue(model2.LoadedPlaywrightElement.IsEventualyVisible(), "Model two may not be on the right page");
        }

        /// <summary>
        /// Get the Selenim page object
        /// </summary>
        /// <returns>The page model</returns>
        private PageModel getPageModel()
        {
            return this.TestObject.Objects["pom"] as PageModel;
        }
    }
}