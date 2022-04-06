//--------------------------------------------------
// <copyright file="PlaywrightSoftAssert.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Playwright override for the soft asserts</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseTest;
using System;

namespace CognizantSoftvision.Maqs.BasePlaywrightTest
{
    /// <summary>
    /// Soft Assert override for Playwright tests
    /// </summary>
    public class PlaywrightSoftAssert : SoftAssert
    {
        /// <summary>
        /// WebDriver to be used
        /// </summary>
        private readonly IPlaywrightTestObject testObject;

        /// <summary>
        /// Initializes a new instance of the PlaywrightSoftAssert class
        /// </summary>
        /// <param name="PlaywrightTestObject">The related Playwright test object</param>
        public PlaywrightSoftAssert(IPlaywrightTestObject PlaywrightTestObject)
            : base(PlaywrightTestObject.Log)
        {
            this.testObject = PlaywrightTestObject;
        }

        /// <summary>
        /// Soft assert method to check if the Action is false
        /// </summary>
        /// <param name="assertFunction">Function to use</param>
        /// <param name="failureMessage">Message to log</param>
        /// <param name="assertName">Soft assert name or name of expected assert being called.</param>
        /// <returns>Boolean of the assert</returns>
        public override bool Assert(Action assertFunction, string assertName, string failureMessage = "")
        {
            bool didPass = base.Assert(assertFunction, assertName, failureMessage);
            if (!didPass && this.testObject.GetDriverManager<PlaywrightDriverManager>().IsDriverIntialized())
            {
                /// TODO
                //////if (PlaywrightConfig.GetSoftAssertScreenshot())
                //////{
                //////    PlaywrightUtilities.CaptureScreenshot(this.testObject.WebDriver, this.testObject, this.TextToAppend(assertName));
                //////}

                //////if (PlaywrightConfig.GetSavePagesourceOnFail())
                //////{
                //////    PlaywrightUtilities.SavePageSource(this.testObject.WebDriver, this.testObject, $" ({this.NumberOfAsserts})");
                //////}

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

            // If softAssertName name is not provided only append the AssertNumber
            if (string.IsNullOrEmpty(softAssertName))
            {
                return $" ({this.NumberOfAsserts})";
            }
            else
            {
                // Make sure that softAssertName has valid file name characters only
                foreach (char invalidChar in System.IO.Path.GetInvalidFileNameChars())
                {
                    softAssertName = softAssertName.Replace(invalidChar, '~');
                }

                // If softAssertName is provided, use combination of softAssertName and AssertNumber
                return $" {softAssertName} ({this.NumberOfAsserts})";
            }
        }
    }
}
