//--------------------------------------------------
// <copyright file="BasePlaywrightPageModel.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>This is the base Playwright page model class</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.Utilities.Logging;
using CognizantSoftvision.Maqs.Utilities.Performance;

namespace CognizantSoftvision.Maqs.BasePlaywrightTest
{
    /// <summary>
    /// Base Playwright page model
    /// </summary>
    public abstract class BasePlaywrightPageModel
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePlaywrightPageModel"/> class.
        /// </summary>
        /// <param name="testObject">The Playwright test object</param>
        protected BasePlaywrightPageModel(IPlaywrightTestObject testObject)
        {
            this.TestObject = testObject;
            this.PageDriver = testObject.PageDriver;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePlaywrightPageModel"/> class.
        /// </summary>
        /// <param name="testObject">The Playwright test object</param>
        /// <param name="customDriver">Driver to use instead of the default test object related driver</param>
        protected BasePlaywrightPageModel(IPlaywrightTestObject testObject, PageDriver customDriver)
        {
            this.TestObject = testObject;
            this.PageDriver = customDriver;
        }

        /// <summary>
        /// Gets the PageDriver from the test object
        /// </summary>
        public PageDriver PageDriver { get; private set; }

        /// <summary>
        /// Gets the log from the test object
        /// </summary>
        public ILogger Log
        {
            get { return this.TestObject.Log; }
        }

        /// <summary>
        /// Gets the performance timer collection from the test object
        /// </summary>
        public IPerfTimerCollection PerfTimerCollection
        {
            get { return this.TestObject.PerfTimerCollection; }
        }

        /// <summary>
        /// Gets or sets the Playwright test object
        /// </summary>
        public IPlaywrightTestObject TestObject { get; protected set; }

        /// <summary>
        /// Override the PageDriver 
        /// This allows you to use something other than the default tests object PageDriver.
        /// </summary>
        /// <param name="PageDriver">The override PageDriver</param>
        public void OverridePageDriver(PageDriver PageDriver)
        {
            // Override driver
            this.PageDriver = PageDriver;
        }

        /// <summary>
        /// Check if the page has been loaded
        /// </summary>
        /// <returns>True if the page was loaded</returns>
        public abstract bool IsPageLoaded();
    }
}
