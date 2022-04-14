//--------------------------------------------------
// <copyright file="StringProcessor.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Help utilities for string processing</summary>
//--------------------------------------------------
using System;
using System.Text;

namespace CognizantSoftvision.Maqs.Utilities.Helper
{
    /// <summary>
    /// Initializes a new instance of the StringProcessor class
    /// </summary>
    public static class StringProcessor
    {
        /// <summary>
        /// Creates a string based on the arguments
        /// If no args are applied, then we want to just return the message
        /// </summary>
        /// <param name="message">The message being used</param>
        /// <param name="args">The arguments being used</param>
        /// <returns>A final string</returns>
        public static string SafeFormatter(string message, params object[] args)
        {
            try
            {
                return string.Format(message, args);
            }
            catch (Exception)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(message);
                foreach (var arg in args)
                {
                    builder.AppendLine(arg.ToString());
                }

                return builder.ToString();
            }
        }

        /// <summary>
        /// Gets a string of a nested exception list
        /// </summary>
        /// <param name="e">Exception to print as string</param>
        /// <returns>A string of the Exceptions with stack trace</returns>
        public static string SafeExceptionFormatter(Exception e)
        {
            StringBuilder sb = new StringBuilder();
            return GetException(e, sb);
        }

        /// <summary>
        /// Get the window size as a string
        /// </summary>
        /// <param name="size">The size in a #X# format</param>
        /// <param name="width">The browser width</param>
        /// <param name="height">The browser height</param>
        public static void ExtractSizeFromString(string size, out int width, out int height)
        {
            string[] sizes = size.Split('X');

            if (!size.Contains("X") || sizes.Length != 2)
            {
                throw new ArgumentException("Browser size is expected to be in an expected format: 1280x720");
            }

            if (!int.TryParse(sizes[0], out width) || !int.TryParse(sizes[1], out height))
            {
                throw new InvalidCastException("Length and Width must be a string that is an integer value: 400x400");
            }
        }

        /// <summary>
        /// Recursive function to grab the inner exceptions
        /// </summary>
        /// <param name="ex">Exception to look into</param>
        /// <param name="sb">String builder to build the string</param>
        /// <param name="level">Recursive level for spacing of logs</param>
        /// <returns>A string with the exceptions</returns>
        private static string GetException(Exception ex, StringBuilder sb, int level = 0)
        {
            string spaces = new string(' ', level);
            sb.Append($"{Environment.NewLine}{spaces}{ex.Message}{(ex.StackTrace == null ? "" : $"{Environment.NewLine}{spaces}{ex.StackTrace}")}");
            if (ex is AggregateException && (ex as AggregateException).InnerExceptions.Count > 0)
            {
                foreach (var exception in (ex as AggregateException).InnerExceptions)
                {
                    GetException(exception, sb, level + 1);
                }
            }
            else if (ex.InnerException != null)
            {
                GetException(ex.InnerException, sb, level + 2);
            }

            return sb.ToString();
        }
    }
}
