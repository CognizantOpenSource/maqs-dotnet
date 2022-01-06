//--------------------------------------------------
// <copyright file="IAppiumTestObject.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Holds Appium test object interface</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseTest;
using OpenQA.Selenium.Appium;
using System;

namespace CognizantSoftvision.Maqs.BaseAppiumTest
{
    /// <summary>
    /// Appium test object interface
    /// </summary>
    public interface IAppiumTestObject : ITestObject
    {
        /// <summary>
        /// Gets the Appium driver
        /// </summary>
        AppiumDriver AppiumDriver { get; }

        /// <summary>
        /// Gets the Appium driver manager
        /// </summary>
        AppiumDriverManager AppiumManager { get; }

        /// <summary>
        /// Override the Appium driver
        /// </summary>
        /// <param name="appiumDriver">New Appium driver</param>
        void OverrideAppiumDriver(AppiumDriver appiumDriver);

        /// <summary>
        /// Override the Appium driver
        /// </summary>
        /// <param name="appiumDriver">New function for initializing a Appium driver</param>
        void OverrideAppiumDriver(Func<AppiumDriver> appiumDriver);
    }
}