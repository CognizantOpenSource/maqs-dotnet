//-----------------------------------------------------
// <copyright file="SeleniumPageObjectUnitTests.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Test the base Selenium page object model</summary>
//-----------------------------------------------------
using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest.Extensions;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Diagnostics.CodeAnalysis;

namespace SeleniumUnitTests
{
    /// <summary>
    /// Test the base selenium page object model
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SeleniumPageObjectUnitTests : BaseSeleniumTest
    {
        /// <summary>
        /// Setup test Selenium page model
        /// </summary>
        [TestInitialize]
        public void CreateSeleniumPageModel()
        {
            this.WebDriver.Navigate().GoToUrl(SeleniumPageModel.Url);
            this.WebDriver.Wait().ForPageLoad();
            this.TestObject.SetObject("pom", new SeleniumPageModel(this.TestObject));
        }

        /// <summary>
        /// Verify test object is the same
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void PageModelTestObject()
        {
            Assert.AreEqual(this.TestObject, this.getPageModel().GetTestObject());
        }

        /// <summary>
        /// Verify web driver is the same
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void PageModelWebDriver()
        {
            Assert.AreEqual(this.WebDriver, this.getPageModel().GetWebDriver());
        }

        /// <summary>
        /// Verify logger is the same
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void PageModelLogger()
        {
            Assert.AreEqual(this.Log, this.getPageModel().GetLogger());
        }

        /// <summary>
        /// Verify perf timer collection is the same
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void PageModelPerfTimerCollection()
        {
            Assert.AreEqual(this.PerfTimerCollection, this.getPageModel().GetPerfTimerCollection());
        }

        /// <summary>
        /// Verify existing lazy element is returned from element store
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void GetLazyElementReturnsExistingElement()
        {
            var lazyElement = this.getPageModel().FlowerTableLazyElement;
            lazyElement.GetRawExistingElement();
            Assert.AreEqual(lazyElement.CachedElement, this.getPageModel().FlowerTableLazyElement.CachedElement);
        }

        /// <summary>
        /// Verify existing lazy element with parent is returned from element store
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void GetLazyElementReturnsExistingElementWithParent()
        {
            var lazyElement = this.getPageModel().FlowerTableCaptionWithParent;
            lazyElement.GetRawExistingElement();
            Assert.AreEqual(lazyElement.CachedElement, this.getPageModel().FlowerTableCaptionWithParent.CachedElement);
        }

        /// <summary>
        /// Verify we can override the page object webdriver
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void OverridePageObjectWebdriver()
        {
            try
            {
                var oldWebDriver = this.getPageModel().GetWebDriver();
                this.getPageModel().OverrideWebDriver(WebDriverFactory.GetDefaultBrowser());

                Assert.AreNotEqual(oldWebDriver, this.getPageModel().GetWebDriver(), "The webdriver was not updated");
            }
            finally
            {
                this.getPageModel().GetWebDriver()?.KillDriver();
            }
        }

        /// <summary>
        /// Do lazy elements respect overrides
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void LazyRespectOverride()
        {
            // Define new named driver
            this.ManagerStore.AddOrOverride("OtherDriver", new SeleniumDriverManager(() =>
                 WebDriverFactory.GetDefaultBrowser(), this.TestObject));
            var otherDriver = this.ManagerStore.GetDriver<IWebDriver>("OtherDriver");

            var model1 = this.getPageModel();
            var model2 = new SeleniumPageModelOther(this.TestObject, otherDriver);
            model2.OpenPage();

            // Make sure the page are properly loading using the different web drivers
            Assert.IsTrue(model1.FlowerTableLazyElement.Exists, "Model one may not be on the right page");
            Assert.IsTrue(model2.LoadedLazyElement.Exists, "Model two may not be on the right page");

            // Swap the drivers
            model1.OverrideWebDriver(otherDriver);
            model2.OverrideWebDriver(this.WebDriver);

            // Make sure the page are properly loading using the different web drivers
            Assert.IsFalse(model1.FlowerTableLazyElement.ExistsNow, "Model one should have changed pages");
            Assert.IsFalse(model2.LoadedLazyElement.ExistsNow, "Model two should have changed pages");

            // Now reload the pages
            model1.OpenPage();
            model2.OpenPage();

            // Make sure the page are properly loading using the different web drivers
            Assert.IsTrue(model1.FlowerTableLazyElement.Exists, "Model one may not be on the right page");
            Assert.IsTrue(model2.LoadedLazyElement.Exists, "Model two may not be on the right page");
        }

        /// <summary>
        /// Get the Selenim page object
        /// </summary>
        /// <returns>The page model</returns>
        private SeleniumPageModel getPageModel()
        {
            return this.TestObject.Objects["pom"] as SeleniumPageModel;
        }
    }
}
