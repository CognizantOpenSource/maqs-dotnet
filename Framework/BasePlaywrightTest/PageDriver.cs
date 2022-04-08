using CognizantSoftvision.Maqs.Utilities.Helper;
using CognizantSoftvision.Maqs.Utilities.Logging;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace CognizantSoftvision.Maqs.BasePlaywrightTest
{
    /// <summary>
    /// Playwright page driver
    /// </summary>
    public class PageDriver : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageDriver"/> class
        /// </summary>
        /// <param name="page">Base page object</param>
        public PageDriver(IPage page)
        {
            this.AsyncPage = page;
        }

        /// <summary>
        /// Gets the underlying async page object
        /// </summary>
        public IPage AsyncPage { get; private set; }

        /// <inheritdoc cref = "IBrowserContext.Browser"  />
        public IBrowser? ParentBrower { get { return this.AsyncPage.Context.Browser; } }

        /// <inheritdoc cref = "IPage.Url"  />
        public string Url { get { return this.AsyncPage.Url; } }

        /// <inheritdoc cref = "IPage.IsClosed"  />
        public bool IsClosed { get { return this.AsyncPage.IsClosed; } }

        /// <inheritdoc cref = "IPage.AddInitScriptAsync" />  
        public void AddInitScript(string? script = null, string? scriptPath = null)
        {
            this.AsyncPage.AddInitScriptAsync(script, scriptPath).Wait();
        }

        /// <inheritdoc cref = "IPage.BringToFrontAsync" /> 
        public void BringToFront()
        {
            this.AsyncPage.BringToFrontAsync().Wait();
        }

        /// <inheritdoc cref = "IPage.CheckAsync" /> 
        public void Check(string selector, PageCheckOptions? options = null)
        {
            this.AsyncPage.CheckAsync(selector, options).Wait();
        }

        /// <inheritdoc cref = "IPage.ClickAsync" /> 
        public void Click(string selector, PageClickOptions? options = null)
        {
            this.AsyncPage.ClickAsync(selector, options).Wait();
        }

        /// <inheritdoc cref = "IPage.CloseAsync" /> 
        public void Close(PageCloseOptions? options = null)
        {
            this.AsyncPage.CloseAsync(options).Wait();
        }

        /// <inheritdoc cref = "IPage.DblClickAsync" /> 
        public void DblClick(string selector, PageDblClickOptions? options = null)
        {
            this.AsyncPage.DblClickAsync(selector, options).Wait();
        }

        /// <inheritdoc cref = "IPage.DispatchEventAsync" /> 
        public void DispatchEvent(string selector, string type, object? eventInit = null, PageDispatchEventOptions? options = null)
        {
            this.AsyncPage.DispatchEventAsync(selector, type, eventInit, options).Wait();
        }

        /// <inheritdoc cref = "IPage.DragAndDropAsync" /> 
        public void DragAndDrop(string source, string target, PageDragAndDropOptions? options = null)
        {
            this.AsyncPage.DragAndDropAsync(source, target, options).Wait();
        }

        /// <inheritdoc cref = "IPage.FillAsync" /> 
        public void Fill(string selector, string value, PageFillOptions? options = null)
        {
            this.AsyncPage.FillAsync(selector, value, options).Wait();
        }

        /// <inheritdoc cref = "IPage.FocusAsync" /> 
        public void Focus(string selector, PageFocusOptions? options = null)
        {
            this.AsyncPage.FocusAsync(selector, options).Wait();
        }

        /// <inheritdoc cref = "IPage.HoverAsync" /> 
        public void Hover(string selector, PageHoverOptions? options = null)
        {
            this.AsyncPage.HoverAsync(selector, options).Wait();
        }

        /// <inheritdoc cref = "IPage.PressAsync" /> 
        public void Press(string selector, string key, PagePressOptions? options = null)
        {
            this.AsyncPage.PressAsync(selector, key, options).Wait();
        }

        /// <inheritdoc cref = "IPage.SetCheckedAsync" /> 
        public void SetChecked(string selector, bool checkedState, PageSetCheckedOptions? options = null)
        {
            this.AsyncPage.SetCheckedAsync(selector, checkedState, options).Wait();
        }

        /// <inheritdoc cref = "IPage.SetContentAsync" /> 
        public void SetContent(string html, PageSetContentOptions? options = null)
        {
            this.AsyncPage.SetContentAsync(html, options).Wait();
        }

        /// <inheritdoc cref = "IPage.SetExtraHTTPHeadersAsync" /> 
        public void SetExtraHTTPHeaders(IEnumerable<KeyValuePair<string, string>> headers)
        {
            this.AsyncPage.SetExtraHTTPHeadersAsync(headers).Wait();
        }

        /// <inheritdoc cref = "IPage.SetInputFilesAsync(string, FilePayload, PageSetInputFilesOptions)" /> 
        public void SetInputFiles(string selector, FilePayload files, PageSetInputFilesOptions? options = null)
        {
            this.AsyncPage.SetInputFilesAsync(selector, files, options).Wait();
        }

        /// <inheritdoc cref = "IPage.SetInputFilesAsync(string, IEnumerable{FilePayload}, PageSetInputFilesOptions)" /> 
        public void SetInputFiles(string selector, IEnumerable<FilePayload> files, PageSetInputFilesOptions? options = null)
        {
            this.AsyncPage.SetInputFilesAsync(selector, files, options).Wait();
        }

        /// <inheritdoc cref = "IPage.SetInputFilesAsync(string, IEnumerable{string}, PageSetInputFilesOptions)" /> 
        public void SetInputFiles(string selector, IEnumerable<string> files, PageSetInputFilesOptions? options = null)
        {
            this.AsyncPage.SetInputFilesAsync(selector, files, options).Wait();
        }

        /// <inheritdoc cref = "IPage.SetInputFilesAsync(string, string, PageSetInputFilesOptions)" /> 
        public void SetInputFiles(string selector, string files, PageSetInputFilesOptions? options = null)
        {
            this.AsyncPage.SetInputFilesAsync(selector, files, options).Wait();
        }

        /// <inheritdoc cref = "IPage.SetViewportSizeAsync" /> 
        public void SetViewportSize(int width, int height)
        {
            this.AsyncPage.SetViewportSizeAsync(width, height).Wait();
        }

        /// <inheritdoc cref = "IPage.TapAsync" /> 
        public void Tap(string selector, PageTapOptions? options = null)
        {
            this.AsyncPage.TapAsync(selector, options).Wait();
        }

        /// <inheritdoc cref = "IPage.TypeAsync" /> 
        public void Type(string selector, string text, PageTypeOptions? options = null)
        {
            this.AsyncPage.TypeAsync(selector, text, options).Wait();
        }

        /// <inheritdoc cref = "IPage.UncheckAsync" /> 
        public void Uncheck(string selector, PageUncheckOptions? options = null)
        {
            this.AsyncPage.UncheckAsync(selector, options).Wait();
        }

        /// <inheritdoc cref = "IPage.WaitForLoadStateAsync" /> 
        public void WaitForLoadState(LoadState? state = null, PageWaitForLoadStateOptions? options = null)
        {
            this.AsyncPage.WaitForLoadStateAsync(state, options).Wait();
        }

        /// <inheritdoc cref = "IPage.WaitForTimeoutAsync" /> 
        public void WaitForTimeout(float timeout)
        {
            this.AsyncPage.WaitForTimeoutAsync(timeout).Wait();
        }

        /// <inheritdoc cref = "IPage.WaitForURLAsync(Func{string, bool}, PageWaitForURLOptions)" /> 
        public void WaitForURL(Func<string, bool> url, PageWaitForURLOptions? options = null)
        {
            this.AsyncPage.WaitForURLAsync(url, options).Wait();
        }

        /// <inheritdoc cref = "IPage.WaitForURLAsync(Regex, PageWaitForURLOptions)" /> 
        public void WaitForURL(Regex url, PageWaitForURLOptions? options = null)
        {
            this.AsyncPage.WaitForURLAsync(url, options).Wait();
        }

        /// <inheritdoc cref = "IPage.WaitForURLAsync(string, PageWaitForURLOptions)" /> 
        public void WaitForURL(string url, PageWaitForURLOptions? options = null)
        {
            this.AsyncPage.WaitForURLAsync(url, options).Wait();
        }

        /// <inheritdoc cref = "IPage.IsCheckedAsync"  />
        public bool IsChecked(string selector, PageIsCheckedOptions? options = null)
        {
            return this.AsyncPage.IsCheckedAsync(selector, options).Result;
        }

        /// <inheritdoc cref = "IPage.IsDisabledAsync"  />
        public bool IsDisabled(string selector, PageIsDisabledOptions? options = null)
        {
            return this.AsyncPage.IsDisabledAsync(selector, options).Result;
        }

        /// <inheritdoc cref = "IPage.IsEditableAsync"  />
        public bool IsEditable(string selector, PageIsEditableOptions? options = null)
        {
            return this.AsyncPage.IsEditableAsync(selector, options).Result;
        }

        /// <inheritdoc cref = "IPage.IsEnabledAsync"  />
        public bool IsEnabled(string selector, PageIsEnabledOptions? options = null)
        {
            return this.AsyncPage.IsEnabledAsync(selector, options).Result;
        }

        /// <inheritdoc cref = "IPage.IsHiddenAsync"  />
        public bool IsHidden(string selector, PageIsHiddenOptions? options = null)
        {
            return this.AsyncPage.IsHiddenAsync(selector, options).Result;
        }

        /// <inheritdoc cref = "IPage.IsVisibleAsync"  />
        public bool IsVisible(string selector, PageIsVisibleOptions? options = null)
        {
            return this.AsyncPage.IsVisibleAsync(selector, options).Result;
        }

        /// <summary>
        /// Check that the element is eventually visible
        /// </summary>
        /// <param name="selector">Element selector</param>
        /// <param name="options">Visible check options</param>
        /// <returns>True if the element becomes visible within the page timeout</returns>
        public bool IsEventualyVisible(string selector, PageIsVisibleOptions? options = null)
        {
            try
            {
                _ = this.AsyncPage.WaitForSelectorAsync(selector, new PageWaitForSelectorOptions
                {
                    State = WaitForSelectorState.Visible,
                    Strict = options == null ?  false : options.Strict
                }).Result;
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
        /// <param name="selector">Element selector</param>
        /// <param name="options">Visible check options</param>
        /// <returns>True if the element becomes is hidden or gone within the page timeout</returns>
        public bool IsEventualyGone(string selector, PageIsVisibleOptions? options = null)
        {
            try
            {
                _ = this.AsyncPage.WaitForSelectorAsync(selector, new PageWaitForSelectorOptions
                {
                    State = WaitForSelectorState.Hidden,
                    Strict = options == null ? false : options.Strict
                }).Result;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc cref = "IPage.QuerySelectorAsync"  />
        public IElementHandle? QuerySelector(string selector, PageQuerySelectorOptions? options = null)
        {
            return this.AsyncPage.QuerySelectorAsync(selector, options).Result;
        }

        /// <inheritdoc cref = "IPage.WaitForSelectorAsync"  />
        public IElementHandle? WaitForSelector(string selector, PageWaitForSelectorOptions? options = null)
        {
            return this.AsyncPage.WaitForSelectorAsync(selector, options).Result;
        }

        /// <inheritdoc cref = "IPage.AddScriptTagAsync"  />
        public IElementHandle AddScriptTag(PageAddScriptTagOptions? options = null)
        {
            return this.AsyncPage.AddScriptTagAsync(options).Result;
        }

        /// <inheritdoc cref = "IPage.AddStyleTagAsync"  />
        public IElementHandle AddStyleTag(PageAddStyleTagOptions? options = null)
        {
            return this.AsyncPage.AddStyleTagAsync(options).Result;
        }

        /// <inheritdoc cref = "IPage.QuerySelectorAllAsync"  />
        public IReadOnlyList<IElementHandle> QuerySelectorAll(string selector)
        {
            return this.AsyncPage.QuerySelectorAllAsync(selector).Result;
        }

        /// <inheritdoc cref = "IPage.SelectOptionAsync(string, IElementHandle, PageSelectOptionOptions)"  />
        public IReadOnlyList<string> SelectOption(string selector, IElementHandle values, PageSelectOptionOptions? options = null)
        {
            return this.AsyncPage.SelectOptionAsync(selector, values, options).Result;
        }

        /// <inheritdoc cref = "IPage.SelectOptionAsync(string, IEnumerable{IElementHandle}, PageSelectOptionOptions)"  />
        public IReadOnlyList<string> SelectOption(string selector, IEnumerable<IElementHandle> values, PageSelectOptionOptions? options = null)
        {
            return this.AsyncPage.SelectOptionAsync(selector, values, options).Result;
        }

        /// <inheritdoc cref = "IPage.SelectOptionAsync(string, IEnumerable{SelectOptionValue}, PageSelectOptionOptions)"  />
        public IReadOnlyList<string> SelectOption(string selector, IEnumerable<SelectOptionValue> values, PageSelectOptionOptions? options = null)
        {
            return this.AsyncPage.SelectOptionAsync(selector, values, options).Result;
        }

        /// <inheritdoc cref = "IPage.SelectOptionAsync(string, IEnumerable{string}, PageSelectOptionOptions)"  />
        public IReadOnlyList<string> SelectOption(string selector, IEnumerable<string> values, PageSelectOptionOptions? options = null)
        {
            return this.AsyncPage.SelectOptionAsync(selector, values, options).Result;
        }

        /// <inheritdoc cref = "IPage.SelectOptionAsync(string, SelectOptionValue, PageSelectOptionOptions)"  />
        public IReadOnlyList<string> SelectOption(string selector, SelectOptionValue values, PageSelectOptionOptions? options = null)
        {
            return this.AsyncPage.SelectOptionAsync(selector, values, options).Result;
        }

        /// <inheritdoc cref = "IPage.SelectOptionAsync(string, string, PageSelectOptionOptions)"  />
        public IReadOnlyList<string> SelectOption(string selector, string values, PageSelectOptionOptions? options = null)
        {
            return this.AsyncPage.SelectOptionAsync(selector, values, options).Result;
        }

        /// <inheritdoc cref = "IPage.EvalOnSelectorAllAsync"  />
        public JsonElement? EvalOnSelectorAll(string selector, string expression, object? arg = null)
        {
            return this.AsyncPage.EvalOnSelectorAllAsync(selector, expression, arg).Result;
        }

        /// <inheritdoc cref = "IPage.EvalOnSelectorAsync"  />
        public JsonElement? EvalOnSelector(string selector, string expression, object? arg = null)
        {
            return this.AsyncPage.EvalOnSelectorAsync(selector, expression, arg).Result;
        }

        /// <inheritdoc cref = "IPage.EvaluateAsync"  />
        public JsonElement? Evaluate(string expression, object? arg = null)
        {
            return this.AsyncPage.EvaluateAsync(expression, arg).Result;
        }

        /// <inheritdoc cref = "IPage.GetAttributeAsync"  />
        public string? GetAttribute(string selector, string name, PageGetAttributeOptions? options = null)
        {
            return this.AsyncPage.GetAttributeAsync(selector, name, options).Result;
        }

        /// <inheritdoc cref = "IPage.TextContentAsync"  />
        public string? TextContent(string selector, PageTextContentOptions? options = null)
        {
            return this.AsyncPage.TextContentAsync(selector, options).Result;
        }

        /// <inheritdoc cref = "IPage.ContentAsync"  />
        public string Content()
        {
            return this.AsyncPage.ContentAsync().Result;
        }

        /// <inheritdoc cref = "IPage.InnerHTMLAsync"  />
        public string InnerHTML(string selector, PageInnerHTMLOptions? options = null)
        {
            return this.AsyncPage.InnerHTMLAsync(selector, options).Result;
        }

        /// <inheritdoc cref = "IPage.InnerTextAsync"  />
        public string InnerText(string selector, PageInnerTextOptions? options = null)
        {
            return this.AsyncPage.InnerTextAsync(selector, options).Result;
        }

        /// <inheritdoc cref = "IPage.InputValueAsync"  />
        public string InputValue(string selector, PageInputValueOptions? options = null)
        {
            return this.AsyncPage.InputValueAsync(selector, options).Result;
        }

        /// <inheritdoc cref = "IPage.TitleAsync"  />
        public string Title()
        {
            return this.AsyncPage.TitleAsync().Result;
        }

        /// <inheritdoc cref = "IPage.GotoAsync"  />
        public IResponse? Goto(string url, PageGotoOptions? options = null)
        {
            return this.AsyncPage.GotoAsync(url,options).Result;
        }

        /// <inheritdoc cref = "IPage.GoBackAsync"  />
        public IResponse? GoBack(PageGoBackOptions? options = null)
        {
            return this.AsyncPage.GoBackAsync(options).Result;
        }

        /// <inheritdoc cref = "IPage.GoForwardAsync"  />
        public IResponse? GoForward(PageGoForwardOptions? options = null)
        {
            return this.AsyncPage.GoForwardAsync(options).Result;
        }

        /// <inheritdoc cref = "IPage.ReloadAsync"  />
        public IResponse? Reload(PageReloadOptions? options = null)
        {
            return this.AsyncPage.ReloadAsync(options).Result;
        }

        /// <summary>
        /// Dispose of the database connection
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose of the database connection
        /// </summary>
        /// <param name="disposing">Is the object being disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            // Make sure the connection exists and is open before trying to close it
            if (disposing && !this.AsyncPage.IsClosed)
            {
                this.AsyncPage.CloseAsync();
            }
        }
    }
}
