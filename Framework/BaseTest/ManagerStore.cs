//--------------------------------------------------
// <copyright file="ManagerDictionary.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Dictionary for handling driver managers</summary>
//--------------------------------------------------
using System;
using System.Collections.Generic;

namespace CognizantSoftvision.Maqs.BaseTest
{
    /// <summary>
    /// Driver manager dictionary
    /// </summary>
    public class ManagerStore : IManagerStore
    {
        /// <summary>
        /// Dictionary to hold all driver managers
        /// </summary>
        private readonly Dictionary<string, IDriverManager> managerDictionary = new Dictionary<string, IDriverManager>();

        /// <inheritdoc /> 
        public int Count => managerDictionary.Count;

        /// <inheritdoc /> 
        public T GetDriver<T>(string key)
        {
            return (T)managerDictionary[key].Get();
        }

        /// <inheritdoc /> 
        public T GetDriver<T, U>() where U : IDriverManager
        {
            return (T)GetManager<U>().Get();
        }

        /// <inheritdoc /> 
        public IDriverManager GetManager(string key)
        {
            return GetManager<IDriverManager>(key);
        }

        /// <inheritdoc /> 
        public T GetManager<T>() where T : IDriverManager
        {
            return (T)managerDictionary[typeof(T).FullName];
        }

        /// <summary>
        /// Get keyed manager of type 'T'
        /// </summary>
        /// <typeparam name="T">The type of driver manager</typeparam>
        /// <returns>Keyed manager as type 'T'</returns>
        public T GetManager<T>(string key) where T : IDriverManager
        {
            return (T)managerDictionary[key];
        }

        /// <inheritdoc /> 
        public void Add(IDriverManager manager)
        {
            managerDictionary.Add(manager.GetType().FullName, manager);
        }

        /// <inheritdoc /> 
        public void Add(string key, IDriverManager manager)
        {
            managerDictionary.Add(key, manager);
        }

        /// <inheritdoc /> 
        public void AddOrOverride(IDriverManager manager)
        {
            this.AddOrOverride(manager.GetType().FullName, manager);
        }

        /// <inheritdoc /> 
        public void AddOrOverride(string key, IDriverManager manager)
        {
            this.Remove(key);
            managerDictionary.Add(key, manager);
        }

        /// <inheritdoc /> 
        public bool Remove(string key)
        {
            if (managerDictionary.ContainsKey(key) && managerDictionary[key] != null)
            {
                managerDictionary[key].Dispose();
            }

            return managerDictionary.Remove(key);
        }

        /// <inheritdoc /> 
        public bool Remove<T>() where T : IDriverManager
        {
            return this.Remove(typeof(T).FullName);
        }

        /// <summary>
        /// Clear the dictionary
        /// </summary>
        public void Clear()
        {
            foreach (KeyValuePair<string, IDriverManager> driver in managerDictionary)
            {
                driver.Value.Dispose();
            }

            managerDictionary.Clear();
        }

        /// <summary>
        /// Cleanup the driver
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Check if the managers contains the keyed manager
        /// </summary>
        /// <param name="key">Key name</param>
        /// <returns>True if the store contains the key</returns>
        public bool Contains(string key)
        {
            return managerDictionary.ContainsKey(key);
        }

        /// <summary>
        /// Check if the managers contains the unkeyed manager of the the given type
        /// </summary>
        /// <typeparam name="T">Manager type</typeparam>
        /// <returns>True if the store contains the unkeyed type</returns>
        public bool Contains<T>() where T : IDriverManager
        {
            return this.Contains(typeof(T).FullName);
        }

        /// <summary>
        ///  Gets the value associated with the specified key
        /// </summary>
        /// <typeparam name="T">Driver type</typeparam>
        /// <param name="key">Key name</param>
        /// <param name="driver">Driver to return</param>
        /// <returns>True if it finds the driver based on key</returns>
        public bool TryGetDriver<T>(string key, out T driver)
        {
            if (Contains(key))
            {
                driver = GetDriver<T>(key);
                return true;
            }
            driver = default;
            return false;
        }

        /// <summary>
        ///  Gets the value associated with the specified key
        /// </summary>
        /// <typeparam name="T">Driver type</typeparam>
        /// <param name="key">Key name</param>
        /// <param name="driver">Driver to return</param>
        /// <returns>True if it finds the driver based on key</returns>
        public bool TryGetManager<T>(string key, out T driver) where T : IDriverManager
        {
            if (Contains(key))
            {
                driver = GetManager<T>(key);
                return true;
            }
            driver = default;
            return false;
        }

        /// <summary>
        /// Cleanup the driver
        /// </summary>
        /// <param name="disposing">Dispose managed objects</param>
        protected virtual void Dispose(bool disposing)
        {
            // Only dealing with managed objects
            if (disposing)
            {
                // Make sure all of the individual drivers are disposed
                foreach (IDriverManager singleDrive in managerDictionary.Values)
                {
                    singleDrive?.Dispose();
                }

                managerDictionary.Clear();
            }
        }
    }
}