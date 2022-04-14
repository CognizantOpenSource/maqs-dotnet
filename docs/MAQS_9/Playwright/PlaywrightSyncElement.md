# <img src="resources/maqslogo.ico" height="32" width="32"> Playwright Sync Element

## Overview 
A sync element is a wrapper for Playwright elements that wraps element finds.  These can be very useful when creating page object models. 

## Sync Element Methods

### Initialization 
Initializing Sync Element
#### Written as
```csharp
// Initializing Sync Element
PlaywrightSyncElement elementID =  new PlaywrightSyncElement(this.TestObject, "#ElementSelector"); 
```

### Check
Check a check box
#### Written as
```csharp
this.ElementID.Check(Checkbox1);
Assert.IsTrue(this.ElementID.IsChecked());
```

### Click
Click an element
#### Written as
```csharp
this.ElementID.Click();
Assert.IsTrue(this.PageDriver.IsEnabled(CloseButtonShowDialog));
```

### DblClick
Double click an element
#### Written as
```csharp
this.ElementID.DblClick();
```

### DispatchEvent
Trigger an event on an element
#### Written as
```csharp
this.ElementID.DispatchEvent("click");
Assert.IsTrue(this.PageDriver.IsEventualyVisible(AlwaysUpOnAsyncPage));
```

### DragTo
Click down on an element, drag it to another element and release
* Also supports source and destination offsets
#### Written as
```csharp
this.ElementID.DragTo(Html5Drop);
```

### EvaluateAll
Advanced sub element search and return syntax
#### Written as
```csharp
var results  = this.ElementID.EvaluateAll<object>("nodes => nodes.map(n => n.innerText)") as List<object>;
Assert.AreEqual(6, results.Count);

```

### Evaluate
Execute JavaScript in the page
#### Written as
```csharp
Assert.AreEqual(3, this.ElementID.Evaluate("1 + 2").Value.GetInt32());
```

### Fill
Fills in an editable item with the given text
#### Written as
```csharp
this.ElementID.Fill("Ted");
Assert.AreEqual("Ted",  this.ElementID.InputValue());
```

### Focus
Sets the current focus on a specific element
#### Written as
```csharp
this.ElementID.Focus();
Assert.IsTrue(this.PageDriver.IsVisible(".datepicker-days"));
```

### GetAttribute
Gets a specific attribute from an element
#### Written as
```csharp
Assert.AreEqual("ShowProgressAnimation();",  this.ElementID.GetAttribute("onclick"));
```

### Hover
Hover over an element
#### Written as
```csharp
this.ElementID.Hover();
Assert.IsTrue(this.PageDriver.IsVisible(TrainingOneLink));
```

### InnerHTML
Get an element's inner HTML
#### Written as
```csharp
Assert.IsTrue(this.ElementID.InnerHTML().Contains("Softvision"));
```

### InnerText
Get an element's inner text
#### Written as
```csharp
Assert.IsTrue(this.ElementID.InnerText().Contains("Softvision"));
```

### InputValue
Get an element's input value
#### Written as
```csharp
this.ElementID.Fill("Ted");
Assert.AreEqual("Ted",  this.ElementID.InputValue());
```

### IsChecked
Gets if an element is checked
#### Written as
```csharp
this.ElementID.Uncheck();
Assert.IsFalse(this.ElementID.IsChecked());
```

### IsDisabled
Gets if an element is disabled
#### Written as
```csharp
Assert.IsTrue(this.ElementID.IsDisabled());
```

### IsEditable
Gets if an element is editable
#### Written as
```csharp
Assert.IsTrue(this.ElementID.IsEditable());
```

### IsEnabled
Gets if an element is enabled
#### Written as
```csharp
Assert.IsTrue(this.ElementID.IsEnabled());
```

### IsEventualyGone
Gets if an element goes away or does not exist within the default page timeout
#### Written as
```csharp
this.ElementID.AddStyleTag(new PageAddStyleTagOptions { Content = "html {display: none;}" });
Assert.IsTrue(this.ElementID.IsEventualyGone());
```

