//--------------------------------------------------
// <copyright file="ISeleniumTestObject.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Holds Selenium test object interface</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseTest;
using OpenQA.Selenium;
using System;

namespace CognizantSoftvision.Maqs.BaseSeleniumTest
{
    /// <summary>
    /// Selenium test object interface
    /// </summary>
    public interface ISeleniumTestObject : ITestObject
    {
        /// <summary>
        /// Gets the Selenium web driver
        /// </summary>
        IWebDriver WebDriver { get; }

        /// <summary>
        /// Gets the Selenium web driver manager
        /// </summary>
        SeleniumDriverManager WebManager { get; }

        /// <summary>
        /// Override the function for creating a Selenium web driver
        /// </summary>
        /// <param name="getDriver">Function for creating a web driver</param>
        void OverrideWebDriver(Func<IWebDriver> getDriver);

        /// <summary>
        /// Override the Selenium web driver
        /// </summary>
        /// <param name="webDriver">New web driver</param>
        void OverrideWebDriver(IWebDriver webDriver);
    }
}