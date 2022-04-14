# <img src="resources/maqslogo.ico" height="32" width="32"> Playwright Test Object

## Overview
Playwright test context data

## WebManager
Gets the Playwright driver manager
```csharp
PlaywrightDriverManager manager = this.TestObject.PageManager();
```

## PageDriver
Gets the Playwright page driver
```csharp
public PageDriver PageDriver
{
    get
    {
        return this.TestObject.PageManager.GetPageDriver();
    }
}
```

## OverridePageDriver
Override the Playwright page driver
```csharp
public void OverridePageDriver(PageDriver PageDriver)
{
    this.OverrideDriverManager(typeof(PlaywrightDriverManager).FullName, new PlaywrightDriverManager(() => PageDriver, this));
}
```

Override the function for creating a Playwright page driver
```csharp
public void OverridePageDriver(Func<PageDriver> getDriver)
{
    this.OverrideDriverManager(typeof(PlaywrightDriverManager).FullName, new PlaywrightDriverManager(getDriver, this));
}
```
