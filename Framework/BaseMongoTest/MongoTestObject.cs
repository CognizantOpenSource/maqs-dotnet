﻿//--------------------------------------------------
// <copyright file="MongoTestObject.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Holds MongoDB context data</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseTest;
using CognizantSoftvision.Maqs.Utilities.Logging;
using MongoDB.Driver;
using System;

namespace CognizantSoftvision.Maqs.BaseMongoTest
{
    /// <summary>
    /// Mongo test context data
    /// </summary>
    /// <typeparam name="T">The Mongo collection type</typeparam>
    public class MongoTestObject<T> : BaseTestObject, IMongoTestObject<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoTestObject{T}" /> class
        /// </summary>
        /// <param name="connectionString">Client connection string</param>
        /// <param name="databaseString">Database connection string</param>
        /// <param name="collectionString">Mongo collection string</param>
        /// <param name="logger">The test's logger</param>
        /// <param name="fullyQualifiedTestName">The test's fully qualified test name</param>
        public MongoTestObject(string connectionString, string databaseString, string collectionString, ILogger logger, string fullyQualifiedTestName) : base(logger, fullyQualifiedTestName)
        {
            this.ManagerStore.Add(typeof(MongoDriverManager<T>).FullName, new MongoDriverManager<T>(connectionString, databaseString, collectionString, this));
        }

        /// <summary>
        /// Gets the Mongo driver manager
        /// </summary>
        public MongoDriverManager<T> MongoDBManager
        {
            get
            {
                return this.ManagerStore.GetManager<MongoDriverManager<T>>();
            }
        }

        /// <summary>
        /// Gets the Mongo driver
        /// </summary>
        public MongoDBDriver<T> MongoDBDriver
        {
            get
            {
                return this.MongoDBManager.GetMongoDriver();
            }
        }

        /// <summary>
        /// Override the Mongo driver settings
        /// </summary>
        /// <param name="connectionString">Client connection string</param>
        /// <param name="databaseString">Database connection string</param>
        /// <param name="collectionString">Mongo collection string</param>
        public void OverrideMongoDBDriver(string connectionString, string databaseString, string collectionString)
        {
            this.MongoDBManager.OverrideDriver(connectionString, databaseString, collectionString);
        }

        /// <summary>
        /// Override the Mongo driver settings
        /// </summary>
        /// <param name="driver">New Mongo driver</param>
        public void OverrideMongoDBDriver(MongoDBDriver<T> driver)
        {
            this.MongoDBManager.OverrideDriver(driver);
        }

        /// <summary>
        /// Override the Mongo driver a collection function
        /// </summary>
        /// <param name="overrideCollectionConnection">The collection function</param>
        public void OverrideMongoDBDriver(Func<IMongoCollection<T>> overrideCollectionConnection)
        {
            this.MongoDBManager.OverrideDriver(overrideCollectionConnection);
        }
    }
}
