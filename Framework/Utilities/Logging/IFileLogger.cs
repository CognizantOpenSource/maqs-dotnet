//--------------------------------------------------
// <copyright file="IFileLogger.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>File logger interface</summary>
//--------------------------------------------------
namespace CognizantSoftvision.Maqs.Utilities.Logging
{
    /// <summary>
    /// Interface for file logger
    /// </summary>
    public interface IFileLogger : ILogger
    {
        /// <summary>
        /// Gets or sets path to the log file
        /// </summary>
        string FilePath { get; set; }
    }
}