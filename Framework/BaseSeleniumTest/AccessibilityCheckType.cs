//--------------------------------------------------
// <copyright file="AccessibilityCheckType.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Accessibility check types</summary>
//--------------------------------------------------
namespace CognizantSoftvision.Maqs.BaseSeleniumTest
{
    /// <summary>
    /// Known browser types
    /// </summary>
    public enum AccessibilityCheckType
    {
        /// <summary>
        /// Check for violations
        /// </summary>
        Violations,

        /// <summary>
        /// Check for passing
        /// </summary>
        Passes,

        /// <summary>
        /// Check for inapplicable
        /// </summary>
        Inapplicable,

        /// <summary>
        /// Check for incomplete
        /// </summary>
        Incomplete
    }
}
