﻿//--------------------------------------------------
// <copyright file="AppiumTestObject.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Holds Appium context data</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseTest;
using CognizantSoftvision.Maqs.Utilities.Logging;
using OpenQA.Selenium.Appium;
using System;

namespace CognizantSoftvision.Maqs.BaseAppiumTest
{
    /// <summary>
    /// Appium test context data
    /// </summary>
    public class AppiumTestObject : BaseTestObject, IAppiumTestObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppiumTestObject" /> class
        /// </summary>
        /// <param name="appiumDriver">The test's Appium driver</param>
        /// <param name="logger">The test's logger</param>
        /// <param name="fullyQualifiedTestName">The test's fully qualified test name</param>
        public AppiumTestObject(AppiumDriver appiumDriver, ILogger logger, string fullyQualifiedTestName) : base(logger, fullyQualifiedTestName)
        {
            this.ManagerStore.Add(typeof(AppiumDriverManager).FullName, new AppiumDriverManager(() => appiumDriver, this));
            this.SoftAssert = new AppiumSoftAssert(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppiumTestObject" /> class
        /// </summary>
        /// <param name="appiumDriver">Function for initializing a Appium driver</param>
        /// <param name="logger">The test's logger</param>
        /// <param name="fullyQualifiedTestName">The test's fully qualified test name</param>
        public AppiumTestObject(Func<AppiumDriver> appiumDriver, ILogger logger, string fullyQualifiedTestName) : base(logger, fullyQualifiedTestName)
        {
            this.ManagerStore.Add(typeof(AppiumDriverManager).FullName, new AppiumDriverManager(appiumDriver, this));
            this.SoftAssert = new AppiumSoftAssert(this);
        }

        /// <summary>
        /// Gets the Appium driver
        /// </summary>
        public AppiumDriver AppiumDriver
        {
            get
            {
                return this.AppiumManager.GetAppiumDriver();
            }
        }

        /// <summary>
        /// Gets the Appium driver manager
        /// </summary>
        public AppiumDriverManager AppiumManager
        {
            get
            {
                return this.ManagerStore.GetManager<AppiumDriverManager>();
            }
        }

        /// <summary>
        /// Override the Appium driver
        /// </summary>
        /// <param name="appiumDriver">New Appium driver</param>
        public void OverrideAppiumDriver(AppiumDriver appiumDriver)
        {
            this.AppiumManager.OverrideDriver(appiumDriver);
        }

        /// <summary>
        /// Override the Appium driver
        /// </summary>
        /// <param name="appiumDriver">New function for initializing a Appium driver</param>
        public void OverrideAppiumDriver(Func<AppiumDriver> appiumDriver)
        {
            this.AppiumManager.OverrideDriver(appiumDriver);
        }
    }
}
