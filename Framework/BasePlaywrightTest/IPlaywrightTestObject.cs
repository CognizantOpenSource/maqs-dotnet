//--------------------------------------------------
// <copyright file="IPlaywrightTestObject.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Holds Playwright test object interface</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseTest;
using Microsoft.Playwright;
using System;

namespace CognizantSoftvision.Maqs.BasePlaywrightTest
{
    /// <summary>
    /// Playwright test object interface
    /// </summary>
    public interface IPlaywrightTestObject : ITestObject
    {
        /// <summary>
        /// Gets the Playwright page driver
        /// </summary>
        PageDriver PageDriver { get; }

        /// <summary>
        /// Gets the Playwright page manager
        /// </summary>
        PlaywrightDriverManager PageManager { get; }

        /// <summary>
        /// Override the function for creating a Playwright page
        /// </summary>
        /// <param name="getPage">Function for creating a page</param>
        void OverridePageDriver(Func<IPage> getPage);

        /// <summary>
        /// Override the Playwright page
        /// </summary>
        /// <param name="page">New page</param>
        void OverridePageDriver(IPage page);

        /// <summary>
        /// Override the Playwright page driver
        /// </summary>
        /// <param name="pageDriver">New page driver</param>
        void OverridePageDriver(PageDriver pageDriver);
    }
}