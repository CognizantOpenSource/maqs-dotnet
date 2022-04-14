# <img src="resources/maqslogo.ico" height="32" width="32"> Playwright Driver Manager

## Overview
Playwright driver manager

[GetPageDriver](#GetPageDriver)  
[Get](#Get)  
[LogVerbose](#LogVerbose)  
[DriverDispose](#DriverDispose)  
[LoggingStartup](#LoggingStartup)  


## GetPageDriver
Get the page driver
```csharp
PageDriver driver = this.GetPageDriver();
``

## Get
Get the web driver
```csharp
public override object Get()
{
    return this.GetPageDriver();
}
```

## LogVerbose
Log a verbose message and include the automation specific call stack data
```csharp
this.LogVerbose("Navigating to: {0}", e.Url);
```

## DriverDispose
Have the driver cleanup after itself
```csharp
protected override void DriverDispose()
{
    Log.LogMessage(MessageType.VERBOSE, "Start dispose driver");

    // If we never created the driver we don't have any cleanup to do
    if (!this.IsDriverIntialized())
    {
        return;
    }

    try
    {
        PageDriver driver = this.GetPageDriver();
        driver.AsyncPage?.CloseAsync().Wait();
    }
    catch (Exception e)
    {
        Log.LogMessage(MessageType.ERROR, $"Failed to close page because: {e.Message}");
    }

    this.BaseDriver = null;
    Log.LogMessage(MessageType.VERBOSE, "End dispose driver");
}
```

## LoggingStartup
Log that the web driver setup
```csharp
this.LoggingStartup(tempDriver);
```