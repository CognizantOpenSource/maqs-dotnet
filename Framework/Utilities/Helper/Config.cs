﻿//--------------------------------------------------
// <copyright file="Config.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Helper class for getting application configuration values</summary>
//--------------------------------------------------
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using VSTestContext = Microsoft.VisualStudio.TestTools.UnitTesting.TestContext;

namespace CognizantSoftvision.Maqs.Utilities.Helper
{
    /// <summary>
    /// Config class
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// Object for locking the configs so 
        /// pending tasks will wait for config to be freed
        /// </summary>
        private static readonly object ConfigLock = new object();

        /// <summary>
        /// The default MAQS session
        /// </summary>
        private const ConfigSection DEFAULTMAQSSECTION = ConfigSection.GlobalMaqs;

        /// <summary>
        /// Configuration mappings from configuration files and environment variables
        /// </summary>
        private static readonly IConfigurationRoot baseConfig = new ConfigurationBuilder()
            .AddInMemoryCollection(GetXml())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables()
            .Build();

        /// <summary>
        /// Configuration mappings from run context 
        /// </summary>
        private static IConfigurationRoot runContextConfig = GetEmptyConfig();

        /// <summary>
        /// Configuration mappings dynamically updated in the test code
        /// </summary>
        private static IConfigurationRoot overrideConfig = GetEmptyConfig();

        /// <summary>
        /// Active composite configuration
        /// </summary>
        private static IConfigurationRoot compositeConfig = null;

        /// <summary>
        /// Loads when class is loaded
        /// </summary>
        static Config()
        {
            UpdateCompositeConfig();
        }

        /// <summary>
        /// Drop dynamic configuration overrides
        /// </summary>
        public static void ClearOverrides()
        {
            lock (ConfigLock)
            {
                overrideConfig = GetEmptyConfig();
                UpdateCompositeConfig();
            }
        }

