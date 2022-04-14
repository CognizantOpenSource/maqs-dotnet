# <img src="resources/maqslogo.ico" height="32" width="32"> Base Playwright Test

## Overview
The BasePlaywrightTest class provides access to the PlaywrightTestObject and PlaywrightDriver.

# Available (overridable) calls
[GetPage](#GetPage)  
[BeforeCleanup](#BeforeCleanup)  
[CreateSpecificTestObject](#CreateSpecificTestObject)  

## GetPage
The default get page driver function.
```csharp
protected virtual PageDriver GetPage()
{
    return PageDriverFactory.GetDefaultPageDriver();
}
```

## BeforeCleanup
Cleanup before tear down the page driver.
```csharp
protected override void BeforeCleanup(TestResultType resultType)
{
    // Try to take a screen shot
    try
    {
        // Just stop if we are not logging or the driver was not initalized or there is no browser
        if (this.LoggingEnabledSetting == LoggingEnabled.NO || !this.TestObject.GetDriverManager<PlaywrightDriverManager>().IsDriverIntialized() || this.PageDriver.ParentBrower == null)
        {
            return;
        }

        // The test did not pass or we want it logged regardless
        if (this.LoggingEnabledSetting == LoggingEnabled.YES || resultType != TestResultType.PASS)
        {
            string fullpath = ((IFileLogger)this.Log).FilePath;
            string fileNameWithoutExtension = Path.Combine(Path.GetDirectoryName(fullpath), Path.GetFileNameWithoutExtension(fullpath));

            AttachTestFiles(this.PageDriver.ParentBrower, fileNameWithoutExtension);
            return;
        }
                
        // We are not logging these results so delete the recordings
        DeleteTestFiles(this.PageDriver.ParentBrower);
    }
    catch (Exception e)
    {
        this.TryToLog(MessageType.WARNING, $"Failed to attach (or cleanup) Playwright test files: {e.Message}");
    }
}
```

## CreateSpecificTestObject
Create a Playwright test object.
```csharp
protected override IPlaywrightTestObject CreateSpecificTestObject(ILogger log)
{
    return new PlaywrightTestObject(() => this.GetPage(), log, this.GetFullyQualifiedTestClassName());
}
```