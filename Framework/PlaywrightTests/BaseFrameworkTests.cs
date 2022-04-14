//--------------------------------------------------
// <copyright file="BaseFrameworkTests.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Low level framework tests</summary>
using CognizantSoftvision.Maqs.BasePlaywrightTest;
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
    public class BaseFrameworkTests
    {
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
                BasePlaywrightTest tester = new BasePlaywrightTest();
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
