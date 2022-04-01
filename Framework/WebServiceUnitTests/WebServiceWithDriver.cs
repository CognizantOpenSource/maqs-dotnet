﻿//--------------------------------------------------
// <copyright file="WebServiceWithDriver.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Web service general unit tests</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseWebServiceTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WebServiceTesterUnitTesting
{
    /// <summary>
    /// Test web service driver testing
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class WebServiceWithDriver : BaseWebServiceTest
    {
        /// <summary>
        /// Make sure the test objects map properly
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        [TestCategory(TestCategories.Utilities)]
        public void WebServiceTestObjectMapCorrectly()
        {
            Assert.AreEqual(this.TestObject.Log, this.Log, "Logs don't match");
            Assert.AreEqual(this.TestObject.SoftAssert, this.SoftAssert, "Soft asserts don't match");
            Assert.AreEqual(this.TestObject.PerfTimerCollection, this.PerfTimerCollection, "Soft asserts don't match");
            Assert.AreEqual(this.TestObject.WebServiceDriver, this.WebServiceDriver, "Web service driver don't match");
        }

        /// <summary>
        /// Make sure test object values are saved as expected
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        [TestCategory(TestCategories.Utilities)]
        public void WebServiceTestObjectValuesCanBeUsed()
        {
            this.TestObject.SetValue("1", "one");

            Assert.AreEqual("one", this.TestObject.Values["1"]);
            string outValue;
            Assert.IsFalse(this.TestObject.Values.TryGetValue("2", out outValue), "Didn't expect to get value for key '2', but got " + outValue);
        }

        /// <summary>
        /// Make sure the test object objects are saved as expected
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        [TestCategory(TestCategories.Utilities)]
        public void WebServiceTestObjectObjectssCanBeUsed()
        {
            StringBuilder builder = new StringBuilder();
            this.TestObject.SetObject("1", builder);

            Assert.AreEqual(this.TestObject.Objects["1"], builder);

            object outObject;
            Assert.IsFalse(this.TestObject.Objects.TryGetValue("2", out outObject), "Didn't expect to get value for key '2'");

            builder.Append("123");

            Assert.AreEqual(((StringBuilder)this.TestObject.Objects["1"]).ToString(), builder.ToString());
        }

        /// <summary>
        /// Make we handle timeouts
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        [TestCategory(TestCategories.Utilities)]
        [ExpectedException(typeof(TimeoutException))]
        public void WebServiceTimeout()
        {
            var httpClient = this.GetHttpClient();
            httpClient.Timeout = TimeSpan.FromTicks(1);
            this.WebServiceDriver = new WebServiceDriver(httpClient);

            this.WebServiceDriver.Get("/api/String/1", "text/plain");

            Assert.Fail("Get call should have timed out");
        }
    }
}
