# <img src="resources/maqslogo.ico" height="32" width="32"> Playwright Basics

## Overview
MAQS provides support for testing web application.  

## BaseTest
BasePlaywrightTest is an abstract test class you can extend.  Extending the class allows you to automatically use MAQS's web application testing capabilities.
```csharp
[TestClass]
public class MyPlaywrightTest : BasePlaywrightTest
```

## PageDriver
The PageDriver is an object that allows you to interact with web pages. MAQS wrapps the Playwright PageDriver so it can be used in a syncronys manner. User can however still access the native asynchronous page element from the PageDriver by calling AsyncPage.
Also note that the driver is thread safe, which means you can run multiple web tests in parallel.   
*Notes:*
* PageDriver and Playwight are thread safe and allow you to run tests in parallel
* Playwright largely does its own logging so the MAQS logs for Playwright are typically very sparse
* For more info on the Playwright driver you can visit the Playwright GitHub page: https://github.com/microsoft/playwright-dotnet. and/or docs: https://playwright.dev/dotnet/

### Underlying stucture
The page driver wraps a IPage.
The IPage is linked to a IBrowser context.
The IBrowser can have N number of contexts.
*You can think a browser context as a incognito browsers and pages as tabs in said browser
The IBrowser is created using IPlaywright
The IPlaywright can create N number of IBrowsers.
*You can think a IPlaywright of the base playwright node service 

## PageManager
Manages fuctions that interact with the page driver

## Configuration 
Information, such as the type of browser and website base url are pulled from the PlaywrightMaqs section your configuration.
```csharp
 this.WebDriver.Goto(PlaywrightConfig.WebBase());
```

## Log
There is also logger (also thread safe) the can be used to add log message to your log.
```csharp
this.Log.LogMessage("I am testing with MAQS");
```
## TestObject
The TestObject can be thought of as your test context.  It holds all the MAQS test execution replated data.  This includes the Playwright driver, logger, soft asserts, performance timers, plus more.
```csharp
this.TestObject.PageDriver.Goto("https://cognizantopensource.github.io/maqs-dotnet-templates/Static/Automation/");
this.TestObject.Log.LogMessage("I am testing with MAQS");
```
*Notes:*  
* *Most of the test object objects are already accessible on the test lever. For example **this.Log** and **this.TestObject.Log** both access the same logger.*
* *You seldom what you use the test object directly.  It is usually only used when you want to share your test MAQS context with another piece of code*

## PlaywrightSyncElement
Syncronys element wrapper

## Sample code
```csharp
using Cognizant.Maqs.BasePlaywrightTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    /// <summary>
    /// PlaywrightTest test class
    /// </summary>
    [TestClass]
    public class PlaywrightTest : BasePlaywrightTest
    {
        [TestMethod]
        public void HomePageTitle()
            this.PageDriver.Goto(PlaywrightConfig.WebBase());
            this.Log.LogMessage("I am testing with MAQS");
            Assert.AreEqual("HOME", this.PageDriver.Title());
        }
    }
}
```