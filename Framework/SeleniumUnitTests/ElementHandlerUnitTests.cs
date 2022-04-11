﻿//--------------------------------------------------
// <copyright file="ElementHandlerUnitTests.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Helper class for getting selenium specific configuration values</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseSeleniumTest.Extensions;
using CognizantSoftvision.Maqs.Utilities.Helper;
using CognizantSoftvision.Maqs.Utilities.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace SeleniumUnitTests
{
    /// <summary>
    /// Unit tests for the ElementHandler class
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ElementHandlerUnitTests : SauceLabsBaseSeleniumTest
    {
        /// <summary>
        /// Url for the site
        /// </summary>
        private static readonly string siteUrl = SeleniumConfig.GetWebSiteBase();

        /// <summary>
        /// Automation site url
        /// </summary>
        private static readonly string siteAutomationUrl = siteUrl + "index.html";

        /// <summary>
        /// Options for computer parts list
        /// </summary>
        private static readonly By computerPartsListOptions = By.CssSelector("#computerParts > option");

        /// <summary>
        /// First name textbox
        /// </summary>
        private static readonly By firstNameTextBox = By.CssSelector("#TextFields > p:nth-child(1) > input[type=\"text\"]");

        /// <summary>
        /// Female radio button
        /// </summary>
        private static readonly By femaleRadioButton = By.CssSelector("#FemaleRadio");

        /// <summary>
        /// First checkbox
        /// </summary>
        private static readonly By checkbox = By.CssSelector("#Checkbox1");

        /// <summary>
        /// Name dropdown list
        /// </summary>
        private static readonly By nameDropdown = By.CssSelector("#namesDropdown");

        /// <summary>
        /// Computer parts list
        /// </summary>
        private static readonly By computerPartsList = By.CssSelector("#computerParts");

        /// <summary>
        /// Training 1 login link
        /// </summary>
        private static readonly By trainingLink = By.CssSelector("A[href='../Training1/LoginPage.html']");

        /// <summary>
        /// Traing login button
        /// </summary>
        private static readonly By trainingLoginButton = By.CssSelector("BUTTON#Login");

        /// <summary>
        /// Show dialog button
        /// </summary>
        private static readonly By showDialog = By.CssSelector("#showDialog1");

        /// <summary>
        /// Close show dialog box button
        /// </summary>
        private static readonly By closeShowDialog = By.CssSelector("#CloseButtonShowDialog");

        /// <summary>
        /// Unit Test for creating a sorted comma delimited string
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void CreateSortedCommaDelimitedStringFromWebElementsTest()
        {
            string expectedText = "Hard Drive, Keyboard, Monitor, Motherboard, Mouse, Power Supply";
            NavigateToUrl();
            Assert.AreEqual(expectedText, WebDriver.CreateCommaDelimitedString(computerPartsListOptions, true), "Expected string does not match actual");
        }

        /// <summary>
        /// Unit Test for creating a comma delimited string
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void CreateCommaDelimitedStringFromWebElementsTest()
        {
            string expectedText = "Motherboard, Power Supply, Hard Drive, Monitor, Mouse, Keyboard";
            NavigateToUrl();
            Assert.AreEqual(expectedText, WebDriver.CreateCommaDelimitedString(computerPartsListOptions), "Expected string does not match actual");
        }

        /// <summary>
        /// Unit test for entering text into a textbox and getting text from a textbox
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void SetTextBoxAndVerifyValueTest()
        {
            string expectedValue = "Tester";
            NavigateToUrl();
            WebDriver.SetTextBox(firstNameTextBox, expectedValue);
            string actualValue = WebDriver.GetElementAttribute(firstNameTextBox);
            VerifyText(actualValue, expectedValue);
        }

        /// <summary>
        /// Unit test for entering text into a textbox and getting text from a textbox
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void SetTextBoxAndVerifyValueTestWithLazy()
        {
            string expectedValue = "Tester";
            NavigateToUrl();
            LazyElement lazy = new LazyElement(this.TestObject, firstNameTextBox);
            lazy.SetTextBox(expectedValue);
            string actualValue = WebDriver.GetElementAttribute(firstNameTextBox);
            VerifyText(actualValue, expectedValue);
        }

        /// <summary>
        /// Check that SetTextBox throws correct exception with an empty input string
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        [ExpectedException(typeof(ArgumentException))]
        public void SetTextBoxThrowException()
        {
            NavigateToUrl();
            WebDriver.SetTextBox(firstNameTextBox, string.Empty);
        }

        /// <summary>
        /// Unit Test for checking a radio button
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void CheckRadioButtonTest()
        {
            NavigateToUrl();
            WebDriver.ClickButton(femaleRadioButton, false);
            Assert.IsTrue(WebDriver.Wait().ForClickableElement(femaleRadioButton).Selected, "Radio button was not selected");
        }

        /// <summary>
        /// Test ClickButton called with WaitForButtonToDisappear as true
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void ClickButtonWaitForButtonToDisappear()
        {
            NavigateToUrl();
            WebDriver.ClickButton(showDialog, false);
            Assert.IsTrue(WebDriver.Wait().ForClickableElement(closeShowDialog).Displayed, "Show Dialog box was not displayed");

            WebDriver.ClickButton(closeShowDialog, true);
            Assert.IsFalse(WebDriver.FindElement(closeShowDialog).Displayed, "Show Dialog box was not closed");
        }

        /// <summary>
        /// Unit Test for checking a checkbox
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void CheckCheckBoxTest()
        {
            NavigateToUrl();
            WebDriver.CheckCheckBox(checkbox, true);
            Assert.IsTrue(WebDriver.Wait().ForClickableElement(checkbox).Selected, "Checkbox was not enabled");

            WebDriver.CheckCheckBox(checkbox, false);
            Assert.IsFalse(WebDriver.Wait().ForClickableElement(checkbox).Selected, "Checkbox was enabled");

            // Check the box again for code coverage
            WebDriver.CheckCheckBox(checkbox, false);
            Assert.IsFalse(WebDriver.Wait().ForClickableElement(checkbox).Selected, "Checkbox was enabled");
        }

        /// <summary>
        /// Unit Test for get element attribute function
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void GetElementAttributeTest()
        {
            NavigateToUrl();
            string actualText = WebDriver.GetElementAttribute(firstNameTextBox, "type");
            VerifyText(actualText, "text");
        }

        /// <summary>
        /// Unit Test for selecting an item from a dropdown and getting the selected item from a dropdown (By actual value)
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void SelectItemFromDropDownTest()
        {
            string expectedSelection = "Emily";
            NavigateToUrl();
            WebDriver.SelectDropDownOption(nameDropdown, expectedSelection);
            string actualSelection = WebDriver.GetSelectedOptionFromDropdown(nameDropdown);
            VerifyText(actualSelection, expectedSelection);
        }

        /// <summary>
        /// Unit Test for selecting an item from a dropdown and getting the selected item from a dropdown (By actual value)
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void SelectItemFromDropDownTestWithElement()
        {
            string expectedSelection = "Emily";
            NavigateToUrl();
            var dropdown = this.WebDriver.FindElement(nameDropdown);
            dropdown.SelectDropDownOption(expectedSelection);
            string actualSelection = WebDriver.GetSelectedOptionFromDropdown(nameDropdown);
            VerifyText(actualSelection, expectedSelection);
        }

        /// <summary>
        /// Unit Test for selecting an item from a dropdown and getting the selected item from a dropdown (By list value)
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void SelectItemFromDropDownByValueTest()
        {
            string expectedSelection = "Jack";
            NavigateToUrl();
            this.WebDriver.Wait().ForPageLoad();
            WebDriver.SelectDropDownOptionByValue(nameDropdown, "two");
            string actualSelection = WebDriver.GetSelectedOptionFromDropdown(nameDropdown);
            VerifyText(actualSelection, expectedSelection);
        }

        /// <summary>
        /// Unit Test for selecting an item from a dropdown and getting the selected item from a dropdown (By list value)
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void SelectItemFromDropDownByValueTestWithElement()
        {
            string expectedSelection = "Jack";
            NavigateToUrl();
            this.WebDriver.Wait().ForPageLoad();
            var element = this.WebDriver.FindElement(nameDropdown);

            element.SelectDropDownOptionByValue("two");
            string actualSelection = WebDriver.GetSelectedOptionFromDropdown(nameDropdown);
            VerifyText(actualSelection, expectedSelection);
        }

        /// <summary>
        /// Unit Test for selecting multiple items from a list box and getting all selected items in a list box(By actual value)
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void SelectMultipleItemsFromListBoxTest()
        {
            StringBuilder results = new StringBuilder();

            List<string> itemsToSelect = new List<string>
            {
                "Monitor",
                "Hard Drive",
                "Keyboard"
            };

            NavigateToUrl();
            WebDriver.SelectMultipleElementsFromListBox(computerPartsList, itemsToSelect);
            List<string> selectedItems = WebDriver.GetSelectedOptionsFromDropdown(computerPartsList);
            ListProcessor.ListOfStringsComparer(itemsToSelect, selectedItems, results);
            if (results.Length > 0)
            {
                Assert.Fail(results.ToString());
            }
        }

        /// <summary>
        /// Unit Test for selecting multiple items from a list box and getting all selected items in a list box(By actual value)
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void SelectMultipleItemsFromListBoxTestWithElement()
        {
            StringBuilder results = new StringBuilder();

            List<string> itemsToSelect = new List<string>
            {
                "Monitor",
                "Hard Drive",
                "Keyboard"
            };

            NavigateToUrl();
            var element = this.WebDriver.Wait().ForClickableElement(computerPartsList);
            element.SelectMultipleElementsFromListBox(itemsToSelect);
            List<string> selectedItems = WebDriver.GetSelectedOptionsFromDropdown(computerPartsList);
            ListProcessor.ListOfStringsComparer(itemsToSelect, selectedItems, results);
            if (results.Length > 0)
            {
                Assert.Fail(results.ToString());
            }
        }

        /// <summary>
        /// Unit Test for selecting multiple items from a list box and getting all selected items in a list box(By list value)
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void SelectMultipleItemsFromListBoxTestByValue()
        {
            List<string> itemsToSelect = new List<string>
            {
                "one",
                "four",
                "five"
            };

            NavigateToUrl();
            WebDriver.SelectMultipleElementsFromListBoxByValue(computerPartsList, itemsToSelect);
            List<string> selectedItems = WebDriver.GetSelectedOptionsFromDropdown(computerPartsList);

            if (selectedItems.Count != 3)
            {
                Assert.Fail("Does not contain 3 elements: " + selectedItems.ToString());
            }
        }

        /// <summary>
        /// Unit test for ClickElementByJavaScript using a hover dropdown, where dropdown is not visible
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void ClickElementByJavascriptFromHoverDropdown()
        {
            NavigateToUrl();
            WebDriver.ClickElementByJavaScript(trainingLink);
            WebDriver.Wait().ForPageLoad();
            WebDriver.Wait().ForExactText(trainingLoginButton, "Login");
        }

        /// <summary>
        /// Unit test for ClickElementByJavaScript using a hover dropdown, where dropdown is not visible
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void ClickElementByJavascriptFromHoverDropdownWithLazy()
        {
            NavigateToUrl();
            LazyElement button = new LazyElement(this.TestObject, trainingLink);
            button.ClickElementByJavaScript();
            WebDriver.Wait().ForPageLoad();
            WebDriver.Wait().ForExactText(trainingLoginButton, "Login");
        }

        /// <summary>
        /// Test to verify scrolling into view
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void ScrollIntoView()
        {
            NavigateToUrl();
            WebDriver.ScrollIntoView(checkbox);
        }

        /// <summary>
        /// Test to verify scrolling into view via web element
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void ScrollIntoViewWebElement()
        {
            NavigateToUrl();
            var element = this.WebDriver.FindElement(checkbox);
            WebDriver.ScrollIntoView(element);
        }

        /// <summary>
        /// Test to verify scrolling into view
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void ScrollIntoViewWithCoords()
        {
            NavigateToUrl();
            WebDriver.ScrollIntoView(checkbox, 50, 0);
        }

        /// <summary>
        /// Test to verify scrolling into view
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void ExecutingScrolling()
        {
            NavigateToUrl();
            WebDriver.ExecuteScrolling(50, 0);
        }

        /// <summary>
        /// Unit test for ClickElementByJavaScript where the element is not present
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        [ExpectedException(typeof(NoSuchElementException), "A JavaScript click that should have failed inappropriately passed.")]
        public void ClickElementByJavascriptFromHoverDropdownNotFound()
        {
            NavigateToUrl();
            WebDriver.ClickElementByJavaScript(By.CssSelector(".NotPresent"));
        }

        /// <summary>
        /// Verify slow type text is correctly typed
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void SlowTypeTest()
        {
            NavigateToUrl();
            WebDriver.SlowType(firstNameTextBox, "Test input slowtype");
            Assert.AreEqual("Test input slowtype", WebDriver.Wait().ForClickableElement(firstNameTextBox).GetAttribute("value"));
        }

        /// <summary>
        /// Verify Send Secret Keys suspends logging
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void SendSecretTextSuspendLoggingTest()
        {
            NavigateToUrl();
            WebDriver.FindElement(firstNameTextBox).SendKeys("somethingTest");
            WebDriver.FindElement(firstNameTextBox).Clear();
            WebDriver.SendSecretKeys(firstNameTextBox, "secretKeys", Log);

            IFileLogger logger = (IFileLogger)TestObject.Log;
            string filepath = logger.FilePath;

            Assert.IsFalse(File.ReadAllText(filepath).Contains("secretKeys"));
            File.Delete(filepath);
        }

        /// <summary>
        /// Verify that logging gets turned back on after secret key throws an error
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void SendSecretTextEnablesLoggingAfterError()
        {
            string checkLogged = "THISSHOULDBELOGGED";
            NavigateToUrl();
            Assert.ThrowsException<ArgumentNullException>(() => this.WebDriver.SendSecretKeys(firstNameTextBox, null, this.Log));
            this.Log.LogMessage(checkLogged);

            IFileLogger logger = (IFileLogger)TestObject.Log;
            string filepath = logger.FilePath;

            Assert.IsTrue(File.ReadAllText(filepath).Contains(checkLogged));
            File.Delete(filepath);
        }

        /// <summary>
        /// Verify Send Secret Keys re-enables after suspending logging
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Selenium)]
        public void SendSecretTextContinueLoggingTest()
        {
            NavigateToUrl();
            WebDriver.SendSecretKeys(firstNameTextBox, "secretKeys", Log);
            WebDriver.FindElement(firstNameTextBox).Clear();
            WebDriver.FindElement(firstNameTextBox).SendKeys("somethingTest");

            IFileLogger logger = (IFileLogger)TestObject.Log;
            string filepath = logger.FilePath;

            Assert.IsTrue(File.ReadAllText(filepath).Contains("somethingTest"));
            File.Delete(filepath);
        }

        /// <summary>
        /// Verify two strings are equal. If not fail test
        /// </summary>
        /// <param name="actualValue">Actual displayed text</param>
        /// <param name="expectedValue">Expected text</param>
        private static void VerifyText(string actualValue, string expectedValue)
        {
            Assert.AreEqual(expectedValue, actualValue, "Values are not equal");
        }

        /// <summary>
        /// Navigate to test page url and wait for page to load
        /// </summary>
        private void NavigateToUrl()
        {
            WebDriver.Navigate().GoToUrl(siteAutomationUrl);
            WebDriver.Wait().ForPageLoad();
        }
    }
}
