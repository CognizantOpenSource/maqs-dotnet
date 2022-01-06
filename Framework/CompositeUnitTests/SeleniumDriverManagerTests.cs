﻿//--------------------------------------------------
// <copyright file="SeleniumDriverManagerTests.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Selenium driver store tests</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest.Extensions;
using CognizantSoftvision.Maqs.BaseWebServiceTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Net.Http;

namespace CompositeUnitTests
{
    /// <summary>
    /// Test the Selenium driver store
    /// </summary>
    [TestClass]
    [TestCategory(TestCategories.Selenium)]
    public class SeleniumDriverManagerTests : BaseSeleniumTest
    {
        /// <summary>
        /// Make sure we can override the driver
        /// </summary>
        [TestMethod]
        public void CanOverrideWebDriver()
        {
            IWebDriver tempDriver = WebDriverFactory.GetBrowserWithDefaultConfiguration(BrowserType.HeadlessChrome);
            this.WebDriver = tempDriver;

            Assert.AreEqual(this.TestObject.WebDriver.GetLowLevelDriver(), tempDriver.GetLowLevelDriver());
        }

        /// <summary>
        /// Make sure we can override the driver with a func
        /// </summary>
        [TestMethod]
        public void CanOverrideWithFunc()
        {
            int oldHash = this.WebDriver.GetLowLevelDriver().GetHashCode();

            this.TestObject.OverrideWebDriver(() => WebDriverFactory.GetBrowserWithDefaultConfiguration(BrowserType.HeadlessChrome));

            Assert.AreNotEqual(oldHash, this.WebDriver.GetLowLevelDriver().GetHashCode());
        }

        /// <summary>
        /// Check that we can add multiples of the same driver type, provided we use a key
        /// </summary>
        [TestMethod]
        public void CanUseMultiple()
        {
            SeleniumDriverManager newDriver = new SeleniumDriverManager(() => WebDriverFactory.GetBrowserWithDefaultConfiguration(BrowserType.HeadlessChrome), this.TestObject);
            this.ManagerStore.Add("test", newDriver);

            Assert.AreNotEqual(this.TestObject.WebDriver.GetLowLevelDriver(), this.ManagerStore.GetDriver<IWebDriver>("test").GetLowLevelDriver());
        }

        /// <summary>
        /// Check Selenium test object with driver and log maps correctly
        /// </summary>
        [TestMethod]
        public void TestObjectWithExistingDriverAndLog()
        {
            SeleniumTestObject newTestObject = new SeleniumTestObject(this.WebDriver, this.Log, "TEST");

            Assert.AreEqual(this.Log, newTestObject.Log);
            Assert.AreNotEqual(this.SoftAssert, newTestObject.SoftAssert);
            Assert.AreEqual("TEST", newTestObject.PerfTimerCollection.TestName);
            Assert.AreNotEqual(this.PerfTimerCollection, newTestObject.PerfTimerCollection);
            Assert.AreNotEqual(this.TestObject.Values, newTestObject.Values);
            Assert.AreNotEqual(this.TestObject.Objects, newTestObject.Objects);
            Assert.AreNotEqual(this.TestObject.ManagerStore, newTestObject.ManagerStore);
            Assert.AreNotEqual(this.TestObject.AssociatedFiles, newTestObject.AssociatedFiles);
        }

        /// <summary>
        /// Make sure the test object driver is the same as the one in the driver store
        /// </summary>
        [TestMethod]
        public void SeleniumWebDriverInDriverStore()
        {
            Assert.AreEqual(this.TestObject.WebDriver, this.TestObject.GetDriverManager<SeleniumDriverManager>().Get());
        }

        /// <summary>
        /// Make sure we can add different driver types
        /// </summary>
        [TestMethod]
        public void MixedStoreTypes()
        {
            this.TestObject.AddDriverManager(new WebServiceDriverManager(() => new HttpClient(), this.TestObject));

            Assert.IsNotNull(this.TestObject.GetDriverManager<SeleniumDriverManager>(), "Expected a Selenium driver store");
            Assert.IsNotNull(this.TestObject.GetDriverManager<WebServiceDriverManager>(), "Expected a web service driver store");
        }

        /// <summary>
        /// Make sure separate interactions go to separate drivers
        /// </summary>
        [TestMethod]
        public void SeparateInteractions()
        {
            SeleniumDriverManager newDriver = new SeleniumDriverManager(() => WebDriverFactory.GetBrowserWithDefaultConfiguration(BrowserType.HeadlessChrome), this.TestObject);
            newDriver.GetWebDriver().Navigate().GoToUrl("https://GlobalAutomation.azurewebsites.net/");

            this.ManagerStore.Add("test", newDriver);

            this.TestObject.WebDriver.Navigate().GoToUrl("https://GlobalAutomation.azurewebsites.net/Automation");

            Assert.AreNotEqual(this.TestObject.WebDriver.Url, this.ManagerStore.GetManager<SeleniumDriverManager>("test").GetWebDriver().Url);
        }

        /// <summary>
        /// Make sure separate lazy elements can interactions with separate drivers
        /// </summary>
        [TestMethod]
        public void SeparateLazyElementInteractions()
        {
            SeleniumDriverManager newDriver = new SeleniumDriverManager(() => WebDriverFactory.GetBrowserWithDefaultConfiguration(BrowserType.HeadlessChrome), this.TestObject);
            newDriver.GetWebDriver().Navigate().GoToUrl("https://GlobalAutomation.azurewebsites.net/");
            this.ManagerStore.Add("test", newDriver);

            this.TestObject.WebDriver.Navigate().GoToUrl("https://GlobalAutomation.azurewebsites.net/Automation");

            LazyElement topNew = new LazyElement(this.TestObject, newDriver.GetWebDriver(), By.CssSelector("*"));
            LazyElement topDefault = new LazyElement(this.TestObject, By.CssSelector("*"));

            Assert.AreNotEqual(topNew.Text, topDefault.Text);
        }

        /// <summary>
        /// Make sure the driver is  initialized if we use it
        /// </summary>
        [TestMethod]
        public void Intialized()
        {
            // Do something so we initialize the web driver
            this.WebDriver.Manage().Window.Maximize();

            SeleniumDriverManager driverDriver = this.ManagerStore.GetManager<SeleniumDriverManager>();
            Assert.IsTrue(driverDriver.IsDriverIntialized(), "The driver should have been initialized");
        }

        /// <summary>
        /// Make sure the driver is not initialized if we don't use it
        /// </summary>
        [TestMethod]
        public void NotIntialized()
        {
            SeleniumDriverManager driverDriver = this.ManagerStore.GetManager<SeleniumDriverManager>();
            Assert.IsFalse(driverDriver.IsDriverIntialized(), "The driver should not be initialized until it gets used");
        }
    }
}
