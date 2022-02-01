﻿//--------------------------------------------------
// <copyright file="BaseAppiumPageModel.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>This is the base Appium page model class</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.Utilities.Logging;
using CognizantSoftvision.Maqs.Utilities.Performance;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CognizantSoftvision.Maqs.BaseAppiumTest
{
    /// <summary>
    /// Base Appium page model
    /// </summary>
    public abstract class BaseAppiumPageModel
    {
        /// <summary>
        /// Store of LazyMobileElement for the page
        /// </summary>
        private readonly Dictionary<string, LazyMobileElement> lazyElementStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAppiumPageModel"/> class.
        /// </summary>
        /// <param name="testObject">The Appium test object</param>
        protected BaseAppiumPageModel(IAppiumTestObject testObject)
        {
            this.TestObject = testObject;
            this.AppiumDriver = testObject.AppiumDriver;
            this.lazyElementStore = new Dictionary<string, LazyMobileElement>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAppiumPageModel"/> class.
        /// </summary>
        /// <param name="testObject">The Appium test object</param>
        /// <param name="appiumDriver">Appium driver to use</param>
        protected BaseAppiumPageModel(IAppiumTestObject testObject, AppiumDriver appiumDriver)
        {
            this.TestObject = testObject;
            this.AppiumDriver = appiumDriver;
            this.lazyElementStore = new Dictionary<string, LazyMobileElement>();
        }

        /// <summary>
        /// Gets the webdriver from the test object
        /// </summary>
        protected AppiumDriver AppiumDriver { get; private set; }

        /// <summary>
        /// Gets the log from the test object
        /// </summary>
        protected ILogger Log
        {
            get { return this.TestObject.Log; }
        }

        /// <summary>
        /// Gets the performance timer collection from the test object
        /// </summary>
        protected IPerfTimerCollection PerfTimerCollection
        {
            get { return this.TestObject.PerfTimerCollection; }
        }

        /// <summary>
        /// Gets or sets the Appium test object
        /// </summary>
        protected IAppiumTestObject TestObject { get; set; }

        /// <summary>
        /// Override the driver 
        /// This allows you to use something other than the default tests object driver.
        /// </summary>
        /// <param name="appiumDriver">The override driver</param>
        public void OverrideDriver(AppiumDriver appiumDriver)
        {
            // Clear cached elements
            this.lazyElementStore.Clear();

            // Set new driver
            this.AppiumDriver = appiumDriver;
        }

        /// <summary>
        /// Check if the page has been loaded
        /// </summary>
        /// <returns>True if the page was loaded</returns>
        public abstract bool IsPageLoaded();

        /// <summary>
        /// Gets LazyMobileElement from page model's lazy element store if it exists, otherwise
        /// initializes a new instance of the LazyElement and adds it to the lazy element store
        /// </summary>
        /// <param name="locator">The 'by' selector for the element</param>
        /// <param name="userFriendlyName">A user friendly name, for logging purposes</param>
        /// <returns>The LazyMobileElement</returns>
        protected LazyMobileElement GetLazyElement(By locator, [CallerMemberName] string userFriendlyName = null)
        {
            string lazyElementStoreKey = $"{locator}{userFriendlyName}";

            if (!this.lazyElementStore.ContainsKey(lazyElementStoreKey))
            {
                this.lazyElementStore.Add(lazyElementStoreKey, new LazyMobileElement(this.TestObject, this.AppiumDriver, locator, userFriendlyName));
            }

            return this.lazyElementStore[lazyElementStoreKey];
        }

        /// <summary>
        /// Gets LazyMobileElement from page model's lazy element store if it exists, otherwise
        /// initializes a new instance of the LazyElement and adds it to the lazy element store
        /// </summary>
        /// <param name="parent">The LazyElement parent element</param>
        /// <param name="locator">The 'by' selector for the element</param>
        /// <param name="userFriendlyName">A user friendly name, for logging purposes</param>
        /// <returns>The LazyMobileElement</returns>
        protected LazyMobileElement GetLazyElement(LazyMobileElement parent, By locator, [CallerMemberName] string userFriendlyName = null)
        {
            string lazyElementStoreKey = $"{parent}{locator}{userFriendlyName}";

            if (!this.lazyElementStore.ContainsKey(lazyElementStoreKey))
            {
                this.lazyElementStore.Add(lazyElementStoreKey, new LazyMobileElement(parent, locator, userFriendlyName));
            }

            return this.lazyElementStore[lazyElementStoreKey];
        }
    }
}
