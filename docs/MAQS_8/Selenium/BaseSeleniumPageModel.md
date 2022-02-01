# <img src="resources/maqslogo.ico" height="32" width="32"> Base Selenium Page Model

## Overview
An abstract base page model class that makes creating and managing page models easy

### TestObject
Get the test object associated with the model 
```csharp
ISeleniumTestObject pageObjectTestObject = pageModel.GetTestObject();
``` 

### GetLogger
Get the logger assoicate with the page object

```csharp
ILogger pageObjectLogger =  pageModel.GetLogger();
``` 

### GetPerfTimerCollection
Get the performance timer collation associated with the model 
```csharp
IPerfTimerCollection pageObjectPerfTimer = pageModel.GetPerfTimerCollection();
``` 

### GetWebDriver
Get the web driver associated with the model 

```csharp
IWebDriver pageObjectWebDriver = pageModel.GetWebDriver();
``` 

### OverrideWebDriver
Associate the model with a different web driver

```csharp
pageModel.OverrideWebDriver(otherDriver);
```

## Sample Usage
```csharp
public class SeleniumPageModel : BaseSeleniumPageModel
{
	/// <summary>
	/// Initializes a new instance of the <see cref="SeleniumPageModel"/> class
	/// </summary>
	/// <param name="testObject">The base Selenium test object</param>
	public SeleniumPageModel(ISeleniumTestObject testObject)
		: base(testObject)
	{
	}
	
	/// <summary>
	/// Initializes a new instance of the <see cref="SeleniumPageModel"/> class
	/// </summary>
	/// <param name="testObject">The base Selenium test object</param>
	/// <param name="customDriver">Web driver to use instead of the default testObject web driver</param>
	public SeleniumPageModel(ISeleniumTestObject testObject, IWebDriver customDriver)
		: base(testObject, customDriver)
	{
	}

	/// <summary>
	/// Get page url
	/// </summary>
	public static string Url
	{
		get { return SeleniumConfig.GetWebSiteBase() + "Automation"; }
	}

	/// <summary>
	/// Gets a parent element
	/// </summary>
	public LazyElement FlowerTableLazyElement
	{
		get { return this.GetLazyElement(By.CssSelector("#FlowerTable"), "Flower table"); }
	}

	/// <summary>
	/// Gets a child element, the second table caption
	/// </summary>
	public LazyElement FlowerTableCaptionWithParent
	{
		get { return this.GetLazyElement(this.FlowerTableLazyElement, By.CssSelector("CAPTION > Strong"), "Flower table caption"); }
	}

	/// <summary>
	/// Open the page
	/// </summary>
	public void OpenPage()
	{
		this.WebDriver.Navigate().GoToUrl(Url);
	}

	/// <summary>
	/// Check if the page has been loaded
	/// </summary>
	/// <returns>True if the page was loaded</returns>
	public override bool IsPageLoaded()
	{
		return FlowerTableLazyElement.Exists;
	}
}
```