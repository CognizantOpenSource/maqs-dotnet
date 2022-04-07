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
using System.Diagnostics.CodeAnalysis;

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
        private readonly string ShowDialog1 = "#showDialog1";
        private readonly string CloseButtonShowDialog = "#CloseButtonShowDialog";
        private readonly string Checkbox1 = "#Checkbox1";
        private readonly string Checkbox2 = "#Checkbox1";
        private readonly string FirstNameText = "INPUT[name='firstname']";
        private readonly string DisabledField = "#disabledField INPUT";
        private readonly string AsyncPageLink = "#AsyncPageLink A";
        private readonly string TrainingDropdown = "#TrainingDropdown";
        private readonly string TrainingOneLink = "A[href='../Training1/LoginPage.html']";

        private readonly string Footer = "FOOTER";
        private readonly string NamesDropDown = "#namesDropdown";
        private readonly string NamesDropDownFirstOption = "#namesDropdown > OPTION[value='1']";

        private readonly string Html5Draggable = "#draggablleImageHTML5";
        private readonly string Html5Drop = "#div2";


        /// <summary>
        /// Setup test Playwright page model
        /// </summary>
        [TestInitialize]
        public void CreatePlaywrightPageModel()
        {
            this.PageDriver.Goto(PageModel.Url);
        }

        ////////////[TestMethod] public void AddInitScriptTest() { this.PageDriver.AddInitScript(); }
        ////////////[TestMethod] public void AddScriptTagTest() { this.PageDriver.AddScriptTag(); }
        ////////////[TestMethod] public void AddStyleTagTest() { this.PageDriver.AddStyleTag(); }
        ////////////[TestMethod] public void BringToFrontTest() { this.PageDriver.BringToFront(); }
        //    [TestMethod] public void SetExtraHTTPHeadersTest() { this.PageDriver.SetExtraHTTPHeaders(); }
        //    [TestMethod] public void SetInputFilesTest() { this.PageDriver.SetInputFiles(); }
        //    [TestMethod] public void DispatchEventTest() { this.PageDriver.DispatchEvent(); }
        //    [TestMethod] public void EvalOnSelectorTest() { this.PageDriver.EvalOnSelector(); }
        //    [TestMethod] public void EvalOnSelectorAllTest() { this.PageDriver.EvalOnSelectorAll(); }
        //    [TestMethod] public void EvaluateTest() { this.PageDriver.Evaluate(); }

        //    [TestMethod] public void FocusTest() { this.PageDriver.Focus(); }


        [TestMethod]
        public void CheckTest()
        {
            Assert.IsFalse(this.PageDriver.IsChecked(Checkbox1));
            this.PageDriver.Check(Checkbox1);
            Assert.IsTrue(this.PageDriver.IsChecked(Checkbox1));
        }

        [TestMethod]
        public void ClickTest()
        {
            Assert.IsFalse(this.PageDriver.IsVisible(CloseButtonShowDialog));
            this.PageDriver.Click(ShowDialog1);
            Assert.IsTrue(this.PageDriver.IsEnabled(CloseButtonShowDialog));
        }

        [TestMethod]
        public void CloseTest()
        {
            Assert.IsFalse(this.PageDriver.AsyncPage.IsClosed);
            this.PageDriver.Close();
            Assert.IsTrue(this.PageDriver.AsyncPage.IsClosed);
        }


        [TestMethod]
        public void ContentTest()
        {
            Assert.IsTrue(this.PageDriver.Content().Contains("Softvision"));
        }
        [TestMethod]
        public void DblClickTest()
        {
            this.PageDriver.DblClick(NamesDropDown);
            Assert.IsFalse(this.PageDriver.IsVisible(NamesDropDownFirstOption));
        }


        [TestMethod]
        public void DragAndDropTest()
        {
            this.PageDriver.DragAndDrop(Html5Draggable, Html5Drop);
        }
        [TestMethod] public void FillTest() {
            this.PageDriver.Fill(FirstNameText, "Ted");
            Assert.AreEqual("Ted", this.PageDriver.InputValue(FirstNameText));
        }
        [TestMethod] public void GetAttributeTest() {
            Assert.AreEqual("ShowProgressAnimation();", this.PageDriver.GetAttribute(ShowDialog1, "onclick"));

        
        }
        //    [TestMethod] public void GoBackTest() { this.PageDriver.GoBack(); }
        //    [TestMethod] public void GoForwardTest() { this.PageDriver.GoForward(); }
        //    [TestMethod] public void ReloadTest() { this.PageDriver.Reload(); }

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



        [TestMethod] public void QuerySelectorTest() { 
            var queryResult = this.PageDriver.QuerySelector(ShowDialog1);
            Assert.IsTrue(queryResult.IsVisibleAsync().Result);
        }
        [TestMethod] public void QuerySelectorAllTest() { 
            var results = this.PageDriver.QuerySelectorAll("DIV");
            Assert.IsTrue(results.Count > 1, "Selector should have found multiple results");
        }
        
        //    [TestMethod] public void SelectOptionTest() { this.PageDriver.SelectOption(); }
        //    [TestMethod] public void SetCheckedTest() { this.PageDriver.SetChecked(); }
        //    [TestMethod] public void SetContentTest() { this.PageDriver.SetContent(); }

        [TestMethod]
        public void HoverTest()
        {
            this.PageDriver.Hover(TrainingDropdown);
            Assert.IsTrue(this.PageDriver.IsVisible(TrainingOneLink));
        }


        [TestMethod]
        public void InnerHTMLTest()
        {

            Assert.IsTrue(this.PageDriver.InnerHTML(Footer).Contains("Softvision"));
        }
        [TestMethod]
        public void InnerTextTest()
        {
            Assert.IsTrue(this.PageDriver.InnerText(Footer).Contains("Softvision"));
        }
        [TestMethod]
        public void IsDisabledTest()
        {

            Assert.IsTrue(this.PageDriver.IsDisabled(DisabledField));
            Assert.IsFalse(this.PageDriver.IsDisabled(FirstNameText));
        }

        [TestMethod]
        public void IsEditableTest()
        {
            Assert.IsFalse(this.PageDriver.IsEditable(DisabledField));
            Assert.IsTrue(this.PageDriver.IsEditable(FirstNameText));
        }

        [TestMethod]
        public void IsEnabledTest()
        {
            Assert.IsFalse(this.PageDriver.IsEnabled(DisabledField));
            Assert.IsTrue(this.PageDriver.IsEnabled(FirstNameText));
        }
        [TestMethod]
        public void IsEventualyGoneTest()
        {
            Assert.IsTrue(this.PageDriver.IsEventualyGone("NotReal"));
            Assert.IsFalse(this.PageDriver.IsEventualyGone(FirstNameText));
        }

        [TestMethod]
        public void IsEventualyVisibleTest()
        {
            Assert.IsTrue(this.PageDriver.IsEventualyVisible(FirstNameText));
            Assert.IsFalse(this.PageDriver.IsEventualyVisible("NotReal"));
        }


        [TestMethod]
        public void IsHiddenTest()
        {
            Assert.IsFalse(this.PageDriver.IsHidden(DisabledField));
            Assert.IsTrue(this.PageDriver.IsHidden(TrainingOneLink));
            Assert.IsTrue(this.PageDriver.IsHidden("NotReal"));
        }

        [TestMethod]
        public void IsVisibleTest()
        {
            Assert.IsTrue(this.PageDriver.IsVisible(FirstNameText));
            Assert.IsFalse(this.PageDriver.IsVisible("NotReal"));
        }

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

        [TestMethod]
        public void TextContentTest()
        {
            Assert.AreEqual("Show dialog", this.PageDriver.TextContent(ShowDialog1));
        }

        [TestMethod]
        public void TitleTest()
        {
            Assert.AreEqual("Automation - Magenic Automation Test Site", this.PageDriver.Title());
        }

        [TestMethod]
        public void TypeAndInputalueTest()
        {
            this.PageDriver.Type(FirstNameText, "Ted");
            Assert.AreEqual("Ted", this.PageDriver.InputValue(FirstNameText));
        }

        [TestMethod]
        public void UncheckTest()
        {
            this.PageDriver.Uncheck(Checkbox2);
        }

        [TestMethod]
        public void WaitForLoadStateTest()
        {
            this.PageDriver.WaitForLoadState();
            Assert.IsTrue(this.PageDriver.IsVisible(Checkbox2));
        }

        [TestMethod]
        public void WaitForSelectorTest()
        {
            this.PageDriver.WaitForSelector(Checkbox2);
            Assert.IsTrue(this.PageDriver.IsVisible(Checkbox2));
        }

        [TestMethod]
        public void WaitForTimeoutTest()
        {

            var before = DateTime.Now;
            this.PageDriver.WaitForTimeout(1000);
            var afterWait = DateTime.Now;

            Assert.IsTrue(afterWait > before.AddMilliseconds(800) && afterWait < before.AddMilliseconds(1200), $"Sleep should have been about 1 second but was {(before - afterWait).TotalSeconds} seconds");
        }

        [TestMethod]
        public void WaitForURLTest()
        {
            this.PageDriver.Click(AsyncPageLink);
            this.PageDriver.WaitForURL("**/async.html");
        }
    }
}
