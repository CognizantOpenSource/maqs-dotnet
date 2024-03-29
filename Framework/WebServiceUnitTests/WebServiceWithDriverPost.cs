﻿//--------------------------------------------------
// <copyright file="WebServiceWithDriverPost.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Web service post unit tests</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseWebServiceTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Text;
using WebServiceTesterUnitTesting.Model;

namespace WebServiceTesterUnitTesting
{
    /// <summary>
    /// Test class for unit tests
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class WebServiceWithDriverPost : BaseWebServiceTest
    {
        /// <summary>
        /// Post JSON request to verify status codes
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        public void PostJSONSerializedVerifyStatusCode()
        {
            ProductJson p = new ProductJson
            {
                Category = "ff",
                Id = 4,
                Name = "ff",
                Price = 3.25f
            };
            var content = WebServiceUtils.MakeStringContent<ProductJson>(p, Encoding.UTF8, "application/json");
            var result = this.WebServiceDriver.PostWithResponse("/api/XML_JSON/Post", "application/json", content, true);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        /// <summary>
        /// Post JSON stream request to verify status codes
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        public void PostJSONStreamSerializedVerifyStatusCode()
        {
            ProductJson p = new ProductJson
            {
                Category = "ff",
                Id = 4,
                Name = "ff",
                Price = 3.25f
            };
            var content = WebServiceUtils.MakeStreamContent<ProductJson>(p, Encoding.UTF8, "application/json");
            var result = this.WebServiceDriver.PostWithResponse("/api/XML_JSON/Post", "application/json", content, true);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        /// <summary>
        /// Post XML request to verify status codes
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        public void PostXMLSerializedVerifyStatusCode()
        {
            Product p = new Product
            {
                Category = "ff",
                Id = 4,
                Name = "ff",
                Price = 3.25f
            };
            var content = WebServiceUtils.MakeStringContent<Product>(p, Encoding.UTF8, "application/xml");
            var result = this.WebServiceDriver.PostWithResponse("/api/XML_JSON/Post", "application/xml", content, true);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        /// <summary>
        /// Post XML request to verify status codes
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        public void PostXMLStreamSerializedVerifyStatusCode()
        {
            Product p = new Product
            {
                Category = "ff",
                Id = 4,
                Name = "ff",
                Price = 3.25f
            };
            var content = WebServiceUtils.MakeStreamContent<Product>(p, Encoding.UTF8, "application/xml");
            var result = this.WebServiceDriver.PostWithResponse("/api/XML_JSON/Post", "application/xml", content, true);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        /// <summary>
        /// Post with JSON
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        public void PostWithJson()
        {
            ProductJson p = new ProductJson
            {
                Category = "ff",
                Id = 4,
                Name = "ff",
                Price = 3.25f
            };
            var content = WebServiceUtils.MakeStreamContent<ProductJson>(p, Encoding.UTF8, "application/json");
            var result = this.WebServiceDriver.Post<ProductJson>("/api/XML_JSON/Post", "application/json", content, true);
            Assert.IsTrue(result == null);
        }

        /// <summary>
        /// Post XML to verify no string is returned
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        public void PostXMLSerializedVerifyEmptyString()
        {
            Product p = new Product
            {
                Category = "ff",
                Id = 4,
                Name = "ff",
                Price = 3.25f
            };
            var content = WebServiceUtils.MakeStringContent<Product>(p, Encoding.UTF8, "application/xml");
            var result = this.WebServiceDriver.Post("/api/XML_JSON/Post", "application/xml", content, true);
            Assert.AreEqual(string.Empty, result);
        }

        /// <summary>
        /// Post string without utility
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        public void PostStringWithoutMakeContent()
        {
            var result = this.WebServiceDriver.Post("/api/String", "text/plain", "Test", Encoding.UTF8, "text/plain");
            Assert.AreEqual(string.Empty, result);
        }

        /// <summary>
        /// Post stream without utility
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        public void PostStreamWithoutMakeContent()
        {
            var result = this.WebServiceDriver.Post("/api/String", "text/plain", "Test", Encoding.UTF8, "text/plain", false, true);
            Assert.AreEqual(string.Empty, result);
        }

        /// <summary>
        /// Post string with utility
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        public void PostStringWithMakeContent()
        {
            var content = WebServiceUtils.MakeStringContent("Test", Encoding.UTF8, "text/plain");
            var result = this.WebServiceDriver.Post("/api/String", "text/plain", content, true);
            Assert.AreEqual(string.Empty, result);
        }

        /// <summary>
        /// Post string without utility to verify status code
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        public void PostStringWithoutContentStatusCode()
        {
            var result = this.WebServiceDriver.PostWithResponse("/api/String", "text/plain", "Test", Encoding.UTF8, "text/plain", true, true);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        /// <summary>
        /// Post stream without utility to verify status code
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        public void PostStreamWithoutContentStatusCode()
        {
            var result = this.WebServiceDriver.PostWithResponse("/api/String", "text/plain", "Test", Encoding.UTF8, "text/plain", false, true);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        /// <summary>
        /// Post string with utility to verify status code
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        public void PostStringMakeContentStatusCode()
        {
            var content = WebServiceUtils.MakeStringContent("Test", Encoding.UTF8, "text/plain");
            var result = this.WebServiceDriver.PostWithResponse("/api/String", "text/plain", content, true);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        /// <summary>
        /// Verifying other http status codes
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        public void PostExpectContentError()
        {
            var result = this.WebServiceDriver.PostWithResponse("/api/String", "text/plain", null, false);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        /// <summary>
        /// Testing string returned
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        public void PostExpectStringError()
        {
            var result = this.WebServiceDriver.Post("/api/String", "text/plain", null, false);
            Assert.AreEqual("{\"message\":\"Value is required\"}", result);
        }

        /// <summary>
        /// Testing string returned
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        public void PostExpectStringErrorEmptyHttpContent()
        {
            var result = this.WebServiceDriver.Post("/api/String", "text/plain", new StringContent(string.Empty, Encoding.UTF8), false);
            Assert.AreEqual("{\"message\":\"Value is required\"}", result);
        }
    }
}
