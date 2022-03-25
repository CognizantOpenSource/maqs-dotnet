//-----------------------------------------------------
// <copyright file="AppiumPageModel.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>A test Appium page object model</summary>
//-----------------------------------------------------
using CognizantSoftvision.Maqs.BaseAppiumTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using OpenQA.Selenium.Appium;

namespace AppiumUnitTests
{
    /// <summary>
    /// Appium page model class for testing
    /// </summary>
    public class AppiumPageModel : BaseAppiumPageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppiumPageModel"/> class
        /// </summary>
        /// <param name="testObject">The base Appium test object</param>
        public AppiumPageModel(IAppiumTestObject testObject)
            : base(testObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppiumPageModel"/> class
        /// </summary>
        /// <param name="testObject">The base Appium test object</param>
        /// <param name="appiumDriver">Appium driver to use</param>
        public AppiumPageModel(IAppiumTestObject testObject, AppiumDriver appiumDriver)
            : base(testObject, appiumDriver)
        {
        }

        /// <summary>
        /// Open the page
        /// </summary>
        public void OpenPage()
        {
            this.AppiumDriver.Navigate().GoToUrl(Config.GetValueForSection(ConfigSection.AppiumMaqs, "WebSiteBase"));
        }

        /// <summary>
        /// Get the top level element
        /// </summary>
        public LazyMobileElement TopLevel
        {
            get { return this.GetLazyElement(MobileBy.XPath("//*[@id='body']"), "Top level"); }
        }

        /// <summary>
        /// Check if the page has been loaded
        /// </summary>
        /// <returns>True if the page was loaded</returns>
        public override bool IsPageLoaded()
        {
            return TopLevel.Exists;
        }
    }
}
