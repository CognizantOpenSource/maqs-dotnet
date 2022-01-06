//--------------------------------------------------
// <copyright file="WebServiceTestObject.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Holds web service context data</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseTest;
using CognizantSoftvision.Maqs.Utilities.Logging;
using System;
using System.Net.Http;

namespace CognizantSoftvision.Maqs.BaseWebServiceTest
{
    /// <summary>
    /// Web service test context data
    /// </summary>
    public class WebServiceTestObject : BaseTestObject, IWebServiceTestObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebServiceTestObject" /> class
        /// </summary>
        /// <param name="httpClient">The test's http client driver</param>
        /// <param name="logger">The test's logger</param>
        /// <param name="fullyQualifiedTestName">The test's fully qualified test name</param>
        public WebServiceTestObject(Func<HttpClient> httpClient, ILogger logger, string fullyQualifiedTestName) : base(logger, fullyQualifiedTestName)
        {
            this.ManagerStore.Add(typeof(WebServiceDriverManager).FullName, new WebServiceDriverManager(httpClient, this));
        }

        /// <summary>
        /// Gets the web service driver
        /// </summary>
        public WebServiceDriver WebServiceDriver
        {
            get
            {
                return this.WebServiceManager.GetWebServiceDriver();
            }
        }

        /// <summary>
        /// Gets the web service driver manager
        /// </summary>
        public WebServiceDriverManager WebServiceManager
        {
            get
            {
                return this.ManagerStore.GetManager<WebServiceDriverManager>();
            }
        }

        /// <summary>
        /// Override the http client
        /// </summary>
        /// <param name="httpClient">The new http client</param>
        public void OverrideWebServiceDriver(HttpClient httpClient)
        {
            this.WebServiceManager.OverrideDriver(httpClient);
        }

        /// <summary>
        /// Override the http client
        /// </summary>
        /// <param name="httpClient">Function for getting a new http client</param>
        public void OverrideWebServiceDriver(Func<HttpClient> httpClient)
        {
            this.WebServiceManager.OverrideDriver(httpClient);
        }

        /// <summary>
        /// Override the http client driver
        /// </summary>
        /// <param name="webServiceDriver">An http client driver</param>
        public void OverrideWebServiceDriver(WebServiceDriver webServiceDriver)
        {
            this.WebServiceManager.OverrideDriver(webServiceDriver);
        }
    }
}