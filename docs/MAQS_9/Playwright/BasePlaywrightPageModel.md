# <img src="resources/maqslogo.ico" height="32" width="32"> Base Playwright Page Model

## Overview
An abstract base page model class that makes creating and managing page models easy

### TestObject
Get the test object associated with the model 
```csharp
IPlaywrightTestObject pageObjectTestObject = pageModel.TestObject;
``` 

### Log
Get the logger assoicate with the page object

```csharp
ILogger pageObjectLogger =  pageModel.Log;
``` 

### PerfTimerCollection
Get the performance timer collation associated with the model 
```csharp
IPerfTimerCollection pageObjectPerfTimer = pageModel.PerfTimerCollection;
``` 

### PageDriver
Get the page driver associated with the model 

```csharp
PageDriver pageObjectPageDriver = pageModel.PageDriver;
``` 

### OverridePageDriver
Associate the model with a different page driver

```csharp
pageModel.OverridePageDriver(otherDriver);
```

## Sample Usage
```csharp
public class PageModel : BasePlaywrightPageModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PageModel"/> class
    /// </summary>
    /// <param name="testObject">The base Playwright test object</param>
    public PageModel(IPlaywrightTestObject testObject)
        : base(testObject)
    {
    }

    /// <summary>
    /// Get page url
    /// </summary>
    public static string Url
    {
        get { return PlaywrightConfig.WebBase(); }
    }

    /// <summary>
    /// Should dialog button
    /// </summary>
    public PlaywrightSyncElement ShowDialog1
    {
        get { return new PlaywrightSyncElement(this.PageDriver, "#showDialog1"); }
    }

    /// <summary>
    /// Close dialog
    /// </summary>
    public PlaywrightSyncElement CloseButtonShowDialog
    {
        get { return new PlaywrightSyncElement(this.PageDriver, "#CloseButtonShowDialog"); }
    }

    /// <summary>
    /// Open the page
    /// </summary>
    public void OpenPage()
    {
        this.PageDriver.Goto(Url);
    }

    /// <summary>
    /// Check if the page has been loaded
    /// </summary>
    /// <returns>True if the page was loaded</returns>
    public override bool IsPageLoaded()
    {
        return true;
    }
}
```