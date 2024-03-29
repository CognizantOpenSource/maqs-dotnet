﻿//-----------------------------------------------------
// <copyright file="PageModelOther.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Another test Playwright page object model</summary>
//-----------------------------------------------------
using CognizantSoftvision.Maqs.BasePlaywrightTest;
using System.Diagnostics.CodeAnalysis;

namespace PlaywrightTests
{
    /// <summary>
    /// Playwright page model class for testing
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class PageModelOther : BasePlaywrightPageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageModel"/> class
        /// </summary>
        /// <param name="testObject">The base Playwright test object</param>
        /// <param name="otherDriver">Page driver to use instead of the default</param>
        public PageModelOther(IPlaywrightTestObject testObject, PageDriver otherDriver)
            : base(testObject, otherDriver)
        {
        }

        /// <summary>
        /// Get page url
        /// </summary>
        public static string Url
        {
            get { return PlaywrightConfig.WebBase() + "async.html"; }
        }

        /// <summary>
        /// Root body
        /// </summary>
        private PlaywrightSyncElement Body
        {
            get { return new PlaywrightSyncElement(this.PageDriver, "BODY"); }
        }

        /// <summary>
        /// Get loaded label
        /// </summary>
        public PlaywrightSyncElement LoadedPlaywrightElement
        {
            get { return new PlaywrightSyncElement(Body, "#loading-div-text[style='']"); }
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
            return LoadedPlaywrightElement.IsEventualyVisible();
        }
    }
}
