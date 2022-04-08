//-----------------------------------------------------
// <copyright file="PageModelOther.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Another test Playwright page object model</summary>
//-----------------------------------------------------
using CognizantSoftvision.Maqs.BasePlaywrightTest;
using Microsoft.Playwright;
using System.Diagnostics.CodeAnalysis;

namespace PlaywrightTests
{
    /// <summary>
    /// Playwright page model class for testing
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class PageModelIFrame : BasePlaywrightPageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageModel"/> class
        /// </summary>
        /// <param name="testObject">The base Playwright test object</param>
        /// <param name="otherDriver">Page driver to use instead of the default</param>
        public PageModelIFrame(IPlaywrightTestObject testObject)
            : base(testObject)
        {
        }

        /// <summary>
        /// Get page url
        /// </summary>
        public static string Url
        {
            get { return PlaywrightConfig.GetWebBase() + "iFrame.html"; }
        }


        /// <summary>
        /// Test frame
        /// </summary>
        private IFrameLocator Frame
        {
            get { return this.PageDriver.AsyncPage.FrameLocator("#frame"); }
        }

        /// <summary>
        /// Get loaded label
        /// </summary>
        public PlaywrightSyncElement ShowDialog
        {
            get { return new PlaywrightSyncElement(Frame, "#showDialog1"); }
        }

        /// <summary>
        /// Get loaded label
        /// </summary>
        public PlaywrightSyncElement CloseDialog
        {
            get { return new PlaywrightSyncElement(Frame, "#CloseButtonShowDialog"); }
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
            return ShowDialog.IsEventualyVisible();
        }
    }
}
