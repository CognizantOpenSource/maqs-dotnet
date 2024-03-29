﻿//--------------------------------------------------
// <copyright file="WebServiceDriverManager.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Web service driver</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseTest;
using CognizantSoftvision.Maqs.Utilities.Logging;
using System;
using System.Net.Http;

namespace CognizantSoftvision.Maqs.BaseWebServiceTest
{
    /// <summary>
    /// Web service driver
    /// </summary>
    public class WebServiceDriverManager : DriverManager
    {
        /// <summary>
        /// Cached copy of the driver
        /// </summary>
        private WebServiceDriver driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebServiceDriverManager" /> class
        /// </summary>
        /// <param name="getDriver">Function for creating an http client</param>
        /// <param name="testObject">The associated test object</param>
        public WebServiceDriverManager(Func<HttpClient> getDriver, ITestObject testObject) : base(getDriver, testObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebServiceDriverManager" /> class
        /// </summary>
        /// <param name="httpClient">Http client to use with the driver</param>
        /// <param name="testObject">The associated test object</param>
        public WebServiceDriverManager(HttpClient httpClient, ITestObject testObject) : base(() => httpClient, testObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebServiceDriverManager" /> class
        /// </summary>
        /// <param name="getDriver">Function for creating an http client</param>
        /// <param name="testObject">The associated test object</param>
        public WebServiceDriverManager(WebServiceDriver getDriver, ITestObject testObject) : base(() => getDriver.HttpClient, testObject)
        {
            this.driver = getDriver;
        }

        /// <summary>
        /// Override the web service driver
        /// </summary>
        /// <param name="driver">A new http driver</param>
        public void OverrideDriver(WebServiceDriver driver)
        {
            this.OverrideDriverGet(() => driver.HttpClient);
            this.driver = driver;
        }

        /// <summary>
        /// Override the web service driver - respects lazy loading
        /// </summary>
        /// <param name="overrideDriver">Function for getting a new HTTP client</param>
        public void OverrideDriver(Func<HttpClient> overrideDriver)
        {
            this.OverrideDriverGet(overrideDriver);
            this.driver = null;
        }

        /// <summary>
        /// Override the web service driver
        /// </summary>
        /// <param name="overrideHttpClient">The new http client driver</param>
        public void OverrideDriver(HttpClient overrideHttpClient)
        {
            this.OverrideDriverGet(() => overrideHttpClient);
            this.driver = null;
        }

        /// <summary>
        /// Get the http driver
        /// </summary>
        /// <returns>The http driver</returns>
        public WebServiceDriver GetWebServiceDriver()
        {
            if (this.driver == null)
            {
                if (LoggingConfig.GetLoggingEnabledSetting() == LoggingEnabled.NO)
                {
                    this.Log.LogMessage(MessageType.INFORMATION, "Getting web service driver");
                    this.driver = new WebServiceDriver(GetBase() as HttpClient);
                }
                else
                {
                    this.Log.LogMessage(MessageType.INFORMATION, "Getting event firing web service driver");
                    this.driver = new EventFiringWebServiceDriver(GetBase() as HttpClient);
                    this.MapEvents(this.driver as EventFiringWebServiceDriver);
                }
            }

            return this.driver;
        }

        /// <summary>
        /// Get the service driver
        /// </summary>
        /// <returns>The web service driver</returns>
        public override object Get()
        {
            return this.GetWebServiceDriver();
        }

        /// <summary>
        /// Map web service events to log events
        /// </summary>
        /// <param name="eventFiringDriver">The event firing web client driver that we want mapped</param>
        public void MapEvents(EventFiringWebServiceDriver eventFiringDriver)
        {
            eventFiringDriver.WebServiceEvent += this.WebService_Event;
            eventFiringDriver.WebServiceActionEvent += this.WebService_Action;
            eventFiringDriver.WebServiceErrorEvent += this.WebService_Error;
            eventFiringDriver.WebServiceVerboseEvent += this.WebService_Verbose;
        }

        /// <summary>
        /// Dispose of the driver
        /// </summary>
        protected override void DriverDispose()
        {
            this.BaseDriver = null;
        }

        /// <summary>
        /// Web service event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="message">The logging message</param>
        private void WebService_Event(object sender, string message)
        {
            this.Log.LogMessage(MessageType.INFORMATION, message);
        }

        /// <summary>
        /// Web service action event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="message">The logging message</param>
        private void WebService_Action(object sender, string message)
        {
            this.Log.LogMessage(MessageType.ACTION, message);
        }

        /// <summary>
        /// Web service error event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="message">The logging message</param>
        private void WebService_Error(object sender, string message)
        {
            this.Log.LogMessage(MessageType.ERROR, message);
        }

        /// <summary>
        /// Web service verbose event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="message">The logging message</param>
        private void WebService_Verbose(object sender, string message)
        {
            this.Log.LogMessage(MessageType.VERBOSE, message);
        }
    }
}
