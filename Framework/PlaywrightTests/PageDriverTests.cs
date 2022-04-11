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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace PlaywrightTests
{
    /// <summary>
    /// Test page driver
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    [TestCategory(TestCategories.Playwright)]
    public class PageDriverTests : BasePlaywrightTest
    {
        /// <summary>
        /// Main selector
        /// </summary>
        private readonly string MainHeader = "H2";

        /// <summary>
        /// Rename header Javascript funtion 
        /// </summary>
        private readonly string RenameHeaderFunc = @"function changeMainHeaderName() {document.querySelector('H2').innerHTML = 'NEWNAME';}";

        /// <summary>
        /// Should dialog button selector
        /// </summary>
        private readonly string ShowDialog1 = "#showDialog1";

        /// <summary>
        /// Close dialog selector
        /// </summary>
        private readonly string CloseButtonShowDialog = "#CloseButtonShowDialog";

        /// <summary>
        /// Checkbox 1 selector
        /// </summary>
        private readonly string Checkbox1 = "#Checkbox1";

        /// <summary>
        /// Checkbox 2 selector
        /// </summary>
        private readonly string Checkbox2 = "#Checkbox2";

        /// <summary>
        /// First name input selector
        /// </summary>
        private readonly string FirstNameText = "INPUT[name='firstname']";

        /// <summary>
        /// Main selector
        /// </summary>
        private readonly string DisabledField = "#disabledField INPUT";

        /// <summary>
        /// Async link selector
        /// </summary>
        private readonly string AsyncPageLink = "#AsyncPageLink A";

        /// <summary>
        /// Async element that load right away selector
        /// </summary>
        private readonly string AlwaysUpOnAsyncPage = ".roundedcorners";

        /// <summary>
        /// Trainging dropdown selector
        /// </summary>
        private readonly string TrainingDropdown = "#TrainingDropdown";

        /// <summary>
        /// Training link selector
        /// </summary>
        private readonly string TrainingOneLink = "A[href='../Training1/LoginPage.html']";

        /// <summary>
        /// Footer selector
        /// </summary>
        private readonly string Footer = "FOOTER";

        /// <summary>
        /// Name dropdown selector
        /// </summary>
        private readonly string NamesDropDown = "#namesDropdown";

        /// <summary>
        /// Name option 1 selector
        /// </summary>
        private readonly string NamesDropDownFirstOption = "#namesDropdown > OPTION[value='1']";

        /// <summary>
        /// Computer parts select element selector
        /// </summary>
        private readonly string ComputerPartsSelection = "#computerParts";

        /// <summary>
        /// Computer part 2 selector
        /// </summary>
        private readonly string ComputerPartsSecond = "#computerParts option[value='two']";

        /// <summary>
        /// Computer part 4 selector
        /// </summary>
        private readonly string ComputerPartsFourth = "#computerParts option[value='four']";

        /// <summary>
        /// All computer parts options selector
        /// </summary>
        private readonly string ComputerPartsAllOptions = "#computerParts option";

        /// <summary>
        /// HTML 5 draggable image selector
        /// </summary>
        private readonly string Html5Draggable = "#draggablleImageHTML5";

        /// <summary>
        /// HTML 5 drop selector
        /// </summary>
        private readonly string Html5Drop = "#div2";

        /// <summary>
        /// Setup test and make sure we are on the correct test page
        /// </summary>
        [TestInitialize]
        public void CreatePlaywrightPageModel()
        {
            this.PageDriver.Goto(PageModel.Url);       
        }

        /// <summary>
        /// Test creating and disposing of page driver
        /// </summary>
        [TestMethod]
        public void NewPageDriver()
        {
            Assert.IsFalse(this.PageDriver.AsyncPage.IsClosed);
            this.PageDriver.Dispose();
            Assert.IsTrue(this.PageDriver.AsyncPage.IsClosed);
        }

        /// <summary>
        /// Test check works as expected
        /// </summary>
        [TestMethod]
        public void CheckTest()
        {
            Assert.IsFalse(this.PageDriver.IsChecked(Checkbox1));
            this.PageDriver.Check(Checkbox1);
            Assert.IsTrue(this.PageDriver.IsChecked(Checkbox1));
        }

        /// <summary>
        /// Test set check works as expected
        /// </summary>
        [TestMethod]
        public void SetCheckTest()
        {
            Assert.IsFalse(this.PageDriver.IsChecked(Checkbox2));
            this.PageDriver.SetChecked(Checkbox2, false);
            Assert.IsFalse(this.PageDriver.IsChecked(Checkbox2));
            this.PageDriver.SetChecked(Checkbox2, true);
            Assert.IsTrue(this.PageDriver.IsChecked(Checkbox2));
        }

        /// <summary>
        /// Test click works as expected
        /// </summary>
        [TestMethod]
        public void ClickTest()
        {
            Assert.IsFalse(this.PageDriver.IsVisible(CloseButtonShowDialog));
            this.PageDriver.Click(ShowDialog1);
            Assert.IsTrue(this.PageDriver.IsEnabled(CloseButtonShowDialog));
        }

        /// <summary>
        /// Test select option works as expected
        /// </summary>
        [TestMethod]
        public void SelectOptionTest()
        {
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
        }

        /// <summary>
        /// Test select single option works as expected
        /// </summary>
        [TestMethod]
        public void SelectMultipleOptionTest()
        {
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
        }

        /// <summary>
        /// Test close works as expected
        /// </summary>
        [TestMethod]
        public void CloseTest()
        {
            Assert.IsFalse(this.PageDriver.IsClosed);
            this.PageDriver.Close();
            Assert.IsTrue(this.PageDriver.IsClosed);
        }

        /// <summary>
        /// Test content works as expected
        /// </summary>
        [TestMethod]
        public void ContentTest()
        {
            Assert.IsTrue(this.PageDriver.Content().Contains("Softvision"));
        }

        /// <summary>
        /// Test double click works as expected
        /// </summary>
        [TestMethod]
        public void DblClickTest()
        {
            this.PageDriver.DblClick(NamesDropDown);
            Assert.IsFalse(this.PageDriver.IsVisible(NamesDropDownFirstOption));
        }

        /// <summary>
        /// Test drag and drop works as expected
        /// </summary>
        [TestMethod]
        public void DragAndDropTest()
        {
            var startPosition = this.PageDriver.AsyncPage.Locator(Html5Draggable).BoundingBoxAsync().Result;
            this.PageDriver.DragAndDrop(Html5Draggable, Html5Drop);
            var endPosition = this.PageDriver.AsyncPage.Locator(Html5Draggable).BoundingBoxAsync().Result;

            Assert.AreNotEqual(startPosition.X, endPosition.X);
        }

        /// <summary>
        /// Test fill works as expected
        /// </summary>
        [TestMethod]
        public void FillTest()
        {
            this.PageDriver.Fill(FirstNameText, "Ted");
            Assert.AreEqual("Ted", this.PageDriver.InputValue(FirstNameText));
        }

        /// <summary>
        /// Test get attribute works as expected
        /// </summary>
        [TestMethod]
        public void GetAttributeTest()
        {
            Assert.AreEqual("ShowProgressAnimation();", this.PageDriver.GetAttribute(ShowDialog1, "onclick"));
        }

        /// <summary>
        /// Test that the press action works
        /// </summary>
        [TestMethod]
        public void PressTest()
        {
            Assert.IsFalse(this.PageDriver.IsVisible(CloseButtonShowDialog));
            this.PageDriver.Press(ShowDialog1, "Enter");
            Assert.IsTrue(this.PageDriver.IsEnabled(CloseButtonShowDialog));
        }

        /// <summary>
        /// Test query selector works as expected
        /// </summary>
        [TestMethod]
        public void QuerySelectorTest()
        {
            var queryResult = this.PageDriver.QuerySelector(ShowDialog1);
            Assert.IsTrue(queryResult.IsVisibleAsync().Result);
        }

        /// <summary>
        /// Test query select all works as expected
        /// </summary>
        [TestMethod]
        public void QuerySelectorAllTest()
        {
            var results = this.PageDriver.QuerySelectorAll("DIV");
            Assert.IsTrue(results.Count > 1, "Selector should have found multiple results");
        }

        /// <summary>
        /// Test hover works as expected
        /// </summary>
        [TestMethod]
        public void HoverTest()
        {
            this.PageDriver.Hover(TrainingDropdown);
            Assert.IsTrue(this.PageDriver.IsVisible(TrainingOneLink));
        }

        /// <summary>
        /// Test inner HTML works as expected
        /// </summary>
        [TestMethod]
        public void InnerHTMLTest()
        {
            Assert.IsTrue(this.PageDriver.InnerHTML(Footer).Contains("Softvision"));
        }

        /// <summary>
        /// Test inner text works as expected
        /// </summary>
        [TestMethod]
        public void InnerTextTest()
        {
            Assert.IsTrue(this.PageDriver.InnerText(Footer).Contains("Softvision"));
        }

        /// <summary>
        /// Test is disabled works as expected
        /// </summary>
        [TestMethod]
        public void IsDisabledTest()
        {
            Assert.IsTrue(this.PageDriver.IsDisabled(DisabledField));
            Assert.IsFalse(this.PageDriver.IsDisabled(FirstNameText));
        }

        /// <summary>
        /// Test is editable works as expected
        /// </summary>
        [TestMethod]
        public void IsEditableTest()
        {
            Assert.IsFalse(this.PageDriver.IsEditable(DisabledField));
            Assert.IsTrue(this.PageDriver.IsEditable(FirstNameText));
        }

        /// <summary>
        /// Test is enabled works as expected
        /// </summary>
        [TestMethod]
        public void IsEnabledTest()
        {
            Assert.IsFalse(this.PageDriver.IsEnabled(DisabledField));
            Assert.IsTrue(this.PageDriver.IsEnabled(FirstNameText));
        }

        /// <summary>
        /// Test eventually gone works as expected
        /// </summary>
        [TestMethod]
        public void IsEventualyGoneTest()
        {
            Assert.IsTrue(this.PageDriver.IsEventualyGone("NotReal"));
            Assert.IsFalse(this.PageDriver.IsEventualyGone(FirstNameText));
        }

        /// <summary>
        /// Test eventurally visible works as expected
        /// </summary>
        [TestMethod]
        public void IsEventualyVisibleTest()
        {
            Assert.IsTrue(this.PageDriver.IsEventualyVisible(FirstNameText));
            Assert.IsFalse(this.PageDriver.IsEventualyVisible("NotReal"));
        }

        /// <summary>
        /// Test is hidden works as expected
        /// </summary>
        [TestMethod]
        public void IsHiddenTest()
        {
            Assert.IsFalse(this.PageDriver.IsHidden(DisabledField));
            Assert.IsTrue(this.PageDriver.IsHidden(TrainingOneLink));
            Assert.IsTrue(this.PageDriver.IsHidden("NotReal"));
        }

        /// <summary>
        /// Test is visible works as expected
        /// </summary>
        [TestMethod]
        public void IsVisibleTest()
        {
            Assert.IsTrue(this.PageDriver.IsVisible(FirstNameText));
            Assert.IsFalse(this.PageDriver.IsVisible("NotReal"));
        }

        /// <summary>
        /// Test set size works as expected
        /// </summary>
        [TestMethod]
        public void SetViewportSizeTest()
        {
            this.PageDriver.SetViewportSize(600, 300);
            Assert.AreEqual(300, this.PageDriver.AsyncPage.ViewportSize.Height);
            Assert.AreEqual(600, this.PageDriver.AsyncPage.ViewportSize.Width);
        }

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

            Assert.IsFalse(this.PageDriver.IsVisible(CloseButtonShowDialog));
            this.PageDriver.Tap(ShowDialog1);
            Assert.IsTrue(this.PageDriver.IsEnabled(CloseButtonShowDialog));
        }

        /// <summary>
        /// Test contenct works as expected
        /// </summary>
        [TestMethod]
        public void TextContentTest()
        {
            Assert.AreEqual("Show dialog", this.PageDriver.TextContent(ShowDialog1));
        }

        /// <summary>
        /// Test title works as expected
        /// </summary>
        [TestMethod]
        public void TitleTest()
        {
            Assert.AreEqual("Automation - Magenic Automation Test Site", this.PageDriver.Title());
        }

        /// <summary>
        /// Test type and input value work as expected
        /// </summary>
        [TestMethod]
        public void TypeAndInputalueTest()
        {
            this.PageDriver.Type(FirstNameText, "Ted");
            Assert.AreEqual("Ted", this.PageDriver.InputValue(FirstNameText));
        }

        /// <summary>
        /// Test uncheck works as expected
        /// </summary>
        [TestMethod]
        public void UncheckTest()
        {
            this.PageDriver.Uncheck(Checkbox2);
            Assert.IsFalse(this.PageDriver.IsChecked(Checkbox2));
        }

        /// <summary>
        /// Test wait for load state works as expected
        /// </summary>
        [TestMethod]
        public void WaitForLoadStateTest()
        {
            this.PageDriver.WaitForLoadState();
            Assert.IsTrue(this.PageDriver.IsVisible(Checkbox2));
        }

        /// <summary>
        /// Test wait for selector works as expected
        /// </summary>
        [TestMethod]
        public void WaitForSelectorTest()
        {
            this.PageDriver.WaitForSelector(Checkbox2);
            Assert.IsTrue(this.PageDriver.IsVisible(Checkbox2));
        }

        /// <summary>
        /// Test wait for timeout works as expected
        /// </summary>
        [TestMethod]
        public void WaitForTimeoutTest()
        {
            var before = DateTime.Now;
            this.PageDriver.WaitForTimeout(1000);
            var afterWait = DateTime.Now;

            Assert.IsTrue(afterWait > before.AddMilliseconds(800) && afterWait < before.AddMilliseconds(1200), $"Sleep should have been about 1 second but was {(before - afterWait).TotalSeconds} seconds");
        }

        /// <summary>
        /// Test wait for url works as expected
        /// </summary>
        [TestMethod]
        public void WaitForUrlAndNavigationTest()
        {
            this.PageDriver.Click(AsyncPageLink);
            this.PageDriver.WaitForURL("**/async.html");
            Assert.AreEqual($"{ PageModel.Url}async.html", this.PageDriver.Url);

            this.PageDriver.GoBack();
            this.PageDriver.WaitForURL(new Regex("/Automation/$"));
            Assert.AreEqual(PageModel.Url, this.PageDriver.Url);

            this.PageDriver.GoForward();
            this.PageDriver.WaitForURL(x => x.EndsWith("/async.html"));
            Assert.AreEqual($"{ PageModel.Url}async.html", this.PageDriver.Url);
        }

        /// <summary>
        /// Test reload works as expected
        /// </summary>
        [TestMethod]
        public void ReloadTest()
        {
            string asyncItemSelector = "#Label";

            this.PageDriver.Click(AsyncPageLink);
            Assert.IsTrue(this.PageDriver.IsEventualyVisible(asyncItemSelector));
            this.PageDriver.Reload();
            this.PageDriver.WaitForTimeout(200);
            Assert.IsFalse(this.PageDriver.IsVisible(asyncItemSelector));
        }

        /// <summary>
        /// Test eval on select works as expected
        /// </summary>
        [TestMethod]
        public void EvalOnSelectorTest()
        {
            Assert.AreEqual("Monitor", this.PageDriver.EvalOnSelector(ComputerPartsFourth, "node => node.innerText").Value.GetString());
        }

        /// <summary>
        /// Test eval on selector all works as expected
        /// </summary>
        [TestMethod]
        public void EvalOnSelectorAllTest()
        {
            Assert.AreEqual(6, this.PageDriver.EvalOnSelectorAll(ComputerPartsAllOptions, "nodes => nodes.map(n => n.innerText)").Value.GetArrayLength());
        }

        /// <summary>
        /// Test eval works as expected
        /// </summary>
        [TestMethod]
        public void EvaluateTest()
        {
            Assert.AreEqual(3, this.PageDriver.Evaluate("1 + 2").Value.GetInt32());
        }

        /// <summary>
        /// Test dispatch works as expected
        /// </summary>
        [TestMethod]
        public void DispatchEventTest()
        {
            this.PageDriver.DispatchEvent(AsyncPageLink, "click");
            Assert.IsTrue(this.PageDriver.IsEventualyVisible(AlwaysUpOnAsyncPage));
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

            this.PageDriver.SetInputFiles("#photo", filePayload);
            Assert.IsNotNull(filePayload);
        }

        /// <summary>
        /// Test focus works as expected
        /// </summary>
        [TestMethod]
        public void FocusTest()
        {
            Assert.IsFalse(this.PageDriver.IsVisible(".datepicker-days"));
            this.PageDriver.Focus("#datepicker INPUT");
            Assert.IsTrue(this.PageDriver.IsVisible(".datepicker-days"));
        }

        /// <summary>
        /// Test bring to front works as expected
        /// </summary>
        [TestMethod]
        public void BringToFrontTest()
        {
            // Switch to a context that supports touch
            var newBrowserContext = PageDriverFactory.GetNewPageDriverFromBrowserContext(this.PageDriver.ParentBrower.Contexts[0]);
            this.PageDriver.BringToFront();

            Assert.IsFalse(newBrowserContext.AsyncPage.IsClosed);
        }

        /// <summary>
        /// Test add script works as expected
        /// </summary>
        [TestMethod]
        public void AddInitScriptTest()
        {
            this.PageDriver.AddInitScript(RenameHeaderFunc);
            this.PageDriver.Reload();
            this.PageDriver.Evaluate("changeMainHeaderName();");

            Assert.AreEqual("NEWNAME", this.PageDriver.InnerText(MainHeader));
        }

        /// <summary>
        /// Test add script tag works as expected
        /// </summary>
        [TestMethod]
        public void AddScriptTagTest()
        {
            this.PageDriver.AddScriptTag(new PageAddScriptTagOptions() { Content = RenameHeaderFunc });
            this.PageDriver.Evaluate("changeMainHeaderName();");
            Assert.AreEqual("NEWNAME", this.PageDriver.InnerText(MainHeader));
        }

        /// <summary>
        /// Test add style works as expected
        /// </summary>
        [TestMethod]
        public void AddStyleTagTest()
        {
            Assert.IsTrue(this.PageDriver.IsEventualyVisible(MainHeader));
            this.PageDriver.AddStyleTag(new PageAddStyleTagOptions { Content = "html {display: none;}" });
            Assert.IsTrue(this.PageDriver.IsEventualyGone(MainHeader));
        }

        /// <summary>
        /// Test set extra headers work as expected
        /// </summary>
        [TestMethod]
        public void SetExtraHTTPHeadersTest()
        {
            this.PageDriver.SetExtraHTTPHeaders(new Dictionary<string, string> { { "sample", "value" } });
            this.PageDriver.AsyncPage.RequestFinished += AsyncPage_RequestFinished;

            this.PageDriver.Click(AsyncPageLink);
            Assert.IsTrue(this.PageDriver.IsEventualyVisible(AlwaysUpOnAsyncPage));
        }


        /// <summary>
        /// Verify the request header has the etra key value pair
        /// </summary>
        /// <param name="_">Ignored object</param>
        /// <param name="request">The request</param>
        private void AsyncPage_RequestFinished(object _, IRequest request)
        {
            this.PageDriver.AsyncPage.RequestFinished -= AsyncPage_RequestFinished;
#pragma warning disable CS0612 // Type or member is obsolete
            Assert.AreEqual("value", request.Headers["sample"]);
#pragma warning restore CS0612 // Type or member is obsolete
        }

    }
}
