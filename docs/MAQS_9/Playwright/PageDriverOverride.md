# <img src="resources/maqslogo.ico" height="32" width="32"> Overriding The Playwright PageDriver

## Overriding the PageDriver 
By default, BasePlaywrightTest will create a PageDriver for you based on your [configuration](MAQS_9/Playwright/PlaywrightConfig.md). This typically works for most instances, but there are times when the default PageDriver implementation provide by MAQS does not suit your needs. This is why we provide several different ways for you to provide your own PageDriver implementation.

There are three primary ways to override the PageDriver.

### Override the base Playwright test get PageDriver function
```csharp
[TestClass]
public class YOURTESTCLASS : BasePlaywrightTest
{
    /// <summary>
    /// Get the PageDriver
    /// </summary>
    /// <returns>The PageDriver</returns>
    protected override PageDriver GetPage()
    {
        return YourNewPageDriverFunction();
    }
```
### Override how to get the driver
```csharp
// Override with a function call
this.TestObject.OverridePageDriver(YourNewPageDriverFunction);

// Override with a lambda expression
this.TestObject.OverridePageDriver(() => PageDriverFactory.GetPageDriverForBrowserWithDefaults(PlaywrightBrowser.Edge));
```

### Override the driver directly
```csharp
// Override with a driver
PageDriver overrideDriver = YourNewPageDriverFunction();
this.TestObject.OverridePageDriver(overrideDriver);

// Override the driver directly 
PageDriver driver = YourNewPageDriverFunction();
this.PageDriver = driver;
```