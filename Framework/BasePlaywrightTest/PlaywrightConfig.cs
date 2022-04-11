//--------------------------------------------------
// <copyright file="PlaywrightConfig.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Helper class for getting Playwright specific configuration values</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.Utilities.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace CognizantSoftvision.Maqs.BasePlaywrightTest
{
    /// <summary>
    /// Config class
    /// </summary>
    public static class PlaywrightConfig
    {
        /// <summary>
        /// Loads when class is loaded
        /// </summary>
        static PlaywrightConfig()
        {
            CheckConfig();
        }

        /// <summary>
        /// Ensure required fields are in the config
        /// </summary>
        private static void CheckConfig()
        {
            var validator = new ConfigValidation()
            {
                RequiredFields = new List<string>()
                {
                    "Timeout"
                }
            };
            Config.Validate(ConfigSection.PlaywrightMaqs, validator);
        }

        /// <summary>
        ///  Static name for the Playwright configuration section
        /// </summary>
        private const string PlaywrightSECTION = "PlaywrightMaqs";


        /// <summary>
        /// Get the web site base url
        /// </summary>
        /// <returns>The web site base url</returns>
        public static string GetWebBase()
        {
            return Config.GetValueForSection(PlaywrightSECTION, "WebBase");
        }

        /// <summary>
        /// Get the browser type
        /// </summary>
        /// <returns>The browser type</returns>
        public static PlaywrightBrowser GetBrowserType()
        {
            return GetBrowserType(GetBrowserName());
        }

        /// <summary>
        /// Get the browser type based on the provide browser name
        /// </summary>
        /// <param name="browserName">Name of the browser</param>
        /// <returns>The browser type</returns>
        public static PlaywrightBrowser GetBrowserType(string browserName)
        {
            return browserName.ToUpper() switch
            {
                "CHROME" => PlaywrightBrowser.Chrome,
                "CHROMIUM" => PlaywrightBrowser.Chromium,
                "FIREFOX" => PlaywrightBrowser.Firefox,
                "EDGE" => PlaywrightBrowser.Edge,
                "WEBKIT" => PlaywrightBrowser.Webkit,
                _ => throw new ArgumentException($"Browser type '{browserName}' is not supported"),
            };
        }

        /// <summary>
        /// Get if we should run Playwright headless
        /// </summary>
        /// <returns>True if we want to run Playwright headless</returns>
        public static bool GetHeadless()
        {
            return Config.GetValueForSection(PlaywrightSECTION, "Headless").Equals("Yes", StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Get the browser type name - Example: Chrome
        /// </summary>
        /// <returns>The browser type</returns>
        public static string GetBrowserName()
        {
            return Config.GetValueForSection(PlaywrightSECTION, "Browser", "Chrome");
        }

        /// <summary>
        /// Get the initialize Playwright timeout
        /// </summary>
        /// <returns>The initialize timeout</returns>
        public static int GetCommandTimeout()
        {
            string value = Config.GetValueForSection(PlaywrightSECTION, "CommandTimeout", "60000");

            if (!int.TryParse(value, out int timeout))
            {
                throw new ArgumentException($"PlaywrightCommandTimeout should be a number but the current value is: {value}");
            }

            return timeout;
        }

        /// <summary>
        /// Get the timeout in milliseconds
        /// </summary>
        /// <returns>The timeout in milliseconds</returns>
        public static int GetTimeoutTime()
        {
            return Convert.ToInt32(Config.GetValueForSection(PlaywrightSECTION, "Timeout"));
        }

        /// <summary>
        /// Get if we want to capture video - This may bloat your test result
        /// </summary>
        /// <returns>True if we want to capture video</returns>
        public static bool GetCaptureVideo()
        {
            return Config.GetValueForSection(PlaywrightSECTION, "CaptureVideo", "NO").Equals("Yes", StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Get if we want to capture screenshots - This may bloat your test result
        /// </summary>
        /// <returns>True if we want to capture screenshots</returns>
        public static bool GetCaptureScreenshots()
        {
            return Config.GetValueForSection(PlaywrightSECTION, "CaptureScreenshots", "NO").Equals("Yes", StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Get if we want to capture snapshots - This may bloat your test result
        /// </summary>
        /// <returns>True if we want to capture snapshots</returns>
        public static bool GetCaptureSnapshots()
        {
            return Config.GetValueForSection(PlaywrightSECTION, "CaptureSnapshots", "NO").Equals("Yes", StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// get the browser size
        /// </summary>
        /// <returns></returns>
        public static Size GetBrowserSize()
        {
            string size = Config.GetValueForSection(PlaywrightSECTION, "BrowserSize", "DEFAULT").ToUpper();

            if (size.Equals("DEFAULT"))
            {
                return new Size(1280, 720);
            }

            StringProcessor.ExtractSizeFromString(size, out int width, out int height);
            return new Size(width, height);
        }

        /// <summary>
        /// Get if we want to use a proxy for the page traffic
        /// </summary>
        /// <returns>True if we want to use the proxy</returns>
        public static bool GetUseProxy()
        {
            return Config.GetValueForSection(PlaywrightSECTION, "UseProxy", "NO").Equals("Yes", StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Get the proxy address to use
        /// </summary>
        /// <returns>The proxy address</returns>
        public static string GetProxyAddress()
        {
            return Config.GetValueForSection(PlaywrightSECTION, "ProxyAddress");
        }
    }
}