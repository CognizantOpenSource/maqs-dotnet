//-----------------------------------------------------
// <copyright file="PlaywrightPageModel.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>A test Playwright page object model</summary>
//-----------------------------------------------------
using CognizantSoftvision.Maqs.BasePlaywrightTest;
using CognizantSoftvision.Maqs.Utilities.Logging;
using CognizantSoftvision.Maqs.Utilities.Performance;
using System.Diagnostics.CodeAnalysis;

namespace PlaywrightTests
{
    /// <summary>
    /// Playwright page model class for testing
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class PlaywrightPageModel : BasePlaywrightPageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaywrightPageModel"/> class
        /// </summary>
        /// <param name="testObject">The base Playwright test object</param>
        public PlaywrightPageModel(IPlaywrightTestObject testObject)
            : base(testObject)
        {
        }

        /// <summary>
        /// Get page url
        /// </summary>
        public static string Url
        {
            get { return PlaywrightConfig.GetWebBase(); }
        }

        /// <summary>
        /// Gets a parent element
        /// </summary>
        public PlaywrightSyncElement FlowerTablePlaywrightElement
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "#FlowerTable"); }
        }

        /// <summary>
        /// Gets a child element, the second table caption
        /// </summary>
        public PlaywrightSyncElement FlowerTableCaptionWithParent
        {
            get { return new PlaywrightSyncElement(this.FlowerTablePlaywrightElement, "CAPTION > Strong"); }
        }

        /// <summary>
        /// Open the page
        /// </summary>
        public void OpenPage()
        {
            this.PageDriver.Goto(Url);
        }

        /// <summary>
        /// Check if the page has been loaded
        /// </summary>
        /// <returns>True if the page was loaded</returns>
        public override bool IsPageLoaded()
        {
            return true;
        }

        /// <summary>
        /// Get page driver
        /// </summary>
        /// <returns>The page driver</returns>
        public PageDriver GetPageDriver()
        {
            return this.PageDriver;
        }

        /// <summary>
        /// Get logger
        /// </summary>
        /// <returns>The logger</returns>
        public ILogger GetLogger()
        {
            return this.Log;
        }

        /// <summary>
        /// Get test object
        /// </summary>
        /// <returns>The test object</returns>
        public IPlaywrightTestObject GetTestObject()
        {
            return this.TestObject;
        }

        /// <summary>
        /// Get performance timer collection
        /// </summary>
        /// <returns>The performance timer collection</returns>
        public IPerfTimerCollection GetPerfTimerCollection()
        {
            return this.PerfTimerCollection;
        }
    }
}
