﻿//--------------------------------------------------
// <copyright file="DatabaseConfig.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Helper class for getting database specific configuration values</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseDatabaseTest.Providers;
using CognizantSoftvision.Maqs.Utilities.Helper;
using System.Collections.Generic;
using System.Data;

namespace CognizantSoftvision.Maqs.BaseDatabaseTest
{
    /// <summary>
    /// Config class
    /// </summary>
    public static class DatabaseConfig
    {
        /// <summary>
        /// Loads when class is loaded
        /// </summary>
        static DatabaseConfig()
        {
            CheckConfig();
        }

        /// <summary>
        /// Ensure required fields are in the config
        /// </summary>
        private static void CheckConfig()
        {
            var validator = new ConfigValidation()
            {
                RequiredFields = new List<string>()
                {
                    "DataBaseConnectionString",
                    "DataBaseProviderType"
                }
            };
            Config.Validate(ConfigSection.DatabaseMaqs, validator);
        }

        /// <summary>
        ///  Static name for the database configuration section
        /// </summary>
        private const string DATABASESECTIION = "DatabaseMaqs";

        /// <summary>
        /// Get the database connection string
        /// </summary>
        /// <returns>The connection string</returns>
        public static string GetConnectionString()
        {
            return Config.GetValueForSection(DATABASESECTIION, "DataBaseConnectionString");
        }

        /// <summary>
        /// Get the database provider type string
        /// </summary>
        /// <returns>The provider type string</returns>
        public static string GetProviderTypeString()
        {
            return Config.GetValueForSection(DATABASESECTIION, "DataBaseProviderType");
        }

        /// <summary>
        /// Gets the database connection based on configuration values
        /// </summary>
        /// <typeparam name="T"> The type of connection client</typeparam>
        /// <param name="provider"> The custom provider.  </param>
        /// <param name="connectionString"> The connection String.  </param>
        /// <returns> The database connection client </returns>
        public static IDbConnection GetOpenConnection<T>(IProvider<T> provider, string connectionString = "") where T : class
        {
            return ConnectionFactory.GetOpenConnection(provider, connectionString);
        }

        /// <summary>
        /// Gets the database connection based on configuration values
        /// </summary>
        /// <returns>The database connection</returns>
        public static IDbConnection GetOpenConnection()
        {
            return ConnectionFactory.GetOpenConnection(GetProviderTypeString(), GetConnectionString());
        }

        /// <summary>
        /// Gets the database connection
        /// </summary>
        /// <param name="providerType"> The provider Type to create. </param>
        /// <param name="connectionString"> The connection String. </param>
        /// <returns> The database connection </returns>
        public static IDbConnection GetOpenConnection(string providerType, string connectionString)
        {
            return ConnectionFactory.GetOpenConnection(providerType, connectionString);
        }
    }
}
