//-----------------------------------------------------
// <copyright file="PageDriverTests.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Test the page driver</summary>
//-----------------------------------------------------
using CognizantSoftvision.Maqs.BasePlaywrightTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace PlaywrightTests
{
    /// <summary>
    /// Test page driver
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    [TestCategory(TestCategories.Playwright)]
    public class PlaywrightSyncElementTests : BasePlaywrightTest
    {
        private static readonly ConcurrentDictionary<IPlaywrightTestObject, PageModel> Models = new ConcurrentDictionary<IPlaywrightTestObject, PageModel>();

        /// <summary>
        /// Setup test and make sure we are on the correct test page
        /// </summary>
        [TestInitialize]
        public void CreatePlaywrightPageModel()
        {
            var pageModel = new PageModel(this.TestObject);
            pageModel.OpenPage();
            Models.TryAdd(this.TestObject, pageModel); 
        }

        /// <summary>
        /// Setup test and make sure we are on the correct test page
        /// </summary>
        [TestCleanup]
        public void CleanupPlaywrightPageModel()
        {
            Models.TryRemove(this.TestObject, out _);
        }

        /// <summary>
        /// Test check works as expected
        /// </summary>
        [TestMethod]
        public void CheckTest()
        {
            Assert.IsFalse(Models[this.TestObject].Checkbox1.IsChecked());
            Models[this.TestObject].Checkbox1.Check();
            Assert.IsTrue(Models[this.TestObject].Checkbox1.IsChecked());
        }

        /// <summary>
        /// Test set check works as expected
        /// </summary>
        [TestMethod]
        public void SetCheckTest()
        {
            Assert.IsFalse(Models[this.TestObject].Checkbox2.IsChecked());
            Models[this.TestObject].Checkbox2.SetChecked(false);
            Assert.IsFalse(Models[this.TestObject].Checkbox2.IsChecked());
            Models[this.TestObject].Checkbox2.SetChecked(true);
            Assert.IsTrue(Models[this.TestObject].Checkbox2.IsChecked());
        }

        /// <summary>
        /// Test click works as expected
        /// </summary>
        [TestMethod]
        public void ClickTest()
        {
            Assert.IsFalse(Models[this.TestObject].CloseButtonShowDialog.IsVisible());
            Models[this.TestObject].ShowDialog1.Click();
            Assert.IsTrue(Models[this.TestObject].CloseButtonShowDialog.IsEnabled());
        }

        /// <summary>
        /// Test select option works as expected
        /// </summary>
        [TestMethod]
        public void SelectOptionTest()
        {
            var singleOption = Models[this.TestObject].NamesDropDown.SelectOption("5");
            Assert.AreEqual(1, singleOption.Count);
            Assert.AreEqual("5", singleOption[0]);

            singleOption = Models[this.TestObject].NamesDropDown.SelectOption(new SelectOptionValue() { Label = "Jill" });
            Assert.AreEqual(1, singleOption.Count);
            Assert.AreEqual("3", singleOption[0]);

            var joe = this.PageDriver.QuerySelector(Models[this.TestObject].NamesDropDownFirstOption.Selector);

            singleOption = Models[this.TestObject].NamesDropDown.SelectOption(joe);
            Assert.AreEqual(1, singleOption.Count);
            Assert.AreEqual("1", singleOption[0]);
        }

        /// <summary>
        /// Test select single option works as expected
        /// </summary>
        [TestMethod]
        public void SelectMultipleOptionTest()
        {
            var multipleOptions = Models[this.TestObject].ComputerPartsSelection.SelectOption(new[] { "one", "five" });
            Assert.AreEqual(2, multipleOptions.Count);
            Assert.AreEqual("one", multipleOptions[0]);
            Assert.AreEqual("five", multipleOptions[1]);

            var second = Models[this.TestObject].ComputerPartsSecond.ElementLocator().ElementHandleAsync().Result;
            var fourth = Models[this.TestObject].ComputerPartsFourth.ElementLocator().ElementHandleAsync().Result;

            multipleOptions = Models[this.TestObject].ComputerPartsSelection.SelectOption( (new[] { fourth, second }));
            Assert.AreEqual(2, multipleOptions.Count);
            Assert.AreEqual("two", multipleOptions[0]);
            Assert.AreEqual("four", multipleOptions[1]);
        }

        /// <summary>
        /// Test double click works as expected
        /// </summary>
        [TestMethod]
        public void DblClickTest()
        {
            Models[this.TestObject].NamesDropDown.DblClick();
            Assert.IsFalse(Models[this.TestObject].NamesDropDownFirstOption.IsVisible());
        }

        /// <summary>
        /// Test drag and drop works as expected
        /// </summary>
        [TestMethod]
        public void DragAndDropTest()
        {
            var startPosition = Models[this.TestObject].Html5Draggable.ElementLocator().BoundingBoxAsync().Result;
            Models[this.TestObject].Html5Draggable.DragTo(Models[this.TestObject].Html5Drop.ElementLocator());
            var endPosition = Models[this.TestObject].Html5Draggable.ElementLocator().BoundingBoxAsync().Result;

            Assert.AreNotEqual(startPosition.X, endPosition.X);
        }

        /// <summary>
        /// Test page locator options work as expected
        /// </summary>
        [TestMethod]
        public void PageLocatorOptionsTest()
        {
            PageLocatorOptions locator = new PageLocatorOptions
            {
                HasTextString = "Elements",
                
            };

            var element = new PlaywrightSyncElement(this.TestObject, Models[this.TestObject].MainHeader.Selector, locator);
            Assert.IsTrue(element.IsVisible());
        }

        /// <summary>
        /// Test fill works as expected
        /// </summary>
        [TestMethod]
        public void FillTest()
        {
            Models[this.TestObject].FirstNameText.Fill("Ted");
            Assert.AreEqual("Ted", Models[this.TestObject].FirstNameText.InputValue());
        }

        /// <summary>
        /// Test get attribute works as expected
        /// </summary>
        [TestMethod]
        public void GetAttributeTest()
        {
            Assert.AreEqual("ShowProgressAnimation();", Models[this.TestObject].ShowDialog1.GetAttribute("onclick"));
        }

        /// <summary>
        /// Test that the press action works
        /// </summary>
        [TestMethod]
        public void PressTest()
        {
            Assert.IsFalse(Models[this.TestObject].CloseButtonShowDialog.IsVisible());
            Models[this.TestObject].ShowDialog1.Press("Enter");
            Assert.IsTrue(Models[this.TestObject].CloseButtonShowDialog.IsEnabled());
        }

        /// <summary>
        /// Test hover works as expected
        /// </summary>
        [TestMethod]
        public void HoverTest()
        {
            Models[this.TestObject].TrainingDropdown.Hover();
            Assert.IsTrue(Models[this.TestObject].TrainingOneLink.IsVisible());
        }

        /// <summary>
        /// Test inner HTML works as expected
        /// </summary>
        [TestMethod]
        public void InnerHTMLTest()
        {
            Assert.IsTrue(Models[this.TestObject].Footer.InnerHTML().Contains("Softvision"));
        }

        /// <summary>
        /// Test inner text works as expected
        /// </summary>
        [TestMethod]
        public void InnerTextTest()
        {
            Assert.IsTrue(Models[this.TestObject].Footer.InnerText().Contains("Softvision"));
        }

        /// <summary>
        /// Test is disabled works as expected
        /// </summary>
        [TestMethod]
        public void IsDisabledTest()
        {
            Assert.IsTrue(Models[this.TestObject].DisabledField.IsDisabled());
            Assert.IsFalse(Models[this.TestObject].FirstNameText.IsDisabled());
        }

        /// <summary>
        /// Test is editable works as expected
        /// </summary>
        [TestMethod]
        public void IsEditableTest()
        {
            Assert.IsFalse(Models[this.TestObject].DisabledField.IsEditable());
            Assert.IsTrue(Models[this.TestObject].FirstNameText.IsEditable());
        }

        /// <summary>
        /// Test is enabled works as expected
        /// </summary>
        [TestMethod]
        public void IsEnabledTest()
        {
            Assert.IsFalse(Models[this.TestObject].DisabledField.IsEnabled());
            Assert.IsTrue(Models[this.TestObject].FirstNameText.IsEnabled());
        }

        /// <summary>
        /// Test eventually gone works as expected
        /// </summary>
        [TestMethod]
        public void IsEventualyGoneTest()
        {
            Assert.IsTrue(Models[this.TestObject].NotReal.IsEventualyGone());
            Assert.IsFalse(Models[this.TestObject].FirstNameText.IsEventualyGone());
        }

        /// <summary>
        /// Test eventurally visible works as expected
        /// </summary>
        [TestMethod]
        public void IsEventualyVisibleTest()
        {
            Assert.IsTrue(Models[this.TestObject].FirstNameText.IsEventualyVisible());
            Assert.IsFalse(Models[this.TestObject].NotReal.IsEventualyVisible());
        }

        /// <summary>
        /// Test is hidden works as expected
        /// </summary>
        [TestMethod]
        public void IsHiddenTest()
        {
            Assert.IsFalse(Models[this.TestObject].DisabledField.IsHidden());
            Assert.IsTrue(Models[this.TestObject].TrainingOneLink.IsHidden());
            Assert.IsTrue(Models[this.TestObject].NotReal.IsHidden());
        }

        /// <summary>
        /// Test is visible works as expected
        /// </summary>
        [TestMethod]
        public void IsVisibleTest()
        {
            Assert.IsTrue(Models[this.TestObject].FirstNameText.IsVisible());
            Assert.IsFalse(Models[this.TestObject].NotReal.IsVisible());
        }


        ////// TODO this may not work
        /// <summary>
        /// Test that the tap action works
        /// </summary>
        [TestMethod]
        public void TapTest()
        {
            // Switch to a context that supports touch
            var newBrowserContext = this.PageDriver.ParentBrower.NewContextAsync(new BrowserNewContextOptions
            {
                HasTouch = true
            }).Result;

            this.PageDriver = PageDriverFactory.GetNewPageDriverFromBrowserContext(newBrowserContext);
            this.PageDriver.Goto(PageModel.Url);
            Models[this.TestObject].OverridePageDriver(this.PageDriver);

            Assert.IsFalse(Models[this.TestObject].CloseButtonShowDialog.IsVisible());
            Models[this.TestObject].ShowDialog1.Tap();
            Assert.IsTrue(Models[this.TestObject].CloseButtonShowDialog.IsEnabled());
        }

        /// <summary>
        /// Test contenct works as expected
        /// </summary>
        [TestMethod]
        public void TextContentTest()
        {
            Assert.AreEqual("Show dialog", Models[this.TestObject].ShowDialog1.TextContent());
        }

        /// <summary>
        /// Test type and input value work as expected
        /// </summary>
        [TestMethod]
        public void TypeAndInputalueTest()
        {
            Models[this.TestObject].FirstNameText.Type("Ted");
            Assert.AreEqual("Ted", Models[this.TestObject].FirstNameText.InputValue());
        }

        /// <summary>
        /// Test uncheck works as expected
        /// </summary>
        [TestMethod]
        public void UncheckTest()
        {
            Models[this.TestObject].Checkbox2.Uncheck();
            Assert.IsFalse(Models[this.TestObject].Checkbox2.IsChecked());
        }


        /// <summary>
        /// Test eval on selector all works as expected
        /// </summary>
        [TestMethod]
        public void EvalOnSelectorAllTest()
        {
            var results  = Models[this.TestObject].ComputerPartsAllOptions.EvalOnSelectorAll<object>("nodes => nodes.map(n => n.innerText)") as List<object>;
            Assert.AreEqual(6, results.Count);
        }

        /// <summary>
        /// Test eval works as expected
        /// </summary>
        [TestMethod]
        public void EvaluateTest()
        {
            Assert.AreEqual(3, Models[this.TestObject].ShowDialog1.Evaluate("1 + 2").Value.GetInt32());
        }

        /// <summary>
        /// Test dispatch works as expected
        /// </summary>
        [TestMethod]
        public void DispatchEventTest()
        {
            Models[this.TestObject].AsyncPageLink.DispatchEvent("click");
            Assert.IsTrue(Models[this.TestObject].AlwaysUpOnAsyncPage.IsEventualyVisible());
        }

        /// <summary>
        /// Test input file works as expected
        /// </summary>
        [TestMethod]
        public void SetInputFilesTest()
        {
            FilePayload filePayload = new FilePayload
            {
                Buffer = this.PageDriver.AsyncPage.ScreenshotAsync().Result,
                Name = "test.png",
                MimeType = "image/png"
            };

            Models[this.TestObject].Upload.SetInputFiles(filePayload);
            Assert.IsNotNull(filePayload);
        }

        /// <summary>
        /// Test focus works as expected
        /// </summary>
        [TestMethod]
        public void FocusTest()
        {
            Assert.IsFalse(Models[this.TestObject].DatePickerDays.IsVisible());
            Models[this.TestObject].DatePickerInput.Focus();
            Assert.IsTrue(Models[this.TestObject].DatePickerDays.IsVisible());
        }

        /// <summary>
        /// Inline frame test
        /// </summary>
        [TestMethod]
        public void InlineFrameTest()
        {
            PageModelIFrame frameModel = new PageModelIFrame(this.TestObject);
            frameModel.OpenPage();

            Assert.IsFalse(frameModel.CloseDialog.IsVisible());
            frameModel.ShowDialog.Click();
            Assert.IsTrue(frameModel.CloseDialog.IsVisible());
        }
    }
}
