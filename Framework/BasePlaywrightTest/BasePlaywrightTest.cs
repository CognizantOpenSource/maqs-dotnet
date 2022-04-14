//--------------------------------------------------
// <copyright file="BasePlaywrightTest.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>This is the base Playwright test class</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseTest;
using CognizantSoftvision.Maqs.Utilities.Logging;
using Microsoft.Playwright;
using System;
using System.IO;
using System.Linq;

namespace CognizantSoftvision.Maqs.BasePlaywrightTest
{
    /// <summary>
    /// Generic base Playwright test class
    /// </summary>
    public class BasePlaywrightTest : BaseExtendableTest<IPlaywrightTestObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasePlaywrightTest"/> class
        /// Setup the page for each test class
        /// </summary>
        public BasePlaywrightTest()
        {
        }

        /// <summary>
        /// Gets or sets the PageDriver
        /// </summary>
        public PageDriver PageDriver
        {
            get
            {
                return this.TestObject.PageDriver;
            }

            set
            {
                this.TestObject.OverridePageDriver(value);
            }
        }

        /// <summary>
        /// The default get page function
        /// </summary>
        /// <returns>The page</returns>
        protected virtual PageDriver GetPage()
        {
            return PageDriverFactory.GetDefaultPageDriver();
        }

        /// <summary>
        /// Create a test object
        /// </summary>
        /// <param name="log">Assocatied logger</param>
        /// <returns>The Playwright test object</returns>
        protected override IPlaywrightTestObject CreateSpecificTestObject(ILogger log)
        {
            return new PlaywrightTestObject(() => this.GetPage(), log, this.GetFullyQualifiedTestClassName());
        }

        /// <summary>
        /// Attach or delete Playwright testing artifacts
        /// </summary>
        /// <param name="resultType">The test result</param>
        protected override void BeforeCleanup(TestResultType resultType)
        {
            // Try to take a screen shot
            try
            {
                // Just stop if we are not logging or the driver was not initalized or there is no browser
                if (this.LoggingEnabledSetting == LoggingEnabled.NO || !this.TestObject.GetDriverManager<PlaywrightDriverManager>().IsDriverIntialized() || this.PageDriver.ParentBrower == null)
                {
                    return;
                }

                // The test did not pass or we want it logged regardless
                if (this.LoggingEnabledSetting == LoggingEnabled.YES || resultType != TestResultType.PASS)
                {
                    string fullpath = ((IFileLogger)this.Log).FilePath;
                    string fileNameWithoutExtension = Path.Combine(Path.GetDirectoryName(fullpath), Path.GetFileNameWithoutExtension(fullpath));

                    AttachTestFiles(this.PageDriver.ParentBrower, fileNameWithoutExtension);
                    return;
                }

                // We are not logging these results so delete the recordings
                DeleteTestFiles(this.PageDriver.ParentBrower);
            }
            catch (Exception e)
            {
                this.TryToLog(MessageType.WARNING, $"Failed to attach (or cleanup) Playwright test files: {e.Message}");
            }
        }


        /// <summary>
        /// Attach Playwright related traces and video
        /// </summary>
        /// <param name="browser">Current test browser</param>
        /// <param name="baseName">Fully qualified log file without extension</param>
        private void AttachTestFiles(IBrowser browser, string baseName)
        {
            int append = 0;
            foreach (var context in browser.Contexts)
            {
                string traceFilePath = $"{baseName}_{append++}.zip";

                context.Tracing.StopAsync(new TracingStopOptions
                {
                    Path = $"{traceFilePath}",
                }).Wait();

                foreach (var video in context.Pages.Select(x => x?.Video).Where(v => v != null))
                {
                    this.TestObject.AddAssociatedFile(video?.PathAsync().Result);
                }
            }
        }

        /// <summary>
        /// Delete Playwright related video
        /// </summary>
        /// <param name="browser">Current test browser</param>
        private static void DeleteTestFiles(IBrowser browser)
        {
            foreach (var context in browser.Contexts)
            {
                foreach (var video in context.Pages.Select(x => x?.Video).Where(v => v != null))
                {
                    video?.DeleteAsync();
                }
            }
        }
    }
}