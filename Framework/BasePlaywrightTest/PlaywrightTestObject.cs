//--------------------------------------------------
// <copyright file="PlaywrightTestObject.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Holds Playwright context data</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseTest;
using CognizantSoftvision.Maqs.Utilities.Logging;
using Microsoft.Playwright;
using System;

namespace CognizantSoftvision.Maqs.BasePlaywrightTest
{
    /// <summary>
    /// Playwright test context data
    /// </summary>
    public class PlaywrightTestObject : BaseTestObject, IPlaywrightTestObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaywrightTestObject" /> class
        /// </summary>
        /// <param name="PageDriver">The test's Playwright page</param>
        /// <param name="logger">The test's logger</param>
        /// <param name="fullyQualifiedTestName">The test's fully qualified test name</param>
        public PlaywrightTestObject(PageDriver PageDriver, ILogger logger, string fullyQualifiedTestName) : base(logger, fullyQualifiedTestName)
        {
            this.ManagerStore.Add(typeof(PlaywrightDriverManager).FullName, new PlaywrightDriverManager(() => PageDriver, this));
            this.SoftAssert = new PlaywrightSoftAssert(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaywrightTestObject" /> class
        /// </summary>
        /// <param name="getDriver">Function for getting a Playwright page</param>
        /// <param name="logger">The test's logger</param>
        /// <param name="fullyQualifiedTestName">The test's fully qualified test name</param>
        public PlaywrightTestObject(Func<PageDriver> getDriver, ILogger logger, string fullyQualifiedTestName) : base(logger, fullyQualifiedTestName)
        {
            this.ManagerStore.Add(typeof(PlaywrightDriverManager).FullName, new PlaywrightDriverManager(getDriver, this));
            this.SoftAssert = new PlaywrightSoftAssert(this);
        }

        /// <summary>
        /// Gets the Playwright driver manager
        /// </summary>
        public PlaywrightDriverManager PageManager
        {
            get
            {
                return this.ManagerStore.GetManager<PlaywrightDriverManager>(typeof(PlaywrightDriverManager).FullName);
            }
        }

        /// <summary>
        /// Gets the Playwright page
        /// </summary>
        public PageDriver PageDriver
        {
            get
            {
                return this.PageManager.GetPageDriver();
            }
        }

        /// <summary>
        /// Override the the function for creating a new page
        /// </summary>
        /// <param name="getPage">New function for creating a page</param>
        public void OverridePageDriver(Func<IPage> getPage)
        {
            this.PageManager.OverrideDriver(getPage);
        }

        /// <summary>
        /// Override the the old page with a new page
        /// </summary>
        /// <param name="page">The new page</param>
        public void OverridePageDriver(IPage page)
        {
            this.PageManager.OverrideDriver(() => page);
        }

        /// <summary>
        /// Override the the old page driver with a new page
        /// </summary>
        /// <param name="pageDriver">The new page drive</param>
        public void OverridePageDriver(PageDriver pageDriver)
        {
            this.PageManager.OverrideDriver(pageDriver);
        }
    }
}