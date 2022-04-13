//--------------------------------------------------
// <copyright file="BaseFrameworkTests.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Low level framework tests</summary>
using CognizantSoftvision.Maqs.BasePlaywrightTest;
using CognizantSoftvision.Maqs.BaseTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using CognizantSoftvision.Maqs.Utilities.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using NUnitAssert = NUnit.Framework.Assert;

namespace PlaywrightTests
{
    /// <summary>
    /// Framework unit test class
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    [DoNotParallelize]
    [TestCategory(TestCategories.Framework)]
    public class BaseFrameworkTests : BaseTestUnitTests.BaseFrameworkTests
    {
        /// <summary>
        ///  Base test does soft assert check
        /// </summary>
        [TestMethod]
        public new void SoftAssertWithNoFailure()
        {
            base.SoftAssertWithNoFailure();
        }

        /// <summary>
        ///  Assure artifacts exist
        /// </summary>
        [Test]
        public void AllTestArtifactsExist()
        {
            Config.AddGeneralTestSettingValues("Log", "YES");
            Config.AddTestSettingValue("CaptureVideo", "YES", ConfigSection.PlaywrightMaqs);
            Config.AddTestSettingValue("CaptureScreenshots", "YES", ConfigSection.PlaywrightMaqs);
            Config.AddTestSettingValue("CaptureSnapshots", "YES", ConfigSection.PlaywrightMaqs);

            try
            {
                BasePlaywrightTest tester = this.GetBaseTest() as BasePlaywrightTest;
                tester.MaqsSetup();

                var logFilePath = (tester.Log as FileLogger).FilePath;
                var zipFilePath = $"{logFilePath.Remove(logFilePath.Length - 4)}_0.zip";
                var videoPath = tester.PageDriver.ParentBrower.Contexts[0].Pages[0].Video.PathAsync().Result;
                var browser = tester.PageDriver.ParentBrower;

                tester.MaqsTeardown();

                // Make sure the brower finish the video
                browser.CloseAsync().Wait();

                NUnitAssert.IsTrue(File.Exists(logFilePath), $"Expected video {videoPath} to exist");
                NUnitAssert.IsTrue(File.Exists(videoPath), $"Expected video {videoPath} to exist");
                NUnitAssert.IsTrue(File.Exists(zipFilePath), $"Expected zip file {zipFilePath} to exist");

                DeleteFailWithWait(logFilePath);
                DeleteFailWithWait(zipFilePath);
                DeleteFailWithWait(videoPath);
            }
            finally
            {
                Config.ClearOverrides();
            }
        }

        /// <summary>
        ///  Base test does soft assert check
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Framework)]
        [ExpectedException(typeof(AssertFailedException))]
        public new void SoftAssertWithFailure()
        {
            base.SoftAssertWithFailure();
        }

        /// <summary>
        ///  Base test does soft assert check
        /// </summary>
        [Test]
        [Category(TestCategories.Framework)]
        [Category(TestCategories.NUnit)]
        public new void SoftAssertNUnitWithNoFailure()
        {
            base.SoftAssertWithNoFailure();
        }

        /// <summary>
        ///  Base test does soft assert check
        /// </summary>
        [Test]
        [Category(TestCategories.Framework)]
        [Category(TestCategories.NUnit)]
        public new void SoftAssertNUnitWithFailure()
        {
            base.SoftAssertNUnitWithFailure();
        }

        /// <summary>
        /// Override the base test object
        /// </summary>
        /// <returns>The base test as base web service</returns>
        protected override BaseTest GetBaseTest()
        {
            return new BasePlaywrightTest();
        }

        /// <summary>
        /// Delete a file that may take a while to unlock
        /// </summary>
        /// <param name="filePath">File to delete</param>
        private static void DeleteFailWithWait(string filePath)
        {
            GenericWait.WaitUntil(() =>
            {
                File.Delete(filePath);
                return true;
            });
        }
    }
}
