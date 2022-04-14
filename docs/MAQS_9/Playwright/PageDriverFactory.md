# <img src="resources/maqslogo.ico" height="32" width="32"> WebDriver Factory

## Overview
A static page driver factory that deals with browser/page options and configurations

[GetDefaultPageDriver](#GetDefaultPageDriver)  
[GetPageDriverForBrowserWithDefaults](#GetPageDriverForBrowserWithDefaults)  
[GetBrowserWithDefaults](#GetBrowserWithDefaults)  
[GetPageDriverFromBrowser](#GetPageDriverFromBrowser)  
[GetNewPageDriverFromBrowserContext](#GetNewPageDriverFromBrowserContext)  
[GetChromiumBasedBrowser](#GetChromiumBasedBrowser)  
[GetFirefoxBasedBrowser](#GetFirefoxBasedBrowser)  
[GetWebkitBasedBrowser](#GetWebkitBasedBrowser)  
[GetDefaultOptions](#GetDefaultOptions)  
[GetDefaultChromeOptions](#GetDefaultChromeOptions)  
[GetDefaultEdgeOptions](#GetDefaultEdgeOptions)  

## GetDefaultPageDriver
Get the default page driver based on the test run configuration 
```csharp
PageDriver tempDriver = PageDriverFactory.GetDefaultPageDriver();
```

## GetPageDriverForBrowserWithDefaults
Get the default page driver (for the specified browser type) based on the test run configuration 
```csharp
PageDriver tempDriver = PageDriverFactory.GetPageDriverForBrowserWithDefaults(PlaywrightBrowser.Chrome);
```

## GetBrowserWithDefaults
Get a browser with the default run configuration 
```csharp
IBrowser tempBrowser = PageDriverFactory.GetBrowserWithDefaults(PlaywrightBrowser.Edge);
```

## GetPageDriverFromBrowser
Get a new page driver for a given browser
*This basically creates a new tab in the primary browser context
```csharp
IBrowser tempBrowser = PageDriverFactory.GetBrowserWithDefaults(PlaywrightBrowser.Edge);
PageDriver tempPage = PageDriverFactory.GetPageDriverFromBrowser(tempBrowser);
```

## GetNewPageDriverFromBrowserContext
Get a new page driver for a given browser context
*This basically creates a new tab
```csharp
IBrowser tempBrowser = PageDriverFactory.GetBrowserWithDefaults(PlaywrightBrowser.Webkit);
PageDriver tempPage = PageDriverFactory.GetNewPageDriverFromBrowserContext(tempBrowser.Contexts[0]);
```

## GetChromiumBasedBrowser
Get a new Chromium base browser
```csharp
IPlaywright newPlaywright = Playwright.CreateAsync().Result;
IBrowser tempBrowser = PageDriverFactory.GetChromiumBasedBrowser(newPlaywright, new BrowserTypeLaunchOptions() { Channel = "msedge" });
```

## GetFirefoxBasedBrowser
Get a new Firefox base browser
```csharp
IPlaywright newPlaywright = Playwright.CreateAsync().Result;
IBrowser tempBrowser = PageDriverFactory.GetFirefoxBasedBrowser(newPlaywright, new BrowserTypeLaunchOptions());
```

## GetWebkitBasedBrowser
Get a new WebKit base browser
```csharp
IPlaywright newPlaywright = Playwright.CreateAsync().Result;
IBrowser tempBrowser = PageDriverFactory.GetWebkitBasedBrowser(newPlaywright, new BrowserTypeLaunchOptions());
```

## GetDefaultOptions 
Get the default options
*These options include proxy, timeout, and headless settings
```csharp
BrowserTypeLaunchOptions options = GetDefaultOptions();
```

## GetDefaultChromeOptions 
Get the default Chrome + Chrome specification
```csharp
BrowserTypeLaunchOptions options = GetDefaultChromeOptions();
```

## GetDefaultEdgeOptions 
Get the default options + Edge specification
```csharp
BrowserTypeLaunchOptions options = GetDefaultEdgeOptions();
```