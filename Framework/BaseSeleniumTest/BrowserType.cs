﻿//--------------------------------------------------
// <copyright file="BrowserType.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Known browser types</summary>
//--------------------------------------------------
namespace CognizantSoftvision.Maqs.BaseSeleniumTest
{
    /// <summary>
    /// Known browser types
    /// </summary>
    public enum BrowserType
    {
        /// <summary>
        /// Chrome web browser
        /// </summary>
        Chrome,

        /// <summary>
        /// Edge web browser
        /// </summary>
        Edge,

        /// <summary>
        /// Firefox web browser
        /// </summary>
        Firefox,

        /// <summary>
        /// Chrome web browser - run headless
        /// </summary>
        HeadlessChrome,

        /// <summary>
        /// IE web browser
        /// </summary>
        IE,

        /// <summary>
        /// Remote web browser - Used when executing on Grid or cloud based provides like Sauce Labs
        /// </summary>
        Remote
    }
}
