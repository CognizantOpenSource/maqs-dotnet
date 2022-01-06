//--------------------------------------------------
// <copyright file="MongoDriverFailureTests.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Mongo database driver failure tests</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseMongoTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using System;
using System.Diagnostics.CodeAnalysis;

namespace FrameworkUnitTests
{
    /// <summary>
    /// Test the Mongo driver store
    /// </summary>
    [TestClass]
    [TestCategory(TestCategories.MongoDB)]
    [ExcludeFromCodeCoverage]
    public class MongoDriverFailureTests : BaseMongoTest<BsonDocument>
    {
        /// <summary>
        /// Test driver call fails correctly
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(TimeoutException))]
        public void ListAllCollectionItemsFailure()
        {
            this.MongoDBDriver.ListAllCollectionItems();
        }

        /// <summary>
        /// Test driver call fails correctly
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(TimeoutException))]
        public void IsCollectionEmptyFailure()
        {
            this.MongoDBDriver.IsCollectionEmpty();
        }

        /// <summary>
        /// Test driver call fails correctly
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(TimeoutException))]
        public void CountAllItemsInCollectionFailure()
        {
            this.MongoDBDriver.CountAllItemsInCollection();
        }


    }
}
