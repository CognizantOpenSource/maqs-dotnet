# <img src="resources/maqslogo.ico" height="32" width="32"> Email Basics

## Overview
MAQS provides support for testing Email applictions.  

## BaseEmailTest
BaseAppiumTest is an abstract test class you can extend.  Extending the class allows you to automatically use MAQS's web service testing capabilities.
```csharp
[TestClass]
public class MyAppiumTests : BaseEmailTest
```

## EmailDriver
The EmailDriver is an object that allows you to interact with appium services.  
This driver wraps common web service interactions, making appium testing relatively easy.  
The driver is also thread safe, which means you can run multiple appium tests in parallel.  
*Information, such as the OS version is pulled from the MAQS configuration.
```csharp
AppiumDriver<IWebElement> driver = AppiumDriverFactory.GetDefaultMobileDriver();
```

## Log
There is also logger (also thread safe) the can be used to add log message to your log.
```csharp
this.Log.LogMessage("I am testing with MAQS");
```

## EmailTestObject
The TestObject can be thought of as your test context.  It holds all the MAQS test execution replated data.  This includes the Email driver, logger, soft asserts, performance timers, plus more.
```csharp
this.TestObject.WebDriver.Navigate().GoToUrl("http://magenicautomation.azurewebsites.net/");
this.TestObject.Log.LogMessage("I am testing with MAQS");
```
*Notes:*  
* *Most of the test object objects are already accessible on the test level. For example **this.Log** and **this.TestObject.Log** both access the same logger.*
* *You seldom use the test object directly. It is usually only used when you want to share your test MAQS context with another piece of code*

## Sample code
```csharp
using Magenic.Maqs.BaseAppiumTest;
using Magenic.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Tests
{
    /// <summary>
    /// Test for creating Mobile Device driver
    /// </summary>
    [TestMethod]
    [TestCategory(TestCategories.Appium)]
    public void MobileDeviceTest()
    {
		PageModel page = new PageModel(this.TestObject);
        page.OpenPage();
    }
}
```