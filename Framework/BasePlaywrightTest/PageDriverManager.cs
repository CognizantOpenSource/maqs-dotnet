//--------------------------------------------------
// <copyright file="PlaywrightDriverManager.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Playwright driver</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseTest;
using CognizantSoftvision.Maqs.Utilities.Data;
using CognizantSoftvision.Maqs.Utilities.Logging;
using Microsoft.Playwright;
using System;
using System.Reflection;
using System.Text;

namespace CognizantSoftvision.Maqs.BasePlaywrightTest
{
    /// <summary>
    /// Playwright driver store
    /// </summary>
    public class PlaywrightDriverManager : DriverManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaywrightDriverManager"/> class
        /// </summary>
        /// <param name="getDriver">Function for getting an Playwright page</param>
        /// <param name="testObject">The associated test object</param>
        public PlaywrightDriverManager(Func<PageDriver> getDriver, ITestObject testObject) : base(getDriver, testObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaywrightDriverManager"/> class
        /// </summary>
        /// <param name="getDriver">Function for getting an Playwright page</param>
        /// <param name="testObject">The associated test object</param>
        public PlaywrightDriverManager(Func<IPage> getDriver, ITestObject testObject) : base(() => new PageDriver(getDriver()), testObject)
        {
        }

        /// <summary>
        /// Override the page
        /// </summary>
        /// <param name="overrideDriver">The new page</param>
        public void OverrideDriver(PageDriver overrideDriver)
        {
            this.OverrideDriverGet(() => overrideDriver);
        }

        /// <summary>
        /// Override the page
        /// </summary>
        /// <param name="overrideDriver">Function for getting a new page</param>
        public void OverrideDriver(Func<PageDriver> overrideDriver)
        {
            this.OverrideDriverGet(overrideDriver);
        }

        /// <summary>
        /// Override the page
        /// </summary>
        /// <param name="overridePage">Function for getting a new page</param>
        public void OverrideDriver(Func<IPage> overridePage)
        {
            this.OverrideDriverGet(() => new PageDriver(overridePage()));
        }

        /// <summary>
        /// Get the page
        /// </summary>
        /// <returns>The page</returns>
        public PageDriver GetPageDriver()
        {
            PageDriver tempDriver;

            if (!this.IsDriverIntialized() && LoggingConfig.GetLoggingEnabledSetting() != LoggingEnabled.NO)
            {
                tempDriver = GetDriver() as PageDriver;
                // TODO: Maybe add event logging
                //////////tempDriver = tempDriver.GetLowLevelDriver();
                //////////tempDriver = new EventFiringPageDriver(tempDriver);
                //////////this.MapEvents(tempDriver as EventFiringPageDriver);

                this.BaseDriver = tempDriver;

                // Log the setup
                this.LoggingStartup(tempDriver);
            }

            return GetBase() as PageDriver;
        }

        /// <summary>
        /// Get the page
        /// </summary>
        /// <returns>The page</returns>
        public override object Get()
        {
            return this.GetPageDriver();
        }

        /// <summary>
        /// Log a verbose message and include the automation specific call stack data
        /// </summary>
        /// <param name="message">The message text</param>
        /// <param name="args">String format arguments</param>
        protected void LogVerbose(string message, params object[] args)
        {
            StringBuilder messages = new StringBuilder();
            messages.AppendLine(StringProcessor.SafeFormatter(message, args));

            var methodInfo = MethodBase.GetCurrentMethod();
            var fullName = $"{methodInfo.DeclaringType.FullName}.{methodInfo.Name}";

            foreach (string stackLevel in Environment.StackTrace.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                string trimmed = stackLevel.Trim();
                if (!trimmed.StartsWith("at Microsoft.") && !trimmed.StartsWith("at System.") && !trimmed.StartsWith("at NUnit.") && !trimmed.StartsWith($"at {fullName}"))
                {
                    messages.AppendLine(stackLevel);
                }
            }

            Log.LogMessage(MessageType.VERBOSE, messages.ToString());
        }

        /// <summary>
        /// Have the driver cleanup after itself
        /// </summary>
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

        /// <summary>
        /// Log that the page setup
        /// </summary>
        /// <param name="pageDriver">The page</param>
        private void LoggingStartup(PageDriver pageDriver)
        {
            try
            {
                Log.LogMessage(MessageType.INFORMATION, $"Driver: {pageDriver.ParentBrower}");
            }
            catch (Exception e)
            {
                Log.LogMessage(MessageType.ERROR, $"Failed to start driver because: {e.Message}");
                Console.WriteLine($"Failed to start driver because: {e.Message}");
            }
        }

        //// TODO
        ///////// <summary>
        ///////// Map Playwright events to log events
        ///////// </summary>
        ///////// <param name="eventFiringDriver">The event firing page that we want mapped</param>
        //////private void MapEvents(EventFiringPageDriver eventFiringDriver)
        //////{
        //////    LoggingEnabled enbled = LoggingConfig.GetLoggingEnabledSetting();

        //////    if (enbled == LoggingEnabled.YES || enbled == LoggingEnabled.ONFAIL)
        //////    {
        //////        eventFiringDriver.ElementClicked += this.PageDriver_ElementClicked;
        //////        eventFiringDriver.ElementClicking += this.PageDriver_ElementClicking;
        //////        eventFiringDriver.ElementValueChanged += this.PageDriver_ElementValueChanged;
        //////        eventFiringDriver.ElementValueChanging += this.PageDriver_ElementValueChanging;
        //////        eventFiringDriver.FindElementCompleted += this.PageDriver_FindElementCompleted;
        //////        eventFiringDriver.FindingElement += this.PageDriver_FindingElement;
        //////        eventFiringDriver.ScriptExecuted += this.PageDriver_ScriptExecuted;
        //////        eventFiringDriver.ScriptExecuting += this.PageDriver_ScriptExecuting;
        //////        eventFiringDriver.Navigated += this.PageDriver_Navigated;
        //////        eventFiringDriver.Navigating += this.PageDriver_Navigating;
        //////        eventFiringDriver.NavigatedBack += this.PageDriver_NavigatedBack;
        //////        eventFiringDriver.NavigatedForward += this.PageDriver_NavigatedForward;
        //////        eventFiringDriver.NavigatingBack += this.PageDriver_NavigatingBack;
        //////        eventFiringDriver.NavigatingForward += this.PageDriver_NavigatingForward;
        //////        eventFiringDriver.ExceptionThrown += this.PageDriver_ExceptionThrown;
        //////    }
        //////}

        ///////// <summary>
        ///////// Event for PageDriver that is navigating forward
        ///////// </summary>
        ///////// <param name="sender">Sender object</param>
        ///////// <param name="e">Event object</param>
        //////private void PageDriver_NavigatingForward(object sender, PageDriverNavigationEventArgs e)
        //////{
        //////    this.LogVerbose($"Navigating to: {e.Url}");
        //////}

        ///////// <summary>
        ///////// Event for PageDriver that is navigating back
        ///////// </summary>
        ///////// <param name="sender">Sender object</param>
        ///////// <param name="e">Event object</param>
        //////private void PageDriver_NavigatingBack(object sender, PageDriverNavigationEventArgs e)
        //////{
        //////    this.LogVerbose($"Navigating back to: {e.Url}");
        //////}

        ///////// <summary>
        ///////// Event for PageDriver that has navigated forward
        ///////// </summary>
        ///////// <param name="sender">Sender object</param>
        ///////// <param name="e">Event object</param>
        //////private void PageDriver_NavigatedForward(object sender, PageDriverNavigationEventArgs e)
        //////{
        //////    Log.LogMessage(MessageType.ACTION, $"Navigate Forward: {e.Url}");
        //////}

        ///////// <summary>
        ///////// Event for PageDriver that is navigated back
        ///////// </summary>
        ///////// <param name="sender">Sender object</param>
        ///////// <param name="e">Event object</param>
        //////private void PageDriver_NavigatedBack(object sender, PageDriverNavigationEventArgs e)
        //////{
        //////    Log.LogMessage(MessageType.ACTION, $"Navigate back: {e.Url}");
        //////}

        ///////// <summary>
        ///////// Event for PageDriver that is navigating
        ///////// </summary>
        ///////// <param name="sender">Sender object</param>
        ///////// <param name="e">Event object</param>
        //////private void PageDriver_Navigating(object sender, PageDriverNavigationEventArgs e)
        //////{
        //////    this.LogVerbose($"Navigating to: {e.Url}");
        //////}

        ///////// <summary>
        ///////// Event for PageDriver that is script executing
        ///////// </summary>
        ///////// <param name="sender">Sender object</param>
        ///////// <param name="e">Event object</param>
        //////private void PageDriver_ScriptExecuting(object sender, PageDriverScriptEventArgs e)
        //////{
        //////    this.LogVerbose($"Script executing: {e.Script}");
        //////}

        ///////// <summary>
        ///////// Event for PageDriver that is finding an element
        ///////// </summary>
        ///////// <param name="sender">Sender object</param>
        ///////// <param name="e">Event object</param>
        //////private void PageDriver_FindingElement(object sender, FindElementEventArgs e)
        //////{
        //////    this.LogVerbose($"Finding element: {e.FindMethod}");
        //////}

        ///////// <summary>
        ///////// Event for PageDriver that is changing an element value
        ///////// </summary>
        ///////// <param name="sender">Sender object</param>
        ///////// <param name="e">Event object</param>
        //////private void PageDriver_ElementValueChanging(object sender, WebElementEventArgs e)
        //////{
        //////    this.LogVerbose($"Value of element changing: {e.Element}");
        //////}

        ///////// <summary>
        ///////// Event for PageDriver that is clicking an element
        ///////// </summary>
        ///////// <param name="sender">Sender object</param>
        ///////// <param name="e">Event object</param>
        //////private void PageDriver_ElementClicking(object sender, WebElementEventArgs e)
        //////{
        //////    Log.LogMessage(MessageType.INFORMATION, $"Element clicking: {e.Element} Text:{e.Element.Text} Location: X:{e.Element.Location.X} Y:{e.Element.Location.Y}");
        //////}

        ///////// <summary>
        ///////// Event for PageDriver when an exception is thrown
        ///////// </summary>
        ///////// <param name="sender">Sender object</param>
        ///////// <param name="e">Event object</param>
        //////private void PageDriver_ExceptionThrown(object sender, PageDriverExceptionEventArgs e)
        //////{
        //////    // First chance handler catches these when it is a real error - These are typically retry loops
        //////    Log.LogMessage(MessageType.VERBOSE, $"Exception thrown: {e.ThrownException}");
        //////}

        ///////// <summary>
        ///////// Event for PageDriver that has navigated
        ///////// </summary>
        ///////// <param name="sender">Sender object</param>
        ///////// <param name="e">Event object</param>
        //////private void PageDriver_Navigated(object sender, PageDriverNavigationEventArgs e)
        //////{
        //////    Log.LogMessage(MessageType.INFORMATION, $"Navigated to: {e.Url}");
        //////}

        ///////// <summary>
        ///////// Event for PageDriver has executed a script
        ///////// </summary>
        ///////// <param name="sender">Sender object</param>
        ///////// <param name="e">Event object</param>
        //////private void PageDriver_ScriptExecuted(object sender, PageDriverScriptEventArgs e)
        //////{
        //////    Log.LogMessage(MessageType.INFORMATION, $"Script executed: {e.Script}");
        //////}

        ///////// <summary>
        ///////// Event for PageDriver that is finished finding an element
        ///////// </summary>
        ///////// <param name="sender">Sender object</param>
        ///////// <param name="e">Event object</param>
        //////private void PageDriver_FindElementCompleted(object sender, FindElementEventArgs e)
        //////{
        //////    Log.LogMessage(MessageType.INFORMATION, $"Found element: {e.FindMethod}");
        //////}

        ///////// <summary>
        ///////// Event for PageDriver that has changed an element value
        ///////// </summary>
        ///////// <param name="sender">Sender object</param>
        ///////// <param name="e">Event object</param>
        //////private void PageDriver_ElementValueChanged(object sender, WebElementEventArgs e)
        //////{
        //////    string element = e.Element.GetAttribute("value");
        //////    Log.LogMessage(MessageType.INFORMATION, $"Element value changed: {element}");
        //////}

        ///////// <summary>
        ///////// Event for PageDriver that has clicked an element
        ///////// </summary>
        ///////// <param name="sender">Sender object</param>
        ///////// <param name="e">Event object</param>
        //////private void PageDriver_ElementClicked(object sender, WebElementEventArgs e)
        //////{
        //////    try
        //////    {
        //////        this.LogVerbose($"Element clicked: {e.Element} Text:{e.Element.Text} Location: X:{e.Element.Location.X} Y:{e.Element.Location.Y}");
        //////    }
        //////    catch
        //////    {
        //////        this.LogVerbose("Element clicked");
        //////    }
        //////}
    }
}
