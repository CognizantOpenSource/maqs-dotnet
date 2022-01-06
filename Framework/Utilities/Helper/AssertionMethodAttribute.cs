//--------------------------------------------------
// <copyright file="SoftAssert.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>This is the SoftAssert class</summary>
//--------------------------------------------------
using System;

namespace CognizantSoftvision.Maqs.Utilities.Helper
{
    /// <summary>
    /// SonarLink 2699 Tests should include assertions
    /// Used for SoftAsserts
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class AssertionMethodAttribute : Attribute
    {
    }
}