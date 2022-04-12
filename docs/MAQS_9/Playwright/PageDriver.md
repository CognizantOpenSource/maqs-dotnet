# <img src="resources/maqslogo.ico" height="32" width="32"> PageDriver

## Overview
Wraps the basic Playwrite interactions and makes them synchronous 

## The PageDriver
The PageDriver object is included in the PlaywriteTestObject. The driver wraps Playwright page functionality. 

### BasePlaywrightTest and PageDriver
Using the PageDriver within a BasePlaywrightTest is easy, simply call the driver: 

```csharp
    this.PageDriver.Goto(Url);  
    Assert.IsTrue(driver.IsVisible(MainHeader));
```

### PageDriver without BasePlaywrightTest
To use the PageDriver without the BasePlaywrightTest, simply create the driver object. 

#### Using a page driver created using the default configuration 
```csharp
    PageDriver driver = PageDriverFactory.GetDefaultPageDriver();

    driver.Goto(Url);  
    Assert.IsTrue(driver.IsVisible(MainHeader));   
```

#### Using a enum browser type
```csharp
    PageDriver driver = PageDriverFactory.GetBrowserWithDefaults(PlaywrightBrowser.Chrome);
```

# Available calls
[AsyncPage](##AsyncPage)  
[ParentBrower](##ParentBrower)  
[AddInitScript](##AddInitScript)  
[AddScriptTag](##AddScriptTag)  
[AddStyleTag](##AddStyleTag)  
[BringToFront](##BringToFront)  
[Check](##Check)  
[Click](##Click)  
[Close](##Close)  
[Contains](##Contains)  
[DblClick](##DblClick)  
[DispatchEvent](##DispatchEvent)  
[DragAndDrop](##DragAndDrop)  
[EvalOnSelector & EvalOnSelectorAll](##EvalOnSelector)  
[Evaluate](##Evaluate)  
[Fill](##Fill)  
[Focus](##Focus)  
[GetAttribute](##GetAttribute)  
[Goto & GoBack & GoForward](##Goto)  
[Hover](##Hover)  
[InnerHTML](##InnerHTML)  
[InnerText](##InnerText)  
[InputValue](##InputValue)  
[IsChecked](##IsChecked)  
[IsDisabled](##IsDisabled)  
[IsEditable](##IsEditable)  
[IsEnabled](##IsEnabled)  
[IsEventualyGone](##IsEventualyGone)  
[IsEventualyVisible](##IsEventualyVisible)  
[IsHidden](##IsHidden)  
[IsVisible](##IsVisible)  
[QuerySelector](##QuerySelector)  
[QuerySelectorAll](##QuerySelectorAll)  
[SelectOption](##SelectOption)  
[SetChecked](##SetChecked)  
[SetExtraHTTPHeaders](##SetExtraHTTPHeaders)  
[SetInputFiles](##SetInputFiles)  
[SetViewportSize](##SetViewportSize)  
[Tap](##Tap)  
[TextContent](##TextContent)  
[Title](##Title)  
[Type](##Type)  
[Url](##Url)  
[WaitForLoadState](##WaitForLoadState)  
[WaitForSelector](##WaitForSelector)  
[WaitForTimeout](##WaitForTimeout)  
[WaitForURL](##WaitForURL)  

# Call details

## AsyncPage
Get the wrapped IPage interface
```csharp
    IPage basePageInterface = this.PageDriver.AsyncPage;
```

## ParentBrower
Get the pages parent browser interface
```csharp
    IBrowser baseBrowserInterface = this.PageDriver.ParentBrower;
```

## AddInitScript
Add an intialization script
```csharp
    this.PageDriver.AddInitScript(RenameHeaderFunc);
    this.PageDriver.Reload();
    this.PageDriver.Evaluate("changeMainHeaderName();");
```

## AddScriptTag
Add a script into the DOM
```csharp
    this.PageDriver.AddScriptTag(new PageAddScriptTagOptions() { Content = RenameHeaderFunc });
    this.PageDriver.Evaluate("changeMainHeaderName();");
    Assert.AreEqual("NEWNAME", this.PageDriver.InnerText(MainHeader));
```

## AddStyleTag
Add a style into the DOM
```csharp
    this.PageDriver.AddStyleTag(new PageAddStyleTagOptions { Content = "html {display: none;}" });
    Assert.IsTrue(this.PageDriver.IsEventualyGone(MainHeader));
```

## BringToFront
Bring this tab associate with this page to the front
```csharp
    this.PageDriver.BringToFront();
```

## Check
Check a check box
```csharp
    this.PageDriver.Check(Checkbox1);
    Assert.IsTrue(this.PageDriver.IsChecked(Checkbox1));
```

## Click
Click an element
```csharp
    this.PageDriver.Click(ShowDialog1);
    Assert.IsTrue(this.PageDriver.IsEnabled(CloseButtonShowDialog));
```

## Close
Close the page/tab
```csharp
    // Close/is closed
    this.PageDriver.Close();
    Assert.IsTrue(this.PageDriver.IsClosed);
```

## Contains
Get the HTML for the page
```csharp
    Assert.IsTrue(this.PageDriver.Content().Contains("Softvision"));
```

## DblClick
Double click an element
```csharp
    this.PageDriver.DblClick(DoubleClickElement);
```

## DispatchEvent
Trigger an event on an element
```csharp
    this.PageDriver.DispatchEvent(AsyncPageLink, "click");
    Assert.IsTrue(this.PageDriver.IsEventualyVisible(AlwaysUpOnAsyncPage));
```


## DragAndDrop
Click down on an element, drag it to another element and release
* Also supports source and destination offsets
```csharp
    var startPosition = this.PageDriver.AsyncPage.Locator(Html5Draggable).BoundingBoxAsync().Result;
    this.PageDriver.DragAndDrop(Html5Draggable, Html5Drop);
    var endPosition = this.PageDriver.AsyncPage.Locator(Html5Draggable).BoundingBoxAsync().Result;

    Assert.AreNotEqual(startPosition.X, endPosition.X);
```

## EvalOnSelector & EvalOnSelectorAll
Advanced element search and return syntax
```csharp
    Assert.AreEqual("Monitor", this.PageDriver.EvalOnSelector(ComputerPartsFourth, "node => node.innerText").Value.GetString());

    Assert.AreEqual(6, this.PageDriver.EvalOnSelectorAll(ComputerPartsAllOptions, "nodes => nodes.map(n => n.innerText)").Value.GetArrayLength());
```

## Evaluate
Execute JavaScript in the page
```csharp
    this.PageDriver.AddScriptTag(new PageAddScriptTagOptions() { Content = RenameHeaderFunc });
    this.PageDriver.Evaluate("changeMainHeaderName();");
    Assert.AreEqual("NEWNAME", this.PageDriver.InnerText(MainHeader));
```

## Fill
Fills in an editable item with the given text
```csharp
    this.PageDriver.Fill(FirstNameText, "Ted");
    Assert.AreEqual("Ted", this.PageDriver.InputValue(FirstNameText));
```

## Focus
Sets the current focus on a specific element
```csharp
    this.PageDriver.Focus("#datepicker INPUT");
    Assert.IsTrue(this.PageDriver.IsVisible(".datepicker-days"));
```

## GetAttribute
Gets a specific attribute from an element
```csharp
    Assert.AreEqual("ShowProgressAnimation();", this.PageDriver.GetAttribute(ShowDialog1, "onclick"));
```

## Goto & GoBack & GoForward
Basic page navigation
```csharp
    this.PageDriver.Goto(PageModel.Url);
    this.PageDriver.Click(AsyncPageLink);
    this.PageDriver.WaitForURL("**/async.html");
    Assert.AreEqual($"{ PageModel.Url}async.html", this.PageDriver.Url);

    this.PageDriver.GoBack();
    this.PageDriver.WaitForURL(new Regex("/Automation/$"));
    Assert.AreEqual(PageModel.Url, this.PageDriver.Url);

    this.PageDriver.GoForward();
    this.PageDriver.WaitForURL(x => x.EndsWith("/async.html"));
    Assert.AreEqual($"{ PageModel.Url}async.html", this.PageDriver.Url);
```

## Hover
Hover over an element
```csharp
    this.PageDriver.Hover(TrainingDropdown);
    Assert.IsTrue(this.PageDriver.IsVisible(TrainingOneLink));
```

## InnerHTML
Get an element's inner HTML
```csharp
    Assert.IsTrue(this.PageDriver.InnerHTML(Footer).Contains("Softvision"));
```

## InnerText
Get an element's inner text
```csharp
    Assert.IsTrue(this.PageDriver.InnerText(Footer).Contains("Softvision"));
```

## InputValue
Get an element's input value
```csharp
    this.PageDriver.Fill(FirstNameText, "Ted");
    Assert.AreEqual("Ted", this.PageDriver.InputValue(FirstNameText));
```

## IsChecked
Gets if an element is checked
```csharp
    this.PageDriver.Uncheck(Checkbox2);
    Assert.IsFalse(this.PageDriver.IsChecked(Checkbox2));
```

## IsDisabled
Gets if an element is disabled
```csharp
    Assert.IsTrue(this.PageDriver.IsDisabled(DisabledField));
```

## IsEditable
Gets if an element is editable
```csharp
    Assert.IsTrue(this.PageDriver.IsEditable(FirstNameText));
```

## IsEnabled
Gets if an element is enabled
```csharp
    Assert.IsTrue(this.PageDriver.IsEnabled(FirstNameText));
```

## IsEventualyGone
Gets if an element goes away or does not exist within the default page timeout
```csharp
    this.PageDriver.AddStyleTag(new PageAddStyleTagOptions { Content = "html {display: none;}" });
    Assert.IsTrue(this.PageDriver.IsEventualyGone(MainHeader));
```

## IsEventualyVisible
Gets if an element is visible within the default page timeout
```csharp
    this.PageDriver.Click(AsyncPageLink);
    Assert.IsTrue(this.PageDriver.IsEventualyVisible(AsyncItemSelector));
```

## IsHidden
Gets if an element is hidden or does not exist
*This check does not wait for a state change
```csharp
    Assert.IsFalse(this.PageDriver.IsHidden(DisabledField));
    Assert.IsTrue(this.PageDriver.IsHidden(TrainingOneLink));
    Assert.IsTrue(this.PageDriver.IsHidden("NotReal"));
```

## IsVisible
Gets if an element is visible
*This check does not wait for a state change
```csharp
    Assert.IsFalse(this.PageDriver.IsVisible(CloseButtonShowDialog));
    this.PageDriver.Click(ShowDialog1);
    Assert.IsTrue(this.PageDriver.IsEnabled(CloseButtonShowDialog));
```

## QuerySelector
Finds an element
*Returns async element
```csharp
    var queryResult = this.PageDriver.QuerySelector("#AnElement");
    Assert.IsTrue(queryResult.IsVisibleAsync().Result);
```
## QuerySelectorAll
Finds a collection of elements
*Returns a collection of async elements
```csharp
    var results = this.PageDriver.QuerySelectorAll("DIV");
    Assert.IsTrue(results.Count > 1, "Selector should have found multiple results");
```

## SelectOption
Selects an option or options
* Old selection options are clears whenever "SelectOption" is called
```csharp
    var singleOption = this.PageDriver.SelectOption(NamesDropDown, "5");
    Assert.AreEqual(1, singleOption.Count);
    Assert.AreEqual("5", singleOption[0]);

    singleOption = this.PageDriver.SelectOption(NamesDropDown, new SelectOptionValue() { Label = "Jill" });
    Assert.AreEqual(1, singleOption.Count);
    Assert.AreEqual("3", singleOption[0]);

    var joe = this.PageDriver.QuerySelector(NamesDropDownFirstOption);

    singleOption = this.PageDriver.SelectOption(NamesDropDown, joe);
    Assert.AreEqual(1, singleOption.Count);
    Assert.AreEqual("1", singleOption[0]);

    var multipleOptions = this.PageDriver.SelectOption(ComputerPartsSelection, new[] { "one", "five" });
    Assert.AreEqual(2, multipleOptions.Count);
    Assert.AreEqual("one", multipleOptions[0]);
    Assert.AreEqual("five", multipleOptions[1]);

    var second = this.PageDriver.QuerySelector(ComputerPartsSecond);
    var fourth = this.PageDriver.QuerySelector(ComputerPartsFourth);

    multipleOptions = this.PageDriver.SelectOption(ComputerPartsSelection, new[] { fourth, second });
    Assert.AreEqual(2, multipleOptions.Count);
    Assert.AreEqual("two", multipleOptions[0]);
    Assert.AreEqual("four", multipleOptions[1]);

    multipleOptions = this.PageDriver.SelectOption(ComputerPartsSelection, new[] { new SelectOptionValue { Value = "two" }, new SelectOptionValue { Value = "three" } });
    Assert.AreEqual(2, multipleOptions.Count);
    Assert.AreEqual("two", multipleOptions[0]);
    Assert.AreEqual("three", multipleOptions[1]);
```

## SetChecked
Check an element, such as a check box
```csharp
    this.PageDriver.SetChecked(Checkbox2, false);
    Assert.IsFalse(this.PageDriver.IsChecked(Checkbox2));
    this.PageDriver.SetChecked(Checkbox2, true);
    Assert.IsTrue(this.PageDriver.IsChecked(Checkbox2));
```

## SetExtraHTTPHeaders
Add exta header key value pairs to your web requests
*Can be used for authentication and/or tagging synthetic tests
```csharp
    this.PageDriver.SetExtraHTTPHeaders(new Dictionary<string, string> { { "sample", "value" } });
```

## SetInputFiles
Upload a file to an input
```csharp
    FilePayload filePayload = new FilePayload
    {
        Buffer = this.PageDriver.AsyncPage.ScreenshotAsync().Result,
        Name = "test.png",
        MimeType = "image/png"
    };

    this.PageDriver.SetInputFiles("#photo", filePayload);
```

## SetViewportSize
Set the page size
```csharp
    this.PageDriver.SetViewportSize(600, 300);
    Assert.AreEqual(300, this.PageDriver.AsyncPage.ViewportSize.Height);
    Assert.AreEqual(600, this.PageDriver.AsyncPage.ViewportSize.Width);
```

## Tap
Tap on an element
*Touch needs to be enabled before tap will work - By default MAQS does not create pages with tap enabled
```csharp
    // Switch to a context that supports touch
    var newBrowserContext = this.PageDriver.ParentBrower.NewContextAsync(new BrowserNewContextOptions
    {
        HasTouch = true
    }).Result;

    PageDriverFactory.GetBrowserWithDefaults(PlaywrightBrowser.Chrome);

    this.PageDriver = PageDriverFactory.GetNewPageDriverFromBrowserContext(newBrowserContext);
    this.PageDriver.Goto(PageModel.Url);

    Assert.IsFalse(this.PageDriver.IsVisible(CloseButtonShowDialog));
    this.PageDriver.Tap(ShowDialog1);
    Assert.IsTrue(this.PageDriver.IsEnabled(CloseButtonShowDialog));
```

## TextContent
Gets the text of an element
```csharp
    Assert.AreEqual("Show dialog", this.PageDriver.TextContent(ShowDialog1));
```

## Title
Gets the title of the page
```csharp
    Assert.AreEqual("Automation Site", this.PageDriver.Title());
```

## Type
Sends and input value to an element
```csharp
    this.PageDriver.Type(FirstNameText, "Ted");
    Assert.AreEqual("Ted", this.PageDriver.InputValue(FirstNameText));
```

## Url
Gets the page URL
```csharp
    this.PageDriver.Goto(PageModel.Url);
    Assert.AreEqual(PageModel.Url, this.PageDriver.Url);
```

## WaitForLoadState
Wait for the page to reach the load stage
* The optional parameters allow you to wait for domcontentloaded or networkidle
```csharp
    this.PageDriver.WaitForLoadState();
    Assert.IsTrue(this.PageDriver.IsVisible(Checkbox2));
```

## WaitForSelector
Wait for an element to exists that matches the selector
```csharp
    this.PageDriver.WaitForSelector(Checkbox2);
    Assert.IsTrue(this.PageDriver.IsVisible(Checkbox2));
```

## WaitForTimeout
Wait a set amount of time before continuing
```csharp
    this.PageDriver.Reload();
    this.PageDriver.WaitForTimeout(200);
    Assert.IsFalse(this.PageDriver.IsVisible(AsyncItemSelector));
```

## WaitForURL
Wait for the URL to match the assumption
*Supports fully qualified, pattern, regex, and predicate matches 
```csharp
    this.PageDriver.Goto(PageModel.Url);
    this.PageDriver.WaitForURL(PageModel.Url);

    this.PageDriver.Click(AsyncPageLink);
    this.PageDriver.WaitForURL("**/async.html");
    Assert.AreEqual($"{ PageModel.Url}async.html", this.PageDriver.Url);

    this.PageDriver.GoBack();
    this.PageDriver.WaitForURL(new Regex("/Automation/$"));
    Assert.AreEqual(PageModel.Url, this.PageDriver.Url);

    this.PageDriver.GoForward();
    this.PageDriver.WaitForURL(x => x.EndsWith("/async.html"));
    Assert.AreEqual($"{ PageModel.Url}async.html", this.PageDriver.Url);
```
