﻿//--------------------------------------------------
// <copyright file="BaseSeleniumTest.cs" company="Magenic">
//  Copyright 2021 Magenic, All rights Reserved
// </copyright>
// <summary>This is the base Selenium test class</summary>
//--------------------------------------------------
using Magenic.Maqs.BaseTest;
using Magenic.Maqs.Utilities.Logging;
using OpenQA.Selenium;
using System;

namespace Magenic.Maqs.BaseSeleniumTest
{
    /// <summary>
    /// Generic base Selenium test class
    /// </summary>
    public class BaseSeleniumTest : BaseExtendableTest<ISeleniumTestObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSeleniumTest"/> class
        /// Setup the web driver for each test class
        /// </summary>
        public BaseSeleniumTest()
        {
        }

        /// <summary>
        /// Gets or sets the WebDriver
        /// </summary>
        public IWebDriver WebDriver
        {
            get
            {
                return this.TestObject.WebDriver;
            }

            set
            {
                this.TestObject.OverrideWebDriver(value);
            }
        }

        /// <summary>
        /// The default get web driver function
        /// </summary>
        /// <returns>The web driver</returns>
        protected virtual IWebDriver GetBrowser()
        {
            return WebDriverFactory.GetDefaultBrowser();
        }

        /// <summary>
        /// Take a screen shot if needed and tear down the web driver
        /// </summary>
        /// <param name="resultType">The test result</param>
        protected override void BeforeLoggingTeardown(TestResultType resultType)
        {
            // Try to take a screen shot
            try
            {
                if (this.TestObject.GetDriverManager<SeleniumDriverManager>().IsDriverIntialized() && this.Log is IFileLogger && resultType != TestResultType.PASS && this.LoggingEnabledSetting != LoggingEnabled.NO)
                {
                    SeleniumUtilities.CaptureScreenshot(this.WebDriver, this.TestObject, " Final");

                    if (SeleniumConfig.GetSavePagesourceOnFail())
                    {
                        SeleniumUtilities.SavePageSource(this.WebDriver, this.TestObject, "FinalPageSource");
                    }
                }
            }
            catch (Exception e)
            {
                this.TryToLog(MessageType.WARNING, $"Failed to get screen shot because: {e.Message}");
            }
        }

        /// <summary>
        /// Create a test object
        /// </summary>
        /// <param name="log">Assocatied logger</param>
        /// <returns>The Selenium test object</returns>
        protected override ISeleniumTestObject CreateSpecificTestObject(ILogger log)
        {
           return new SeleniumTestObject(() => this.GetBrowser(), log, this.GetFullyQualifiedTestClassName());
        }
    }
}