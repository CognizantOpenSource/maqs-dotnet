//--------------------------------------------------
// <copyright file="BaseAppiumTest.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>This is the base Appium test class</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseTest;
using CognizantSoftvision.Maqs.Utilities.Logging;
using OpenQA.Selenium.Appium;
using System;

namespace CognizantSoftvision.Maqs.BaseAppiumTest
{
    /// <summary>
    /// Generic base Appium test class
    /// </summary>
    public class BaseAppiumTest : BaseExtendableTest<IAppiumTestObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAppiumTest"/> class.
        /// Setup the web driver for each test class
        /// </summary>
        public BaseAppiumTest()
        {
        }

        /// <summary>
        /// Gets or sets the AppiumDriver
        /// </summary>
        public AppiumDriver AppiumDriver
        {
            get
            {
                return this.TestObject.AppiumDriver;
            }

            set
            {
                this.TestObject.OverrideAppiumDriver(value);
            }
        }

        /// <summary>
        /// The default get appium driver function
        /// </summary>
        /// <returns>The appium driver</returns>
        protected virtual AppiumDriver GetMobileDevice()
        {
            return AppiumDriverFactory.GetDefaultMobileDriver();
        }

        /// <summary>
        /// Take a screen shot if needed and tear down the appium driver
        /// </summary>
        /// <param name="resultType">The test result</param>
        protected override void BeforeCleanup(TestResultType resultType)
        {
            try
            {
                // Captures screenshot if test result is not a pass and logging is enabled
                if (this.TestObject.GetDriverManager<AppiumDriverManager>().IsDriverIntialized() && this.Log is IFileLogger && resultType != TestResultType.PASS &&
                    this.LoggingEnabledSetting != LoggingEnabled.NO)
                {
                    AppiumUtilities.CaptureScreenshot(this.AppiumDriver, this.TestObject, " Final");

                    if (AppiumConfig.GetSavePagesourceOnFail())
                    {
                        AppiumUtilities.SavePageSource(this.AppiumDriver, this.TestObject, "FinalPageSource");
                    }
                }
            }
            catch (Exception exception)
            {
                this.TryToLog(MessageType.WARNING, $"Failed to get screen shot because: {exception.Message}");
            }
        }

        /// <summary>
        /// Create a test object
        /// </summary>
        /// <param name="log">Assocatied logger</param>
        /// <returns>The Appium test object</returns>
        protected override IAppiumTestObject CreateSpecificTestObject(ILogger log)
        {
            return new AppiumTestObject(() => this.GetMobileDevice(), log, this.GetFullyQualifiedTestClassName());
        }
    }
}
