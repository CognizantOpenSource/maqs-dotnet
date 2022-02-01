# <img src="resources/maqslogo.ico" height="32" width="32"> Base Appium Page Model

## Overview
An abstract base page model class that makes creating and managing page models easy

### OverrideDriver
Associate the model with a different Appium driver

```csharp
pageModel.OverrideDriver(otherDriver);
```

## Sample Usage
```csharp
public class AppiumPageModel : BaseAppiumPageModel
{
	/// <summary>
	/// Initializes a new instance of the <see cref="AppiumPageModel"/> class
	/// </summary>
	/// <param name="testObject">The base Appium test object</param>
	public AppiumPageModel(IAppiumTestObject testObject)
		: base(testObject)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="AppiumPageModel"/> class
	/// </summary>
	/// <param name="testObject">The base Appium test object</param>
	/// <param name="appiumDriver">Appium driver to use</param>
	public AppiumPageModel(IAppiumTestObject testObject, AppiumDriver appiumDriver)
		: base(testObject, appiumDriver)
	{
	}

	/// <summary>
	/// Open the page
	/// </summary>
	public void OpenPage()
	{
		this.AppiumDriver.Navigate().GoToUrl(Config.GetValueForSection(ConfigSection.AppiumMaqs, "WebSiteBase"));
	}

	/// <summary>
	/// Get the top level element
	/// </summary>
	public LazyMobileElement TopLevel
	{
		get { return this.GetLazyElement(MobileBy.XPath("//*[@class=\"jumbotron\"]"), "Top level"); }
	}

	/// <summary>
	/// Check if the page has been loaded
	/// </summary>
	/// <returns>True if the page was loaded</returns>
	public override bool IsPageLoaded()
	{
		return TopLevel.Exists;
	}
}
```