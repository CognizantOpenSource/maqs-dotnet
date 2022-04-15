//--------------------------------------------------
// <copyright file="BaseTestObject.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Holds base context data</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.Utilities.Logging;
using CognizantSoftvision.Maqs.Utilities.Performance;
using System;
using System.Collections.Generic;
using System.IO;

namespace CognizantSoftvision.Maqs.BaseTest
{
    /// <summary>
    /// Base test context data
    /// </summary>
    public class BaseTestObject : ITestObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestObject" /> class
        /// </summary>
        public BaseTestObject(ITestObject baseTestObject)
        {
            this.Log = baseTestObject.Log;
            this.SoftAssert = baseTestObject.SoftAssert;
            this.PerfTimerCollection = baseTestObject.PerfTimerCollection;
            this.Values = baseTestObject.Values;
            this.Objects = baseTestObject.Objects;
            this.ManagerStore = baseTestObject.ManagerStore;
            this.AssociatedFiles = baseTestObject.AssociatedFiles;

            baseTestObject.Log.LogMessage(MessageType.INFORMATION, "Setup test object " + this.PerfTimerCollection.TestName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestObject" /> class
        /// </summary>
        /// <param name="logger">The test's logger</param>
        /// <param name="fullyQualifiedTestName">The test's fully qualified name</param>
        public BaseTestObject(ILogger logger, string fullyQualifiedTestName) : this(logger, new SoftAssert(logger), new PerfTimerCollection(logger, fullyQualifiedTestName), fullyQualifiedTestName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestObject" /> class
        /// </summary>
        /// <param name="logger">The test's logger</param>
        /// <param name="softAssert">The test's soft assert</param>
        /// <param name="collection">The test's performance timer collection</param>
        /// <param name="fullyQualifiedTestName">The test's fully qualified test name</param>
        public BaseTestObject(ILogger logger, ISoftAssert softAssert, IPerfTimerCollection collection, string fullyQualifiedTestName)
        {
            this.Log = logger;
            this.SoftAssert = softAssert;
            this.PerfTimerCollection = collection;
            this.Values = new Dictionary<string, string>();
            this.Objects = new Dictionary<string, object>();
            this.ManagerStore = new ManagerStore();
            this.AssociatedFiles = new HashSet<string>();

            logger.LogMessage(MessageType.INFORMATION, "Setup test object for " + fullyQualifiedTestName);
        }

        /// <inheritdoc /> 
        public ILogger Log { get; set; }

        /// <inheritdoc /> 
        public IPerfTimerCollection PerfTimerCollection { get; set; }

        /// <inheritdoc /> 
        public ISoftAssert SoftAssert { get; set; }

        /// <inheritdoc /> 
        public Dictionary<string, string> Values { get; private set; }

        /// <inheritdoc /> 
        public HashSet<string> AssociatedFiles { get; private set; }

        /// <inheritdoc /> 
        public Dictionary<string, object> Objects { get; private set; }

        /// <inheritdoc /> 
        public IManagerStore ManagerStore { get; private set; }

        /// <inheritdoc /> 
        public void SetValue(string key, string value)
        {
            if (this.Values.ContainsKey(key))
            {
                this.Values[key] = value;
            }
            else
            {
                this.Values.Add(key, value);
            }
        }

        /// <inheritdoc /> 
        public void SetObject(string key, object value)
        {
            if (this.Objects.ContainsKey(key))
            {
                this.Objects[key] = value;
            }
            else
            {
                this.Objects.Add(key, value);
            }
        }

        /// <inheritdoc /> 
        public T GetDriverManager<T>() where T : IDriverManager
        {
            return this.ManagerStore.GetManager<T>();
        }

        /// <inheritdoc /> 
        public void AddDriverManager<T>(T manager, bool overrideIfExists = false) where T : IDriverManager
        {
            if (overrideIfExists)
            {
                this.OverrideDriverManager(manager);
            }
            else
            {
                this.AddDriverManager(typeof(T).FullName, manager);
            }
        }

        /// <inheritdoc /> 
        public void AddDriverManager(string key, IDriverManager manager)
        {
            this.ManagerStore.Add(key, manager);
        }

        /// <inheritdoc /> 
        public bool AddAssociatedFile(string path)
        {
            if (File.Exists(path))
            {
                return this.AssociatedFiles.Add(path);
            }

            return false;
        }

        /// <inheritdoc /> 
        public bool RemoveAssociatedFile(string path)
        {
            return this.AssociatedFiles.Remove(path);
        }

        /// <inheritdoc /> 
        public string[] GetArrayOfAssociatedFiles()
        {
            string[] associatedFiles = new string[this.AssociatedFiles.Count];
            this.AssociatedFiles.CopyTo(associatedFiles, 0);
            return associatedFiles;
        }

        /// <inheritdoc /> 
        public bool ContainsAssociatedFile(string path)
        {
            return this.AssociatedFiles.Contains(path);
        }

        /// <inheritdoc /> 
        public void OverrideDriverManager<T>(T manager) where T : IDriverManager
        {
            this.ManagerStore.AddOrOverride(manager);
        }

        /// <summary>
        /// Dispose the of the driver store
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose the of the driver store
        /// </summary>
        /// <param name="disposing">True if you want to release managed resources</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || this.ManagerStore is null)
            {
                return;
            }

            this.Log.LogMessage(MessageType.VERBOSE, "Start dispose");

            this.ManagerStore.Dispose();

            this.Log.LogMessage(MessageType.VERBOSE, "End dispose");
        }
    }
}