### IsEventualyVisible
Gets if an element is visible within the default page timeout
#### Written as
```csharp
this.ElementID.Click(AsyncPageLink);
Assert.IsTrue(this.ElementID.IsEventualyVisible());
```

### IsHidden
Gets if an element is hidden or does not exist
#### Written as
*This check does not wait for a state change
```csharp
Assert.IsFalse(this.ElementID.IsHidden());
```

### IsVisible
Gets if an element is visible
*This check does not wait for a state change
#### Written as
```csharp
Assert.IsTrue(this.ElementID.IsEnabled());
```

### SelectOption
Selects an option or options
* Old selection options are clears whenever "SelectOption" is called
#### Written as
```csharp
var singleOption =  this.ElementID.SelectOption("5");
Assert.AreEqual(1, singleOption.Count);
Assert.AreEqual("5", singleOption[0]);

singleOption =  this.ElementID.SelectOption(new SelectOptionValue() { Label = "Jill" });
Assert.AreEqual(1, singleOption.Count);
Assert.AreEqual("3", singleOption[0]);

var joe =  this.PageDriver.QuerySelector(NamesDropDownFirstOption);

singleOption =  this.ElementID.SelectOption(joe);
Assert.AreEqual(1, singleOption.Count);
Assert.AreEqual("1", singleOption[0]);

var multipleOptions = this.ElementID.SelectOption( new[] { "one", "five" });
Assert.AreEqual(2, multipleOptions.Count);
Assert.AreEqual("one", multipleOptions[0]);
Assert.AreEqual("five", multipleOptions[1]);

var second = this.PageDriver.QuerySelector(ComputerPartsSecond);
var fourth = this.PageDriver.QuerySelector(ComputerPartsFourth);

multipleOptions = this.ElementID.SelectOption(new[] { fourth, second });
Assert.AreEqual(2, multipleOptions.Count);
Assert.AreEqual("two", multipleOptions[0]);
Assert.AreEqual("four", multipleOptions[1]);

multipleOptions = this.ElementID.SelectOption(new[] { new SelectOptionValue { Value = "two" }, new SelectOptionValue { Value = "three" } });
Assert.AreEqual(2, multipleOptions.Count);
Assert.AreEqual("two", multipleOptions[0]);
Assert.AreEqual("three", multipleOptions[1]);
```

### SetChecked
Check an element, such as a check box
#### Written as
```csharp
this.ElementID.SetChecked(false);
Assert.IsFalse(this.ElementID.IsChecked());
this.ElementID.SetChecked(true);
Assert.IsTrue(this.ElementID.IsChecked());
```

### SetInputFiles
Upload a file to an input
#### Written as
```csharp
FilePayload filePayload = new FilePayload
{
   Buffer =  this.ElementID.AsyncPage.ScreenshotAsync().Result,
   Name = "test.png",
   MimeType = "image/png"
};

this.ElementID.SetInputFiles(filePayload);
```

### Tap
Tap on an element
*Touch needs to be enabled before tap will work - By default MAQS does not create pages with tap enabled
#### Written as
```csharp
// Switch to a context that supports touch
var newBrowserContext =  this.ElementID.ParentBrower.NewContextAsync(new BrowserNewContextOptions
{
   HasTouch = true
}).Result;

PageDriverFactory.GetBrowserWithDefaults(PlaywrightBrowser.Chrome);

this.PageDriver = PageDriverFactory.GetNewPageDriverFromBrowserContext(newBrowserContext);
this.PageDriver.Goto(PageModel.Url);

PlaywrightSyncElement elementID =  new PlaywrightSyncElement(this.TestObject, "#ElementSelector"); 

Assert.IsFalse(this.PageDriver.IsVisible(CloseButtonShowDialog));
this.ElementID.Tap();
Assert.IsTrue(this.PageDriver.IsEnabled(CloseButtonShowDialog));
```

### TextContent
Gets the text of an element
#### Written as
```csharp
Assert.AreEqual("Show dialog",  this.ElementID.TextContent());
```

### Type
Sends and input value to an element
#### Written as
```csharp
this.ElementID.Type("Ted");
Assert.AreEqual("Ted",  this.ElementID.InputValue());
```