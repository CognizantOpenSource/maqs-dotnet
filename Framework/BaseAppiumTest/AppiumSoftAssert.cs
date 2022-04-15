//--------------------------------------------------
// <copyright file="AppiumSoftAssert.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>This is the Appium soft assert class</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseTest;
using System;

namespace CognizantSoftvision.Maqs.BaseAppiumTest
{
    /// <summary>
    /// Soft Assert override for appium tests
    /// </summary>
    public class AppiumSoftAssert : SoftAssert
    {
        /// <summary>
        /// AppiumDriver to be used
        /// </summary>
        private readonly IAppiumTestObject appiumTestObject;

        /// <summary>
        /// Initializes a new instance of the AppiumSoftAssert class
        /// </summary>
        /// <param name="appiumTestObject">The related Appium test object</param>
        public AppiumSoftAssert(IAppiumTestObject appiumTestObject)
            : base(appiumTestObject.Log)
        {
            this.appiumTestObject = appiumTestObject;
        }

        /// <inheritdoc /> 
        public override bool Assert(Action assertFunction, string assertName, string failureMessage = "")
        {
            bool didPass = base.Assert(assertFunction, assertName, failureMessage);

            if (!didPass && this.appiumTestObject.GetDriverManager<AppiumDriverManager>().IsDriverIntialized())
            {
                if (AppiumConfig.GetSoftAssertScreenshot())
                {
                    AppiumUtilities.CaptureScreenshot(this.appiumTestObject.AppiumDriver, this.appiumTestObject, this.TextToAppend(assertName));
                }

                if (AppiumConfig.GetSavePagesourceOnFail())
                {
                    AppiumUtilities.SavePageSource(this.appiumTestObject.AppiumDriver, this.appiumTestObject, $" ({ this.NumberOfAsserts})");
                }

                return false;
            }
            return didPass;
        }

        /// <summary>
        /// Method to determine the text to be appended to the screenshot file names
        /// </summary>
        /// <param name="softAssertName">Soft assert name</param>
        /// <returns>String to be appended</returns>
        private string TextToAppend(string softAssertName)
        {
            string appendToFileName;

            // If softAssertName name is not provided only append the AssertNumber
            if (softAssertName == string.Empty)
            {
                appendToFileName = $" ({this.NumberOfAsserts})";
            }
            else
            {
                // Make sure that softAssertName has valid file name characters only
                foreach (char invalidChar in System.IO.Path.GetInvalidFileNameChars())
                {
                    softAssertName = softAssertName.Replace(invalidChar, '~');
                }

                // If softAssertName is provided, use combination of softAssertName and AssertNumber
                appendToFileName = " " + softAssertName + $" ({this.NumberOfAsserts})";
            }

            return appendToFileName;
        }
    }
}
