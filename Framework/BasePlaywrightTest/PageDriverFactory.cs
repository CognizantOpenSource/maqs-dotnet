//--------------------------------------------------
// <copyright file="PageDriverFactory.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>page factory</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.Utilities.Logging;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;


namespace CognizantSoftvision.Maqs.BasePlaywrightTest
{
    /// <summary>
    /// Factory for creating page drivers
    /// </summary>
    public static class PageDriverFactory
    {
        /// <summary>
        /// Get the default page based on the test run configuration
        /// </summary>
        /// <returns>A page</returns>
        public static PageDriver GetDefaultPageDriver()
        {
            return GetPageDriverForBrowserWithDefaults(PlaywrightConfig.BrowserType());
        }

        /// <summary>
        /// Get the default page (for the specified browser type) based on the test run configuration
        /// </summary>
        /// <param name="browser">The type of browser</param>
        /// <returns>A page</returns>
        public static IBrowser GetBrowserWithDefaults(PlaywrightBrowser browser)
        {
            var playwright = Playwright.CreateAsync().Result;

            IBrowser? asyncBrowser = browser switch
            {
                PlaywrightBrowser.Chromium => playwright.GetChromiumBasedBrowser(GetDefaultOptions()),
                PlaywrightBrowser.Chrome => playwright.GetChromiumBasedBrowser(GetDefaultChromeOptions()),
                PlaywrightBrowser.Edge => playwright.GetChromiumBasedBrowser(GetDefaultEdgeOptions()),
                PlaywrightBrowser.Firefox => playwright.GetFirefoxBasedBrowser(GetDefaultOptions()),
                PlaywrightBrowser.Webkit => playwright.GetWebkitBasedBrowser(GetDefaultOptions()),
                _ => throw new ArgumentException($"Browser type '{browser}' is not supported"),
            };

            return asyncBrowser;
        }

        /// <summary>
        /// Get the default page (for the specified browser type) based on the test run configuration
        /// </summary>
        /// <param name="browser">The type of browser</param>
        /// <returns>A page</returns>
        public static PageDriver GetPageDriverForBrowserWithDefaults(PlaywrightBrowser browser)
        {
            IBrowser asyncBrowser = GetBrowserWithDefaults(browser);
            return GetPageDriverFromBrowser(asyncBrowser);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="browser">The current browser</param>
        /// <returns>A page</returns>
        public static PageDriver GetPageDriverFromBrowser(IBrowser browser)
        {
            IBrowserContext context;

            // Get resolution
            var resolution = PlaywrightConfig.BrowserSize();
            ViewportSize size = new ViewportSize() { Width = resolution.Width, Height = resolution.Height };

            // Default to the first context, if at least one context exists
            if (browser.Contexts.Count > 0)
            {
                return GetNewPageDriverFromBrowserContext(browser.Contexts[0]);
            }

            if (LoggingConfig.GetLoggingEnabledSetting() != LoggingEnabled.NO)
            {
                if (PlaywrightConfig.CaptureVideo())
                {
                    context = browser.NewContextAsync(new BrowserNewContextOptions
                    {
                        RecordVideoDir = $"{LoggingConfig.GetLogDirectory()}/videos/",
                        ViewportSize = size,
                    }).Result;
                }
                else
                {
                    context = browser.NewContextAsync(new BrowserNewContextOptions
                    {
                        ViewportSize = size,
                    }).Result;
                }

                // Start tracing before creating / navigating a page.
                context.Tracing.StartAsync(new TracingStartOptions
                {
                    Screenshots = PlaywrightConfig.CaptureScreenshots(),
                    Snapshots = PlaywrightConfig.CaptureSnapshots(),
                    Sources = true,
                }).Wait();
            }
            else
            {
                context = browser.NewContextAsync(new BrowserNewContextOptions
                {
                    ViewportSize = size,
                }).Result;
            }

            return GetNewPageDriverFromBrowserContext(context);
        }

        /// <summary>
        /// Get a new page driver for the given browser context
        /// </summary>
        /// <param name="context">The current browser's context</param>
        /// <returns>A page</returns>
        public static PageDriver GetNewPageDriverFromBrowserContext(IBrowserContext context)
        {
            IPage page = context.NewPageAsync().Result;
            page.SetDefaultTimeout(PlaywrightConfig.TimeoutTime());
            page.SetDefaultNavigationTimeout(PlaywrightConfig.TimeoutTime());

            return new PageDriver(page);
        }

        /// <summary>
        /// Get Chromium browser
        /// </summary>
        /// <param name="playwright">Playw</param>
        /// <param name="options">Browser options</param>
        /// <returns>A Chromium browser</returns>
        public static IBrowser GetChromiumBasedBrowser(this IPlaywright playwright, BrowserTypeLaunchOptions options)
        {
            return playwright.Chromium.LaunchAsync(options).Result;
        }

        /// <summary>
        /// Get Firefox browser
        /// </summary>
        /// <param name="playwright">Playw</param>
        /// <param name="options">Browser options</param>
        /// <returns>A Firefox browser</returns>
        public static IBrowser GetFirefoxBasedBrowser(this IPlaywright playwright, BrowserTypeLaunchOptions options)
        {
            return playwright.Firefox.LaunchAsync(options).Result;
        }

        /// <summary>
        /// Get Webkit browser
        /// </summary>
        /// <param name="playwright">Playw</param>
        /// <param name="options">Browser options</param>
        /// <returns>A Webkit browser</returns>
        public static IBrowser GetWebkitBasedBrowser(this IPlaywright playwright, BrowserTypeLaunchOptions options)
        {
            return playwright.Webkit.LaunchAsync(options).Result;
        }

        /// <summary>
        /// Get the default Chrome options
        /// </summary>
        /// <returns>The default Chrome options</returns>
        public static BrowserTypeLaunchOptions GetDefaultChromeOptions()
        {
            BrowserTypeLaunchOptions options = GetDefaultOptions();
            options.Channel = "chrome";

            return options;
        }

        /// <summary>
        /// Get the default Edge options
        /// </summary>
        /// <returns>The default Chrome options</returns>
        public static BrowserTypeLaunchOptions GetDefaultEdgeOptions()
        {
            BrowserTypeLaunchOptions options = GetDefaultOptions();
            options.Channel = "msedge";

            return options;
        }


        /// <summary>
        /// Get the default options
        /// </summary>
        /// <returns>The default options</returns>
        public static BrowserTypeLaunchOptions GetDefaultOptions()
        {
            // Check if we should add proxy
            if(PlaywrightConfig.UseProxy())
            {
                return new BrowserTypeLaunchOptions
                {
                    Proxy = new Proxy { Server = PlaywrightConfig.ProxyAddress() },
                    Headless = PlaywrightConfig.Headless(),
                    Timeout = PlaywrightConfig.CommandTimeout(),
                };

            }

            // Return options without proxy
            return new BrowserTypeLaunchOptions
            {
                Headless = PlaywrightConfig.Headless(),
                Timeout = PlaywrightConfig.CommandTimeout(),
            };
        }
    }
}