﻿//-----------------------------------------------------
// <copyright file="SeleniumPageModel.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>A test selenium page object model</summary>
//-----------------------------------------------------
using CognizantSoftvision.Maqs.BaseAppiumTest;
using CognizantSoftvision.Maqs.Utilities.Logging;
using CognizantSoftvision.Maqs.Utilities.Performance;
using OpenQA.Selenium.Appium;

namespace AppiumUnitTests
{
    /// <summary>
    /// Notepad page model class for testing
    /// </summary>
    public class NotepadPageModel : BaseAppiumPageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumPageModel"/> class
        /// </summary>
        /// <param name="testObject">The base Appium test object</param>
        public NotepadPageModel(IAppiumTestObject testObject)
            : base(testObject)
        {
        }

        /// <summary>
        /// Get the document
        /// </summary>
        private LazyMobileElement Document
        {
            get { return this.GetLazyElement(MobileBy.ClassName("Notepad"), "Document"); }
        }

        /// <summary>
        /// Gets the text editor
        /// </summary>
        public LazyMobileElement TextEditor
        {
            get { return this.GetLazyElement(Document, MobileBy.Name("Text Editor"), "Text Editor"); }
        }

        /// <summary>
        /// Get the close button
        /// </summary>
        private LazyMobileElement Close
        {
            get { return this.GetLazyElement(MobileBy.Name("Close"), "Close"); }
        }

        /// <summary>
        /// Get the don't save button
        /// </summary>
        public LazyMobileElement DontSave
        {
            get { return this.GetLazyElement(MobileBy.Name("Don't Save"), "Don't Save"); }
        }

        public void CloseAndDontSave()
        {
            this.Close.Click();
            this.DontSave.Click();
        }

        /// <summary>
        /// Check if the page has been loaded
        /// </summary>
        /// <returns>True if the page was loaded</returns>
        public override bool IsPageLoaded()
        {
            return TextEditor.Exists;
        }

        /// <summary>
        /// Get web driver
        /// </summary>
        /// <returns>The web driver</returns>
        public AppiumDriver GetAppiumDriver()
        {
            return this.AppiumDriver;
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
        public IAppiumTestObject GetTestObject()
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
