//--------------------------------------------------
// <copyright file="PlaywrightDriverManager.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Playwright driver</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using CognizantSoftvision.Maqs.Utilities.Logging;
using Microsoft.Playwright;
using System;
using System.Reflection;
using System.Text;

namespace CognizantSoftvision.Maqs.BasePlaywrightTest
{
    /// <summary>
    /// Playwright driver store
    /// </summary>
    public class PlaywrightDriverManager : DriverManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaywrightDriverManager"/> class
        /// </summary>
        /// <param name="getDriver">Function for getting an Playwright page</param>
        /// <param name="testObject">The associated test object</param>
        public PlaywrightDriverManager(Func<PageDriver> getDriver, ITestObject testObject) : base(getDriver, testObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaywrightDriverManager"/> class
        /// </summary>
        /// <param name="getDriver">Function for getting an Playwright page</param>
        /// <param name="testObject">The associated test object</param>
        public PlaywrightDriverManager(Func<IPage> getDriver, ITestObject testObject) : base(() => new PageDriver(getDriver()), testObject)
        {
        }

        /// <summary>
        /// Override the page
        /// </summary>
        /// <param name="overrideDriver">The new page</param>
        public void OverrideDriver(PageDriver overrideDriver)
        {
            this.OverrideDriverGet(() => overrideDriver);
        }

        /// <summary>
        /// Override the page
        /// </summary>
        /// <param name="overrideDriver">Function for getting a new page</param>
        public void OverrideDriver(Func<PageDriver> overrideDriver)
        {
            this.OverrideDriverGet(overrideDriver);
        }

        /// <summary>
        /// Override the page
        /// </summary>
        /// <param name="overridePage">Function for getting a new page</param>
        public void OverrideDriver(Func<IPage> overridePage)
        {
            this.OverrideDriverGet(() => new PageDriver(overridePage()));
        }

        /// <summary>
        /// Get the page
        /// </summary>
        /// <returns>The page</returns>
        public PageDriver GetPageDriver()
        {
            PageDriver? tempDriver;

            if (!this.IsDriverIntialized() && LoggingConfig.GetLoggingEnabledSetting() != LoggingEnabled.NO)
            {
                tempDriver = GetDriver() as PageDriver;
                this.BaseDriver = tempDriver;

                // Log the setup
                this.LoggingStartup(tempDriver);
            }

            tempDriver = GetBase() as PageDriver;

            if(tempDriver == null)
            {
                throw new PlaywrightException("Base driver is null");
            }

            return tempDriver;
        }

        /// <summary>
        /// Get the page
        /// </summary>
        /// <returns>The page</returns>
        public override object Get()
        {
            return this.GetPageDriver();
        }

        /// <summary>
        /// Have the driver cleanup after itself
        /// </summary>
        protected override void DriverDispose()
        {
            Log.LogMessage(MessageType.VERBOSE, "Start dispose driver");

            // If we never created the driver we don't have any cleanup to do
            if (!this.IsDriverIntialized())
            {
                return;
            }

            try
            {
                PageDriver driver = this.GetPageDriver();
                driver.AsyncPage?.CloseAsync().Wait();
            }
            catch (Exception e)
            {
                Log.LogMessage(MessageType.ERROR, $"Failed to close page because: {e.Message}");
            }

            this.BaseDriver = null;
            Log.LogMessage(MessageType.VERBOSE, "End dispose driver");
        }

        /// <summary>
        /// Log that the page setup
        /// </summary>
        /// <param name="pageDriver">The page</param>
        private void LoggingStartup(PageDriver? pageDriver)
        {
            try
            {
                Log.LogMessage(MessageType.INFORMATION, $"Driver: {pageDriver?.ParentBrower}");
            }
            catch (Exception e)
            {
                Log.LogMessage(MessageType.ERROR, $"Failed to start driver because: {e.Message}");
                Console.WriteLine($"Failed to start driver because: {e.Message}");
            }
        }
    }
}