        /// <summary>
        /// Validates the app config section by ensuring required values are present
        /// </summary>
        /// <param name="configSection">The config section to be validated</param>
        /// <param name="configValidation">A list of strings containing the requried field names</param>
        public static void Validate(ConfigSection configSection, ConfigValidation configValidation)
        {
            // Don't run the validation if the user has decided to skip the validation
            if (GetGeneralValue("SkipConfigValidation").Equals("Yes", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (configValidation == null)
            {
                throw new MaqsConfigException("The value passed in for configValidation (required fields in a config) is null");
            }
            var configSectionPassed = GetSectionDictionary(configSection);

            List<string> exceptions = new List<string>();

            // Check if we have any required fields
            if (configValidation.RequiredFields != null && configValidation.RequiredFields.Count > 0)
            {
                foreach (var requiredField in configValidation.RequiredFields)
                {
                    if (!configSectionPassed.ContainsKey(requiredField))
                    {
                        exceptions.Add($"Key missing: {requiredField}");
                    }
                }
            }

            // Check if we have any one of required fields
            if (configValidation.RequiredOneOfFields != null && configValidation.RequiredOneOfFields.Count > 0 && !configValidation.RequiredOneOfFields.Any(x => configSectionPassed.ContainsKey(x)))
            {
                // We have one of fields and didn't find any of them
                exceptions.Add($"Need at least one of the following keys: {string.Join(", ", configValidation.RequiredOneOfFields)}");
            }

            if (exceptions.Count > 0)
            {
                StringBuilder message = new StringBuilder();
                foreach (var exception in exceptions)
                {
                    message.AppendLine(exception);
                }

                message.AppendLine("*This check can be skipped by setting the 'SkipConfigValidation' configuration value to 'Yes'.");
                throw new MaqsConfigException(message.ToString());
            }
        }

        /// <summary>
        /// Get a specific config section
        /// </summary>
        /// <param name="section">The configuration section</param>
        /// <returns>Section values, with overrides respected</returns>
        public static Dictionary<string, string> GetSectionDictionary(ConfigSection section)
        {
            return GetSectionDictionary(section.ToString());
        }

        /// <summary>
        /// Get a specific config section
        /// </summary>
        /// <param name="section">The name of the section</param>
        /// <returns>Section values, with overrides respected</returns>
        public static Dictionary<string, string> GetSectionDictionary(string section)
        {
            lock (ConfigLock)
            {
                Dictionary<string, string> sectionValues = new Dictionary<string, string>();
                List<string> keyList = new List<string>();

                foreach (IConfigurationSection child in compositeConfig.GetSection(section).GetChildren())
                {
                    keyList.AddRange(GetRecursiveKeys(child));
                }

                // Loop over all the key value pairs
                foreach (var key in keyList.Distinct(StringComparer.CurrentCultureIgnoreCase).ToList())
                {
                    sectionValues.Add(key, GetValueForSection(section, key));
                }

                return sectionValues;
            }
        }

        /// <summary>
        /// Add VSTest property/parameter overrides
        /// </summary>
        /// <param name="context">The VSTest context</param>
        public static void UpdateWithVSTestContext(VSTestContext context)
        {
            Dictionary<string, string> paramValues = new Dictionary<string, string>();

            // Update configuration settings for Visual Studio unit test
            List<string> propeties = new List<string>();
            IDictionary<string, object> contextProperties = (IDictionary<string, object>)context.GetType().InvokeMember("Properties", BindingFlags.GetProperty, null, context, null);

            // Get a list of framework reserved properties so we can exclude them
            foreach (var property in context.GetType().GetProperties())
            {
                propeties.Add(property.Name);
            }

            foreach (KeyValuePair<string, object> property in contextProperties)
            {
                if (!propeties.Contains(property.Key) && property.Value is string)
                {
                    // Add the override properties
                    paramValues.Add(property.Key, property.Value as string);
                }
            }

            SetRuntimeConfig(paramValues);
        }

        /// <summary>
        /// Add NUnit property/parameter overrides
        /// </summary>
        /// <param name="testParameters">NUnit test parameters</param>
        public static void UpdateWithNUnitTestParameters(TestParameters testParameters)
        {
            Dictionary<string, string> paramValues = new Dictionary<string, string>();

            foreach (string propertyName in testParameters.Names)
            {
                // Add the override properties
                paramValues.Add(propertyName, testParameters[propertyName]);
            }

            SetRuntimeConfig(paramValues);
        }

        /// <summary>
        /// Add configuration override values
        /// </summary>
        /// <param name="configurations">Dictionary of configuration overrides</param>
        public static void AddGeneralTestSettingValues(IDictionary<string, string> configurations)
        {
            AddTestSettingValues(configurations, DEFAULTMAQSSECTION);
        }

        /// <summary>
        /// Add configuration override values
        /// </summary>
        /// <param name="key">Key for the value you are adding or overriding</param>
        /// <param name="value">Value being added or overridden</param>
        public static void AddGeneralTestSettingValues(string key, string value)
        {
            var newKeyValue = new Dictionary<string, string> { { key, value } };
            AddTestSettingValues(newKeyValue, DEFAULTMAQSSECTION.ToString());
        }

        /// <summary>
        /// Add configuration override values
        /// </summary>
        /// <param name="configurations">Dictionary of configuration overrides</param>
        /// <param name="section">What section it should be added to</param>
        public static void AddTestSettingValues(IDictionary<string, string> configurations, ConfigSection section = DEFAULTMAQSSECTION)
        {
            AddTestSettingValues(configurations, section.ToString());
        }

        /// <summary>
        /// Add configuration override values
        /// </summary>
        /// <param name="configurations">Dictionary of configuration overrides</param>
        /// <param name="section">What section it should be added to</param>
        public static void AddTestSettingValues(IDictionary<string, string> configurations, string section)
        {
            lock (ConfigLock)
            {
                var configSection = overrideConfig.GetSection(section);

                // Loop over all the configuration overrides
                foreach (KeyValuePair<string, string> configuration in configurations)
                {
                    configSection.GetSection(configuration.Key).Value = configuration.Value;
                }
            }
        }

        /// <summary>
        /// Add configuration override values
        /// </summary>
        /// <param name="key">Key for the value you are adding or overriding</param>
        /// <param name="value">Value being added or overridden</param>
        /// <param name="section">What section it should be added to</param>
        public static void AddTestSettingValues(string key, string value, string section)
        {
            var newKeyValue = new Dictionary<string, string> { { key, value } };
            AddTestSettingValues(newKeyValue, section);
        }

        /// <summary>
        /// Add configuration override values
        /// </summary>
        /// <param name="key">Key for the value you are adding or overriding</param>
        /// <param name="value">Value being added or overridden</param>
        /// <param name="section">What section it should be added to</param>
        public static void AddTestSettingValue(string key, string value, ConfigSection section = DEFAULTMAQSSECTION)
        {
            var newKeyValue = new Dictionary<string, string> { { key, value } };
            AddTestSettingValues(newKeyValue, section);
        }

        /// <summary>
        /// Get value from the general (GLOBALMAQS) section of the config file
        /// </summary>
        /// <param name="key">Config file key</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The configuration value - Returns the empty string if the key is not found</returns>
        public static string GetGeneralValue(string key, string defaultValue = "")
        {
            return GetValueForSection(DEFAULTMAQSSECTION, key, defaultValue);
        }

        /// <summary>
        /// Get the value from a specific section
        /// </summary>
        /// <param name="section">The configuration section</param>
        /// <param name="key">The key</param>
        /// <returns>The configuration value - Returns the empty string if the key is not found</returns>
        public static string GetValueForSection(ConfigSection section, string key)
        {
            return GetValueForSection(section.ToString(), key, string.Empty);
        }

        /// <summary>
        /// Get the value from a specific section
        /// </summary>
        /// <param name="section">The section name</param>
        /// <param name="key">The key</param>
        /// <returns>The configuration value - Returns the empty string if the key is not found</returns>
        public static string GetValueForSection(string section, string key)
        {
            return GetValueForSection(section, key, string.Empty);
        }

        /// <summary>
        /// Get the configuration value for a specific key
        /// </summary>
        /// <param name="section">The configuration section</param>
        /// <param name="key">Config file key</param>
        /// <param name="defaultValue">Default value - Returned the key cannot be found</param>
        /// <returns>The configuration value</returns>
        public static string GetValueForSection(ConfigSection section, string key, string defaultValue)
        {
            return GetValueForSection(section.ToString(), key, defaultValue);
        }

        /// <summary>
        /// Get the configuration value for a specific key
        /// </summary>
        /// <param name="section">The section name</param>
        /// <param name="key">Config file key</param>
        /// <param name="defaultValue">Default value - Returned the key cannot be found</param>
        /// <returns>The configuration value</returns>
        public static string GetValueForSection(string section, string key, string defaultValue)
        {
            section = section.ToLower();

            if (TryGetSectionValue(section, key, out string value))
            {
                // Return GlobalMaqs specific app.config value
                return value;
            }

            // Return the default if value is not found in appsettings.json
            return defaultValue;
        }

        /// <summary>
        /// Does the configuration key exist under the global MAQS section
        /// </summary>
        /// <param name="key">Config file key</param>
        /// <returns>True if the key exists</returns>
        [Browsable(false)]
        public static bool DoesGeneralKeyExist(string key)
        {
            return DoesKeyExist(DEFAULTMAQSSECTION, key);
        }

        /// <summary>
        /// Does the configuration key exist
        /// </summary>
        /// <param name="section">The configuration section</param>
        /// <param name="key">Config file key</param>
        /// <returns>True if the key exists</returns>
        [Browsable(false)]
        public static bool DoesKeyExist(ConfigSection section, string key)
        {
            return DoesKeyExist(section.ToString(), key);
        }

        /// <summary>
        /// Does the configuration key exist
        /// </summary>
        /// <param name="key">Config file key</param>
        /// <param name="section">The section name</param>
        /// <returns>True if the key exists</returns>
        [Browsable(false)]
        public static bool DoesKeyExist(string section, string key)
        {
            section = section.ToLower();
            return TryGetSectionValue(section, key, out _);
        }

        /// <summary>
        /// Get the 
        /// </summary>
        /// <param name="config">Root configuration</param>
        /// <param name="path">Path to the configuration node</param>
        /// <returns></returns>
        private static string GetValue(this IConfigurationRoot config, params string[] path)
        {
            lock (ConfigLock)
            {
                string fullPath = string.Join(":", path);
                return config.GetSection(fullPath).Value;
            }
        }

        /// <summary>
        /// Set default runtime configuration
        /// </summary>
        /// <param name="userProvidedConfiguration">New top level configuration</param>
        public static void AddDefaultConfiguration(IConfigurationRoot userProvidedConfiguration)
        {
            lock (ConfigLock)
            {
                runContextConfig = new ConfigurationBuilder().AddConfiguration(overrideConfig).AddConfiguration(userProvidedConfiguration).Build();
                UpdateCompositeConfig();
            }
        }

        /// <summary>
        /// Get capabilities in a multilevel format
        /// </summary>
        /// <param name="section">The configuration section</param>
        /// <returns>Multilevel dictionary of remote capabilities</returns>
        public static Dictionary<string, object> GetMultilevelDictionary(ConfigSection section)
        {
            Dictionary<string, object> structured = new Dictionary<string, object>();

            foreach (var pair in GetSectionDictionary(section))
            {

                var keys = pair.Key.Split(':');
                var value = pair.Value;

                if (keys.Length < 3)
                {
                    // Key has one or less colons so just add to the root dictionary directly 
                    structured[pair.Key] = value;
                }
                else
                {
                    string topKey = $"{keys[0]}:{keys[1]}";

                    // Make sure the lower level dictionary exists
                    if (!structured.ContainsKey(topKey))
                    {
                        structured[topKey] = new Dictionary<string, object>();
                    }

                    AddSubKeys(structured[topKey] as Dictionary<string, object>, keys, value);
                }
            }

            return structured;
        }

        /// <summary>
        /// Add sub keys and values
        /// </summary>
        /// <param name="current">Current dictorary we are adding to</param>
        /// <param name="keys">Keys to add</param>
        /// <param name="value">Value to add for key path</param>
        private static void AddSubKeys(Dictionary<string, object> current, string[] keys, string value)
        {
            int lastIndex = keys.Length - 1;

            // Add sub dictionaries
            for (int i = 2; i < keys.Length; i++)
            {
                if (i == lastIndex)
                {
                    current[keys[i]] = value;
                }
                else
                {
                    if (!current.ContainsKey(keys[i]))
                    {
                        current[keys[i]] = new Dictionary<string, object>();
                    }

                    current = current[keys[i]] as Dictionary<string, object>;
                }
            }
        }


        /// <summary>
        /// Rebuild the composite configuration
        /// </summary>
        private static void UpdateCompositeConfig()
        {
            compositeConfig = new ConfigurationBuilder().AddConfiguration(baseConfig).AddConfiguration(runContextConfig).AddConfiguration(overrideConfig).Build();
        }

        /// <summary>
        /// Set runtime configuration
        /// </summary>
        /// <param name="keyValuePairs">Dictionary of runtime configurations</param>
        private static void SetRuntimeConfig(Dictionary<string, string> keyValuePairs)
        {
            lock (ConfigLock)
            {
                // Only add if values were provided
                if (keyValuePairs.Count > 0)
                {
                    if (runContextConfig.AsEnumerable().Any())
                    {
                        // Merge old and new values
                        runContextConfig = new ConfigurationBuilder().AddInMemoryCollection(runContextConfig.AsEnumerable()).AddInMemoryCollection(keyValuePairs).Build();
                    }
                    else
                    {
                        // Config was empty so just add new values
                        runContextConfig = new ConfigurationBuilder().AddInMemoryCollection(keyValuePairs).Build();
                    }

                    UpdateCompositeConfig();
                }
            }
        }

        /// <summary>
        /// Get the config value for a given path
        /// </summary>
        /// <param name="path">Path to the desired key</param>
        /// <returns>A tuple. Tuple 'found' is if the value was found. Tuple 'value' is the found value or null.</returns>
        public static (bool found, string value) GetValueByPath(params string[] path)
        {
            // Check for base config
            string value = compositeConfig.GetValue(path);

            // Fall back to the old Magenic MAQS section if the newer global section is not found
            if (string.IsNullOrEmpty(value) && path.Length > 0 && path[0].Equals(DEFAULTMAQSSECTION.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                string[] newPath = (string[])path.Clone();

#pragma warning disable CS0618 // Type or member is obsolete
                newPath[0] = ConfigSection.MagenicMaqs.ToString().ToLower();
#pragma warning restore CS0618 // Type or member is obsolete

                value = compositeConfig.GetValue(newPath);
            }

            return (value != null, value);
        }

        /// <summary>
        /// Try to get a value for a given key in a given section
        /// </summary>
        /// <param name="section">The section name</param>
        /// <param name="key">The value key</param>
        /// <param name="value">The out value</param>
        /// <returns>True if the value was found</returns>
        private static bool TryGetSectionValue(string section, string key, out string value)
        {
            // Get the value if it exists
            var result = GetValueByPath(section, key);

            // Set return value, this may be null
            value = result.value;

            // Return if the value was found
            return result.found;
        }

        /// <summary>
        /// Recursively get keys with values
        /// </summary>
        /// <param name="child">Child node to get keys for</param>
        /// <returns>List of keys</returns>
        private static List<string> GetRecursiveKeys(IConfigurationSection child)
        {
            List<string> keyList = new List<string>();

            // Add the current child key if it has a value
            if (!string.IsNullOrEmpty(child.Value))
            {
                keyList.Add(child.Key);
            }

            // Recusively look for lower level children that have values
            foreach (IConfigurationSection subchild in child.GetChildren())
            {
                foreach (string key in GetRecursiveKeys(subchild))
                {
                    keyList.Add($"{child.Key}:{key}");
                }
            }

            return keyList;
        }

        /// <summary>
        /// Create an empty configuration root
        /// </summary>
        /// <returns>An empty configuration root</returns>
        private static IConfigurationRoot GetEmptyConfig()
        {
            return new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>()).Build();
        }

        /// <summary>
        /// Reads the xml config file and adds the keys
        /// </summary>
        /// <returns>A dictionary of key value pairs</returns>
        private static IEnumerable<KeyValuePair<string, string>> GetXml()
        {
            // get the file path
            string defaultPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string name = "App.config";
            string path = Path.Combine(defaultPath, name);

            // read the xml file
            XElement rootElement;
            try
            {
                rootElement = XDocument.Parse(File.ReadAllText(path)).Root;
            }
            catch (FileNotFoundException)
            {
                // xml file is not required
                return new List<KeyValuePair<string, string>>();
            }

            // create a list of key pairs
            Dictionary<string, string> keysAndValues = new Dictionary<string, string>();

            // find the values MAQS is looking for
            foreach (XElement node in rootElement.Elements())
            {
                // set if keys under this node would be saved and if they should overwrite previous keys
                if (node.Name.LocalName.ToLower().Contains("runtime") || node.Name.LocalName.ToLower().Contains("configsections"))
                {
                    continue;
                }

                // Get info form nodes
                foreach (XElement config in node.Elements())
                {
                    RecursiveNodeKeyValues(node.Name.LocalName, config, keysAndValues);
                }
            }

            return keysAndValues;
        }

        /// <summary>
        /// Recursively get keys with values
        /// </summary>
        /// <param name="path">Previous path</param>
        /// <param name="node">Current XML node</param>
        /// <param name="keysAndValues">Keys and values</param>
        private static void RecursiveNodeKeyValues(string path, XElement node, Dictionary<string, string> keysAndValues)
        {
            IEnumerable<XAttribute> attributes = node.Attributes();

            string key;

            if (attributes.Any(x => x.Name.LocalName.Equals("key")))
            {
                // This node has a key so use it
                string value = node.Attribute("key").Value;
                key = path == string.Empty ? value : $"{path}:{value}";
            }
            else
            {
                // No key was provided so use the element name
                string value = node.Name.LocalName;
                key = path == string.Empty ? value : $"{path}:{value}";
            }

            // This node has a value associated with it so add it to the keys and values dictionary
            if (attributes.Any(y => y.Name.LocalName.Equals("value")))
            {
                keysAndValues.Add(key, node.Attribute("value").Value);
            }

            // Look at children elements
            foreach (var element in node.Elements())
            {
                RecursiveNodeKeyValues(key, element, keysAndValues);
            }
        }
    }
}
