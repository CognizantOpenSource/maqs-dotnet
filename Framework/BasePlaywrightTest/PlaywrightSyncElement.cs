//--------------------------------------------------
// <copyright file="PlaywrightSyncElement.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Playwright synchronous element wrapper</summary>
//--------------------------------------------------
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace CognizantSoftvision.Maqs.BasePlaywrightTest
{
    /// <summary>
    /// Playwright synchronous element
    /// </summary>
    public class PlaywrightSyncElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaywrightSyncElement" /> class
        /// </summary>
        /// <param name="page">The assoicated playwright page</param>
        /// <param name="selector">Element selector</param>
        /// <param name="options">Advanced locator options</param>
        public PlaywrightSyncElement(IPage page, string selector, PageLocatorOptions? options = null)
        {
            this.ParentPage = page;
            this.Selector = selector;
            this.PageOptions = options;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaywrightSyncElement" /> class
        /// </summary>
        /// <param name="parent">The parent playwright element</param>
        /// <param name="selector">Sub element selector</param>
        /// <param name="options">Advanced locator options</param>
        public PlaywrightSyncElement(PlaywrightSyncElement parent, string selector, LocatorLocatorOptions? options = null)
        {
            this.ParentLocator = parent.ElementLocator();
            this.Selector = selector;
            this.LocatorOptions = options;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaywrightSyncElement" /> class
        /// </summary>
        /// <param name="frame">The assoicated playwright frame locator</param>
        /// <param name="selector">Element selector</param>
        public PlaywrightSyncElement(IFrameLocator frame, string selector)
        {
            this.ParentFrameLocator = frame;
            this.Selector = selector;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaywrightSyncElement" /> class
        /// </summary>
        /// <param name="testObject">The assoicated playwright test object</param>
        /// <param name="selector">Element selector</param>
        /// <param name="options">Advanced locator options</param>
        public PlaywrightSyncElement(IPlaywrightTestObject testObject, string selector, PageLocatorOptions? options = null) : this(testObject.PageDriver.AsyncPage, selector, options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaywrightSyncElement" /> class
        /// </summary>
        /// <param name="driver">The assoicated playwright page driver</param>
        /// <param name="selector">Element selector</param>
        /// <param name="options">Advanced locator options</param>
        public PlaywrightSyncElement(PageDriver driver, string selector, PageLocatorOptions? options = null) : this(driver.AsyncPage, selector, options)
        {
        }

        /// <summary>
        /// Gets the parent async page
        /// </summary>
        public IPage? ParentPage { get; private set; }

        /// <summary>
        /// Gets the parent locator
        /// </summary>
        public ILocator? ParentLocator { get; private set; }

        /// <summary>
        /// Gets the parent frame locator
        /// </summary>
        public IFrameLocator? ParentFrameLocator { get; private set; }

        /// <summary>
        /// Gets the page locator options
        /// </summary>
        public PageLocatorOptions? PageOptions { get; private set; }

        /// <summary>
        /// Gets the page locator options
        /// </summary>
        public LocatorLocatorOptions? LocatorOptions { get; private set; }

        /// <summary>
        /// Gets the selector string
        /// </summary>
        public string Selector { get; private set; }

        /// <summary>
        /// ILocator for this element
        /// </summary>
        /// <returns></returns>
        public ILocator ElementLocator()
        {
            if(this.ParentPage != null)
            {
                return this.ParentPage.Locator(Selector, PageOptions);
            }
            else if(this.ParentLocator != null)
            {
                return this.ParentLocator.Locator(Selector, LocatorOptions);
            }
            else if (this.ParentFrameLocator != null)
            {
                return this.ParentFrameLocator.Locator(Selector);
            }

            throw new NullReferenceException("Both parent IPage and PlaywrightElement are null");
        }

        /// <inheritdoc cref = "ILocator.CheckAsync" /> 
        public void Check(LocatorCheckOptions? options = null)
        {
            ElementLocator().CheckAsync(options).Wait();
        }

        /// <inheritdoc cref = "ILocator.ClickAsync" /> 
        public void Click(LocatorClickOptions? options = null)
        {
            ElementLocator().ClickAsync(options).Wait();
        }

        /// <inheritdoc cref = "ILocator.DblClickAsync" /> 
        public void DblClick(LocatorDblClickOptions? options = null)
        {
            ElementLocator().DblClickAsync(options).Wait();
        }

        /// <inheritdoc cref = "ILocator.DispatchEventAsync" /> 
        public void DispatchEvent(string type, object? eventInit = null, LocatorDispatchEventOptions? options = null)
        {
            ElementLocator().DispatchEventAsync(type, eventInit, options).Wait();
        }

        /// <inheritdoc cref = "ILocator.DragToAsync(ILocator, LocatorDragToOptions?)" /> 
        public void DragTo(ILocator target, LocatorDragToOptions? options = null)
        {
            ElementLocator().DragToAsync(target, options).Wait();
        }

        /// <inheritdoc cref = "ILocator.FillAsync" /> 
        public void Fill(string value, LocatorFillOptions? options = null)
        {
            ElementLocator().FillAsync(value, options).Wait();
        }

        /// <inheritdoc cref = "ILocator.FocusAsync" /> 
        public void Focus(LocatorFocusOptions? options = null)
        {
            ElementLocator().FocusAsync(options).Wait();
        }

        /// <inheritdoc cref = "ILocator.HoverAsync" /> 
        public void Hover(LocatorHoverOptions? options = null)
        {
            ElementLocator().HoverAsync(options).Wait();
        }

        /// <inheritdoc cref = "ILocator.PressAsync" /> 
        public void Press(string key, LocatorPressOptions? options = null)
        {
            ElementLocator().PressAsync(key, options).Wait();
        }

        /// <inheritdoc cref = "ILocator.SetCheckedAsync" /> 
        public void SetChecked(bool checkedState, LocatorSetCheckedOptions? options = null)
        {
            ElementLocator().SetCheckedAsync(checkedState, options).Wait();
        }

        /// <inheritdoc cref = "ILocator.SetInputFilesAsync(FilePayload, LocatorSetInputFilesOptions)" /> 
        public void SetInputFiles(FilePayload files, LocatorSetInputFilesOptions? options = null)
        {
            ElementLocator().SetInputFilesAsync(files, options).Wait();
        }

        /// <inheritdoc cref = "ILocator.SetInputFilesAsync(IEnumerable{FilePayload}, LocatorSetInputFilesOptions)" /> 
        public void SetInputFiles(IEnumerable<FilePayload> files, LocatorSetInputFilesOptions? options = null)
        {
            ElementLocator().SetInputFilesAsync(files, options).Wait();
        }

        /// <inheritdoc cref = "ILocator.SetInputFilesAsync(IEnumerable{string}, LocatorSetInputFilesOptions)" /> 
        public void SetInputFiles(IEnumerable<string> files, LocatorSetInputFilesOptions? options = null)
        {
            ElementLocator().SetInputFilesAsync(files, options).Wait();
        }

        /// <inheritdoc cref = "ILocator.SetInputFilesAsync(string, LocatorSetInputFilesOptions)" /> 
        public void SetInputFiles(string files, LocatorSetInputFilesOptions? options = null)
        {
            ElementLocator().SetInputFilesAsync(files, options).Wait();
        }

        /// <inheritdoc cref = "ILocator.TapAsync" /> 
        public void Tap(LocatorTapOptions? options = null)
        {
            ElementLocator().TapAsync(options).Wait();
        }

        /// <inheritdoc cref = "ILocator.TypeAsync" /> 
        public void Type(string text, LocatorTypeOptions? options = null)
        {
            ElementLocator().TypeAsync(text, options).Wait();
        }

        /// <inheritdoc cref = "ILocator.UncheckAsync" /> 
        public void Uncheck(LocatorUncheckOptions? options = null)
        {
            ElementLocator().UncheckAsync(options).Wait();
        }

        /// <inheritdoc cref = "ILocator.IsCheckedAsync"  />
        public bool IsChecked(LocatorIsCheckedOptions? options = null)
        {
            return ElementLocator().IsCheckedAsync(options).Result;
        }

        /// <inheritdoc cref = "ILocator.IsDisabledAsync"  />
        public bool IsDisabled(LocatorIsDisabledOptions? options = null)
        {
            return ElementLocator().IsDisabledAsync(options).Result;
        }

        /// <inheritdoc cref = "ILocator.IsEditableAsync"  />
        public bool IsEditable(LocatorIsEditableOptions? options = null)
        {
            return ElementLocator().IsEditableAsync(options).Result;
        }

        /// <inheritdoc cref = "ILocator.IsEnabledAsync"  />
        public bool IsEnabled(LocatorIsEnabledOptions? options = null)
        {
            return ElementLocator().IsEnabledAsync(options).Result;
        }

        /// <inheritdoc cref = "ILocator.IsHiddenAsync"  />
        public bool IsHidden(LocatorIsHiddenOptions? options = null)
        {
            return ElementLocator().IsHiddenAsync(options).Result;
        }

        /// <inheritdoc cref = "ILocator.IsVisibleAsync"  />
        public bool IsVisible(LocatorIsVisibleOptions? options = null)
        {
            return ElementLocator().IsVisibleAsync(options).Result;
        }

        /// <summary>
        /// Check that the element is eventually visible
        /// </summary>
        /// <returns>True if the element becomes visible within the page timeout</returns>
        public bool IsEventualyVisible()
        {
            try
            {
                this.ElementLocator().WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                }).Wait();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check that the element stops being visible
        /// </summary>
        /// <returns>True if the element becomes is hidden or gone within the page timeout</returns>
        public bool IsEventualyGone()
        {
            try
            {
                this.ElementLocator().WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Hidden,
                }).Wait();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc cref = "ILocator.SelectOptionAsync(IElementHandle, LocatorSelectOptionOptions)"  />
        public IReadOnlyList<string> SelectOption(IElementHandle values, LocatorSelectOptionOptions? options = null)
        {
            return ElementLocator().SelectOptionAsync(values, options).Result;
        }

        /// <inheritdoc cref = "ILocator.SelectOptionAsync(IEnumerable{IElementHandle}, LocatorSelectOptionOptions)"  />
        public IReadOnlyList<string> SelectOption(IEnumerable<IElementHandle> values, LocatorSelectOptionOptions? options = null)
        {
            return ElementLocator().SelectOptionAsync(values, options).Result;
        }

        /// <inheritdoc cref = "ILocator.SelectOptionAsync(IEnumerable{SelectOptionValue}, LocatorSelectOptionOptions)"  />
        public IReadOnlyList<string> SelectOption(IEnumerable<SelectOptionValue> values, LocatorSelectOptionOptions? options = null)
        {
            return ElementLocator().SelectOptionAsync(values, options).Result;
        }

        /// <inheritdoc cref = "ILocator.SelectOptionAsync(IEnumerable{string}, LocatorSelectOptionOptions)"  />
        public IReadOnlyList<string> SelectOption(IEnumerable<string> values, LocatorSelectOptionOptions? options = null)
        {
            return ElementLocator().SelectOptionAsync(values, options).Result;
        }

        /// <inheritdoc cref = "ILocator.SelectOptionAsync(SelectOptionValue, LocatorSelectOptionOptions)"  />
        public IReadOnlyList<string> SelectOption(SelectOptionValue values, LocatorSelectOptionOptions? options = null)
        {
            return ElementLocator().SelectOptionAsync(values, options).Result;
        }

        /// <inheritdoc cref = "ILocator.SelectOptionAsync(string, LocatorSelectOptionOptions)"  />
        public IReadOnlyList<string> SelectOption(string values, LocatorSelectOptionOptions? options = null)
        {
            return ElementLocator().SelectOptionAsync(values, options).Result;
        }

        /// <inheritdoc cref = "ILocator.EvaluateAllAsync"  />
        public T EvalOnSelectorAll<T>(string expression, object? arg = null)
        {
            return ElementLocator().EvaluateAllAsync<T>(expression, arg).Result;
        }

        /// <inheritdoc cref = "ILocator.EvaluateAsync"  />
        public JsonElement? Evaluate(string expression, object? arg = null)
        {
            return ElementLocator().EvaluateAsync(expression, arg).Result;
        }

        /// <inheritdoc cref = "ILocator.GetAttributeAsync"  />
        public string? GetAttribute(string name, LocatorGetAttributeOptions? options = null)
        {
            return ElementLocator().GetAttributeAsync(name, options).Result;
        }

        /// <inheritdoc cref = "ILocator.TextContentAsync"  />
        public string? TextContent(LocatorTextContentOptions? options = null)
        {
            return ElementLocator().TextContentAsync(options).Result;
        }

        /// <inheritdoc cref = "ILocator.AllTextContentsAsync"  />
        public IReadOnlyList<string> AllTextContents()
        {
            return ElementLocator().AllTextContentsAsync().Result;
        }

        /// <inheritdoc cref = "ILocator.AllInnerTextsAsync"  />
        public IReadOnlyList<string> AllInnerTexts()
        {
            return ElementLocator().AllInnerTextsAsync().Result;
        }

        /// <inheritdoc cref = "ILocator.InnerHTMLAsync"  />
        public string InnerHTML(LocatorInnerHTMLOptions? options = null)
        {
            return ElementLocator().InnerHTMLAsync(options).Result;
        }

        /// <inheritdoc cref = "ILocator.InnerTextAsync"  />
        public string InnerText(LocatorInnerTextOptions? options = null)
        {
            return ElementLocator().InnerTextAsync(options).Result;
        }

        /// <inheritdoc cref = "ILocator.InputValueAsync"  />
        public string InputValue(LocatorInputValueOptions? options = null)
        {
            return ElementLocator().InputValueAsync(options).Result;
        }
    }
}
