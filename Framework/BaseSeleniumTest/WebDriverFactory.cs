﻿//--------------------------------------------------
// <copyright file="WebDriverFactory.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Web driver factory</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.Utilities.Helper;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace CognizantSoftvision.Maqs.BaseSeleniumTest
{
    /// <summary>
    /// Static web driver factory
    /// </summary>
    public static class WebDriverFactory
    {
        /// <summary>
        /// Path to Chrome web driver
        /// </summary>
        private static String ChromeDriverPath = null;

        /// <summary>
        /// Path to Edge web driver
        /// </summary>
        private static string EdgeDriverPath = null;

        /// <summary>
        /// Path to Firefox web driver
        /// </summary>
        private static String FirefoxDriverPath = null;

        /// <summary>
        /// Path to IE web driver
        /// </summary>
        private static String IEDriverPath = null;

        /// <summary>
        /// Get the default web driver based on the test run configuration
        /// </summary>
        /// <returns>A web driver</returns>
        public static IWebDriver GetDefaultBrowser()
        {
            return GetBrowserWithDefaultConfiguration(SeleniumConfig.GetBrowserType());
        }

        /// <summary>
        /// Get the default web driver (for the specified browser type) based on the test run configuration
        /// </summary>
        /// <param name="browser">The type of browser</param>
        /// <returns>A web driver</returns>
        public static IWebDriver GetBrowserWithDefaultConfiguration(BrowserType browser)
        {
            IWebDriver webDriver = null;
            TimeSpan timeout = SeleniumConfig.GetCommandTimeout();
            string size = SeleniumConfig.GetBrowserSize();
            try
            {
                switch (browser)
                {
                    case BrowserType.IE:
                        webDriver = GetIEDriver(timeout, GetDefaultIEOptions(), size);
                        break;
                    case BrowserType.Firefox:
                        webDriver = GetFirefoxDriver(timeout, GetDefaultFirefoxOptions(), size);
                        break;

                    case BrowserType.Chrome:
                        webDriver = GetChromeDriver(timeout, GetDefaultChromeOptions(), size);
                        break;

                    case BrowserType.HeadlessChrome:
                        webDriver = GetHeadlessChromeDriver(timeout, GetDefaultHeadlessChromeOptions(size));
                        break;

                    case BrowserType.Edge:
                        webDriver = GetEdgeDriver(timeout, GetDefaultEdgeOptions(), size);
                        break;

                    case BrowserType.Remote:
                        webDriver = new RemoteWebDriver(SeleniumConfig.GetHubUri(), GetDefaultRemoteOptions().ToCapabilities(), SeleniumConfig.GetCommandTimeout());
                        break;

                    default:
                        throw new ArgumentException($"Browser type '{browser}' is not supported");
                }

                return webDriver;
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(ArgumentException))
                {
                    throw;
                }
                else
                {
                    try
                    {
                        // Try to cleanup
                        webDriver?.KillDriver();
                    }
                    catch (Exception quitExecption)
                    {
                        throw new WebDriverException("Web driver setup and teardown failed. Your web driver may be out of date", quitExecption);
                    }
                }

                // Log that something went wrong
                throw new WebDriverException("Your web driver may be out of date or unsupported.", e);
            }
        }

        /// <summary>
        /// Get the default Chrome options
        /// </summary>
        /// <returns>The default Chrome options</returns>
        public static ChromeOptions GetDefaultChromeOptions()
        {
            ChromeOptions chromeOptions = new ChromeOptions();

            chromeOptions.SetProxySettings();
            return chromeOptions;
        }

        /// <summary>
        /// Get the default headless Chrome options
        /// </summary>
        /// <param name="size">Browser size in the following format: MAXIMIZE, DEFAULT, or #x# (such as 1920x1080)</param>
        /// <returns>The default headless Chrome options</returns>
        public static ChromeOptions GetDefaultHeadlessChromeOptions(string size = "MAXIMIZE")
        {
            ChromeOptions headlessChromeOptions = new ChromeOptions();
            headlessChromeOptions.AddArgument("--no-sandbox");
            headlessChromeOptions.AddArguments("--headless");
            headlessChromeOptions.AddArguments(GetHeadlessWindowSizeString(size));

            headlessChromeOptions.SetProxySettings();
            return headlessChromeOptions;
        }

        /// <summary>
        /// Get the default IE options
        /// </summary>
        /// <returns>The default IE options</returns>
        public static InternetExplorerOptions GetDefaultIEOptions()
        {
            var options = new InternetExplorerOptions
            {
                IgnoreZoomLevel = true
            };

            options.SetProxySettings();
            return options;
        }

        /// <summary>
        /// Get the default Firefox options
        /// </summary>
        /// <returns>The default Firefox options</returns>
        public static FirefoxOptions GetDefaultFirefoxOptions()
        {
            FirefoxOptions firefoxOptions = new FirefoxOptions
            {
                Profile = new FirefoxProfile()
            };

            firefoxOptions.SetProxySettings();
            return firefoxOptions;
        }

        /// <summary>
        /// Get the default Edge options
        /// </summary>
        /// <returns>The default Edge options</returns>
        public static EdgeOptions GetDefaultEdgeOptions()
        {
            EdgeOptions edgeOptions = new EdgeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };

            edgeOptions.SetProxySettings();
            return edgeOptions;
        }

        /// <summary>
        /// Initialize a new Chrome driver
        /// </summary>
        /// <param name="commandTimeout">Browser command timeout</param>
        /// <param name="chromeOptions">Browser options</param>
        /// <param name="size">Browser size in the following format: MAXIMIZE, DEFAULT, or #x# (such as 1920x1080)</param>
        /// <returns>A new Chrome driver</returns>
        public static IWebDriver GetChromeDriver(TimeSpan commandTimeout, ChromeOptions chromeOptions, string size = "MAXIMIZE")
        {
            return CreateDriver(() =>
            {
                LazyInitializer.EnsureInitialized(ref ChromeDriverPath, () => new DriverManager().SetUpDriver(new ChromeConfig(), SeleniumConfig.GetChromeVersion()));

                var driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), chromeOptions, commandTimeout);
                SetBrowserSize(driver, size);
                return driver;
            }, SeleniumConfig.GetRetryRefused());
        }

        /// <summary>
        /// Initialize a new headless Chrome driver
        /// </summary>
        /// <param name="commandTimeout">Browser command timeout</param>
        /// <param name="headlessChromeOptions">Browser options</param>
        /// <returns>A new headless Chrome driver</returns>
        public static IWebDriver GetHeadlessChromeDriver(TimeSpan commandTimeout, ChromeOptions headlessChromeOptions)
        {
            return CreateDriver(() =>
            {
                LazyInitializer.EnsureInitialized(ref ChromeDriverPath, () => new DriverManager().SetUpDriver(new ChromeConfig(), SeleniumConfig.GetChromeVersion()));

                return new ChromeDriver(ChromeDriverService.CreateDefaultService(), headlessChromeOptions, commandTimeout);
            }, SeleniumConfig.GetRetryRefused());
        }

        /// <summary>
        /// Initialize a new Firefox driver
        /// </summary>
        /// <param name="commandTimeout">Browser command timeout</param>
        /// <param name="firefoxOptions">Browser options</param>
        /// <param name="size">Browser size in the following format: MAXIMIZE, DEFAULT, or #x# (such as 1920x1080)</param>
        /// <returns>A new Firefox driver</returns>
        public static IWebDriver GetFirefoxDriver(TimeSpan commandTimeout, FirefoxOptions firefoxOptions, string size = "MAXIMIZE")
        {
            return CreateDriver(() =>
            {
                LazyInitializer.EnsureInitialized(ref FirefoxDriverPath, () => new DriverManager().SetUpDriver(new FirefoxConfig(), SeleniumConfig.GetFirefoxVersion()));

                // Add support for encoding 437 that was removed in .net core
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                var driver = new FirefoxDriver(Path.GetDirectoryName(FirefoxDriverPath), firefoxOptions, commandTimeout);
                SetBrowserSize(driver, size);

                return driver;
            }, SeleniumConfig.GetRetryRefused());
        }

        /// <summary>
        /// Initialize a new Edge driver
        /// </summary>
        /// <param name="commandTimeout">Browser command timeout</param>
        /// <param name="edgeOptions">Browser options</param>
        /// <param name="size">Browser size in the following format: MAXIMIZE, DEFAULT, or #x# (such as 1920x1080)</param>
        /// <returns>A new Edge driver</returns>
        public static IWebDriver GetEdgeDriver(TimeSpan commandTimeout, EdgeOptions edgeOptions, string size = "MAXIMIZE")
        {
            return CreateDriver(() =>
            {
                LazyInitializer.EnsureInitialized(ref EdgeDriverPath, () => new DriverManager().SetUpDriver(new EdgeConfig(), SeleniumConfig.GetEdgeVersion()));

                var driver = new EdgeDriver(Path.GetDirectoryName(EdgeDriverPath), edgeOptions, commandTimeout);
                SetBrowserSize(driver, size);
                return driver;
            }, SeleniumConfig.GetRetryRefused());
        }

        /// <summary>
        /// Get a new IE driver
        /// </summary>
        /// <param name="commandTimeout">Browser command timeout</param>
        /// <param name="internetExplorerOptions">Browser options</param>
        /// <param name="size">Browser size in the following format: MAXIMIZE, DEFAULT, or #x# (such as 1920x1080)</param>
        /// <returns>A new IE driver</returns>
        public static IWebDriver GetIEDriver(TimeSpan commandTimeout, InternetExplorerOptions internetExplorerOptions, string size = "MAXIMIZE")
        {
            return CreateDriver(() =>
            {
                LazyInitializer.EnsureInitialized(ref IEDriverPath, () => new DriverManager().SetUpDriver(new InternetExplorerConfig(), SeleniumConfig.GetIEVersion()));

                var driver = new InternetExplorerDriver(Path.GetDirectoryName(IEDriverPath), internetExplorerOptions, commandTimeout);
                SetBrowserSize(driver, size);

                return driver;
            }, SeleniumConfig.GetRetryRefused());
        }

        /// <summary>
        /// Get the default remote driver options - Default values are pulled from the configuration
        /// </summary>
        /// <returns>The remote driver options</returns>
        public static DriverOptions GetDefaultRemoteOptions()
        {
            RemoteBrowserType remoteBrowser = SeleniumConfig.GetRemoteBrowserType();
            string remotePlatform = SeleniumConfig.GetRemotePlatform();
            string remoteBrowserVersion = SeleniumConfig.GetRemoteBrowserVersion();
            Dictionary<string, object> capabilities = SeleniumConfig.GetRemoteCapabilitiesAsObjects();

            return GetRemoteOptions(remoteBrowser, remotePlatform, remoteBrowserVersion, capabilities);
        }

        /// <summary>
        /// Get the remote driver options
        /// </summary>
        /// <param name="remoteBrowser">The remote browser type</param>
        /// <returns>The remote driver options</returns>
        public static DriverOptions GetRemoteOptions(RemoteBrowserType remoteBrowser)
        {
            return GetRemoteOptions(remoteBrowser, string.Empty, string.Empty, new Dictionary<string, string>());
        }

        /// <summary>
        /// Get the remote driver options
        /// </summary>
        /// <param name="remoteBrowser">The remote browser type</param>
        /// <param name="remoteCapabilities">Additional remote capabilities</param>
        /// <returns>The remote driver options</returns>
        public static DriverOptions GetRemoteOptions(RemoteBrowserType remoteBrowser, Dictionary<string, string> remoteCapabilities)
        {
            return GetRemoteOptions(remoteBrowser, string.Empty, string.Empty, remoteCapabilities);
        }

        /// <summary>
        /// Get the remote driver options
        /// </summary>
        /// <param name="remoteBrowser">The remote browser type</param>
        /// <param name="remotePlatform">The remote platform</param>
        /// <param name="remoteBrowserVersion">The remote browser version</param>
        /// <param name="remoteCapabilities">Additional remote capabilities</param>
        /// <returns>The remote driver options</returns>
        public static DriverOptions GetRemoteOptions(RemoteBrowserType remoteBrowser, string remotePlatform, string remoteBrowserVersion, Dictionary<string, string> remoteCapabilities)
        {
            Dictionary<string, object> capabilities;
            if (remoteCapabilities != null)
            {
                capabilities = remoteCapabilities.ToDictionary(pair => pair.Key, pair => (object)pair.Value);
            }
            else
            {
                capabilities = new Dictionary<string, object>();
            }
            return GetRemoteOptions(remoteBrowser, remotePlatform, remoteBrowserVersion, capabilities);
        }

        /// <summary>
        /// Get the remote driver options
        /// </summary>
        /// <param name="remoteBrowser">The remote browser type</param>
        /// <param name="remoteCapabilities">Additional remote capabilities</param>
        /// <returns>The remote driver options</returns>
        public static DriverOptions GetRemoteOptions(RemoteBrowserType remoteBrowser, Dictionary<string, object> remoteCapabilities)
        {
            return GetRemoteOptions(remoteBrowser, string.Empty, string.Empty, remoteCapabilities);
        }

        /// <summary>
        /// Get the remote driver options
        /// </summary>
        /// <param name="remoteBrowser">The remote browser type</param>
        /// <param name="remotePlatform">The remote platform</param>
        /// <param name="remoteBrowserVersion">The remote browser version</param>
        /// <param name="remoteCapabilities">Additional remote capabilities</param>
        /// <returns>The remote driver options</returns>
        public static DriverOptions GetRemoteOptions(RemoteBrowserType remoteBrowser, string remotePlatform, string remoteBrowserVersion, Dictionary<string, object> remoteCapabilities)
        {
            DriverOptions options;

            switch (remoteBrowser)
            {
                case RemoteBrowserType.IE:
                    options = new InternetExplorerOptions();
                    break;

                case RemoteBrowserType.Firefox:
                    options = new FirefoxOptions();
                    break;

                case RemoteBrowserType.Chrome:
                    options = new ChromeOptions();
                    break;

                case RemoteBrowserType.Edge:
                    options = new EdgeOptions();
                    break;

                case RemoteBrowserType.Safari:
                    options = new SafariOptions();
                    break;

                default:
                    throw new ArgumentException($"Remote browser type '{remoteBrowser}' is not supported");
            }

            // Make sure the remote capabilities dictionary exists
            if (remoteCapabilities == null)
            {
                remoteCapabilities = new Dictionary<string, object>();
            }

            // Add a platform setting if one was provided
            if (!string.IsNullOrEmpty(remotePlatform) && !remoteCapabilities.ContainsKey("platform"))
            {
                options.PlatformName = remotePlatform;
            }

            // Add a remote browser setting if one was provided
            if (!string.IsNullOrEmpty(remoteBrowserVersion) && !remoteCapabilities.ContainsKey("version"))
            {
                options.BrowserVersion = remoteBrowserVersion;
            }

            // Add additional capabilities to the driver options
            options.SetDriverOptions(remoteCapabilities);
            options.SetProxySettings();
            return options;
        }

        /// <summary>
        /// Add additional capabilities to the driver options
        /// </summary>
        /// <param name="driverOptions">The driver option you want to add capabilities to</param>
        /// <param name="additionalCapabilities">Capabilities to add</param>
        public static void SetDriverOptions(this DriverOptions driverOptions, Dictionary<string, object> additionalCapabilities)
        {
            // If there are no additional capabilities just return
            if (additionalCapabilities == null)
            {
                return;
            }

            foreach (KeyValuePair<string, object> keyValue in additionalCapabilities)
            {
                // Make sure there is a value
                if (keyValue.Value != null && (!(keyValue.Value is string) || !string.IsNullOrEmpty(keyValue.Value as string)))
                {
                    // Handle W3C complient keys
                    if (keyValue.Key.Contains(":"))
                    {
                        SetSingleComplexOption(driverOptions, keyValue.Key, keyValue.Value);
                    }
                    else
                    {
                        SetSingleSimpleOption(driverOptions, keyValue.Key, keyValue.Value);
                    }
                }
            }
        }

        /// <summary>
        /// Sets the proxy settings for the driver options (if configured)
        /// </summary>
        /// <param name="options">The driver options</param>
        public static void SetProxySettings(this DriverOptions options)
        {
            if (SeleniumConfig.GetUseProxy())
            {
                SetProxySettings(options, SeleniumConfig.GetProxyAddress());
            }
        }

        /// <summary>
        /// Sets the proxy settings for the driver options
        /// </summary>
        /// <param name="options">The driver options</param>
        /// <param name="proxyAddress">The proxy address</param>
        public static void SetProxySettings(this DriverOptions options, string proxyAddress)
        {
            if (!string.IsNullOrEmpty(proxyAddress))
            {
                Proxy proxy = new Proxy
                {
                    HttpProxy = proxyAddress,
                    SslProxy = proxyAddress
                };
                options.Proxy = proxy;
            }
        }

        /// <summary>
        /// Creates a web driver, but if the creation fails it tries to cleanup after itself
        /// </summary>
        /// <param name="createFunction">Function for creating a web driver</param>
        /// <param name="retry">If we fail to get the webdriver should we retry</param>
        /// <returns>A web driver</returns>
        public static IWebDriver CreateDriver(Func<IWebDriver> createFunction, bool retry = false)
        {
            IWebDriver webDriver = null;

            try
            {
                return createFunction();
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(ArgumentException))
                {
                    throw;
                }
                else
                {
                    try
                    {
                        // Try to cleanup
                        webDriver?.KillDriver();
                    }
                    catch (Exception quitExecption)
                    {
                        throw new WebDriverException("Web driver setup and teardown failed. Your web driver may be out of date", quitExecption);
                    }
                }

                if (retry && e.Message.ToLower().Contains("refused"))
                {
                    return CreateDriver(createFunction, false);
                }
                else
                {
                    // Log that something went wrong
                    throw new WebDriverException("Your web driver may be out of date or unsupported.", e);
                }
            }
        }

        /// <summary>
        /// Sets the browser size based on the provide string value
        /// Browser size is expected to be: MAXIMIZE, DEFAULT, or #x# (such as 1920x1080)
        /// MAXIMIZE just maximizes the bowser
        /// DEFAULT does not change the current size
        /// #x# sets a custom size
        /// </summary>
        /// <param name="webDriver">the webDriver from the Browser method</param>
        /// <param name="size">Browser size in the following format: MAXIMIZE, DEFAULT, or #x# (such as 1920x1080)</param>
        public static void SetBrowserSize(IWebDriver webDriver, string size)
        {
            size = size.ToUpper();

            if (size == "MAXIMIZE")
            {
                webDriver.Manage().Window.Maximize();
            }
            else if (size != "DEFAULT")
            {
                StringProcessor.ExtractSizeFromString(size, out int width, out int height);
                webDriver.Manage().Window.Size = new Size(width, height);
            }
        }

        /// <summary>
        /// Get the browser/browser size as a string
        /// </summary>
        /// <param name="size">Browser size in the following format: MAXIMIZE, DEFAULT, or #x# (such as 1920x1080)</param>
        /// <returns>The browser size as a string - Specifically for headless Chrome options</returns>
        private static string GetHeadlessWindowSizeString(string size)
        {
            if (size == "MAXIMIZE" || size == "DEFAULT")
            {
                // If we need a string default to 1920 by 1080
                return "window-size=1920,1080";
            }
            else
            {
                StringProcessor.ExtractSizeFromString(size, out int width, out int height);
                return string.Format("window-size={0},{1}", width, height);
            }
        }

        /// <summary>
        /// Add single complex (AKA with colon) option
        /// </summary>
        /// <param name="driverOptions">Driver to add the option to</param>
        /// <param name="key">Option key</param>
        /// <param name="value">Option value</param>
        private static void SetSingleComplexOption(this DriverOptions driverOptions, string key, object value)
        {
            try
            {
                // Check if this is a Json string
                var jsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(value as string);
                driverOptions.AddAdditionalOption(key, jsonDictionary);
            }
            catch
            {
                // Not Json string so add as a normal string
                driverOptions.AddAdditionalOption(key, value);
            }
        }

        /// <summary>
        /// Add single simple (AKA has no colon) option
        /// </summary>
        /// <param name="driverOptions">Driver to add the option to</param>
        /// <param name="key">Option key</param>
        /// <param name="value">Option value</param>
        private static void SetSingleSimpleOption(this DriverOptions driverOptions, string key, object value)
        {
            switch (driverOptions)
            {
                case ChromeOptions chromeOptions:
                    chromeOptions.AddAdditionalChromeOption(key, value);
                    break;
                case FirefoxOptions firefoxOptions:
                    firefoxOptions.AddAdditionalFirefoxOption(key, value);
                    break;
                case InternetExplorerOptions ieOptions:
                    ieOptions.AddAdditionalInternetExplorerOption(key, value);
                    break;
                case EdgeOptions ieOptions:
                    ieOptions.AddAdditionalEdgeOption(key, value);
                    break;
                default:
                    // Not one of our 4 main types
                    driverOptions.AddAdditionalOption(key, value);
                    break;
            }
        }
    }
}