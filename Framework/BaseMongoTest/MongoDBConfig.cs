﻿//--------------------------------------------------
// <copyright file="MongoDBConfig.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Helper class for getting MongoDB specific configuration values</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.Utilities.Helper;
using System.Collections.Generic;

namespace CognizantSoftvision.Maqs.BaseMongoTest
{
    /// <summary>
    /// Config class
    /// </summary>
    public static class MongoDBConfig
    {
        /// <summary>
        /// Loads when class is loaded
        /// </summary>
        static MongoDBConfig()
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
                    "MongoConnectionString",
                    "MongoDatabase",
                    "MongoCollection"
                }
            };
            Config.Validate(ConfigSection.MongoMaqs, validator);
        }

        /// <summary>
        ///  Static name for the mongo configuration section
        /// </summary>
        private const string MONGOSECTION = "MongoMaqs";

        /// <summary>
        /// Get the client connection string
        /// </summary>
        /// <returns>The connection type</returns>
        public static string GetConnectionString()
        {
            return Config.GetValueForSection(MONGOSECTION, "MongoConnectionString");
        }

        /// <summary>
        /// Get the database connection string
        /// </summary>
        /// <returns>The database name</returns>
        public static string GetDatabaseString()
        {
            return Config.GetValueForSection(MONGOSECTION, "MongoDatabase");
        }

        /// <summary>
        /// Get the mongo collection string
        /// </summary>
        /// <returns>The mongo collection string</returns>
        public static string GetCollectionString()
        {
            return Config.GetValueForSection(MONGOSECTION, "MongoCollection");
        }

        /// <summary>
        /// Get the database timeout in seconds
        /// </summary>
        /// <returns>The timeout in seconds from the config file or default of 30 seconds when no app.config key is found</returns>
        public static int GetQueryTimeout()
        {
            return int.Parse(Config.GetValueForSection(MONGOSECTION, "MongoTimeout", "30"));
        }
    }
}