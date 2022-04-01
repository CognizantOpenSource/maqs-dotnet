//-----------------------------------------------------
// <copyright file="SeleniumPageModelOther.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Another test Selenium page object model</summary>
//-----------------------------------------------------
using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest.Extensions;
using OpenQA.Selenium;
using System.Diagnostics.CodeAnalysis;

namespace SeleniumUnitTests
{
    /// <summary>
    /// Selenium page model class for testing
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SeleniumPageModelOther : BaseSeleniumPageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumPageModel"/> class
        /// </summary>
        /// <param name="testObject">The base Selenium test object</param>
        /// <param name="otherDriver">Web driver to use instead of the default</param>
        public SeleniumPageModelOther(ISeleniumTestObject testObject, IWebDriver otherDriver)
            : base(testObject, otherDriver)
        {
        }

        /// <summary>
        /// Root body
        /// </summary>
        private LazyElement Body
        {
            get { return this.GetLazyElement(By.CssSelector("BODY"), "Root body"); }
        }

        /// <summary>
        /// Get page url
        /// </summary>
        public static string Url
        {
            get { return SeleniumConfig.GetWebSiteBase() + "async.html"; }
        }

        /// <summary>
        /// Get loaded label
        /// </summary>
        public LazyElement LoadedLazyElement
        {
            get { return this.GetLazyElement(Body, By.CssSelector("#loading-div-text[style='']"), "Loaded label"); }
        }

        /// <summary>
        /// Open the page
        /// </summary>
        public void OpenPage()
        {
            this.WebDriver.Navigate().GoToUrl(Url);
        }

        /// <summary>
        /// Check if the page has been loaded
        /// </summary>
        /// <returns>True if the page was loaded</returns>
        public override bool IsPageLoaded()
        {
            return LoadedLazyElement.Exists;
        }
    }
}
