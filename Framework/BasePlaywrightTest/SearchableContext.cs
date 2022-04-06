using Microsoft.Playwright;
using System.Collections.Generic;
using System.Text.Json;

namespace CognizantSoftvision.Maqs.BasePlaywrightTest
{
    public class SearchableContext<T> where T : IPage, ILocator
    {
        /// <summary>
        /// Gets the underlying async page object
        /// </summary>
        public T SearchConext { get; private set; }

        public SearchableContext(T context)
        {
            this.SearchConext = context;
        }
    }
}
