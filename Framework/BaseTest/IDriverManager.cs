//--------------------------------------------------
// <copyright file="IDriverManager.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Base driver manager interface</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.Utilities.Logging;
using System;

namespace CognizantSoftvision.Maqs.BaseTest
{
    /// <summary>
    /// Interface for base driver manager
    /// </summary>
    public interface IDriverManager : IDisposable
    {
        /// <summary>
        /// Gets the testing object
        /// </summary>
        ILogger Log { get; }

        /// <summary>
        /// Get the driver
        /// </summary>
        /// <returns>The driver</returns>
        object Get();

        /// <summary>
        /// Check if the underlying driver has been initialized
        /// </summary>
        /// <returns>True if the underlying driver has already been initialized</returns>
        bool IsDriverIntialized();
    }
}