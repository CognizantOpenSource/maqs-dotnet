﻿//--------------------------------------------------
// <copyright file="WebServiceGets.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Web service get unit tests</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseWebServiceTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using WebServiceTesterUnitTesting.Model;

namespace WebServiceTesterUnitTesting
{
    /// <summary>
    /// Test web service gets
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class WebServiceGets
    {
        /// <summary>
        /// String to hold the URL
        /// </summary>
        private static readonly string url = WebServiceConfig.GetWebServiceUri();

        /// <summary>
        /// Make sure the web service have been woken up
        /// </summary>
        /// <param name="context">The test context</param>
        [AssemblyInitialize]
        public static void PrimeSite(TestContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            try
            {
                WebServiceDriver client = new WebServiceDriver(new Uri("http://localhost:5026"));
                client.Get("/api/String/1", "text/plain", false);
            }
            catch
            {
                // eat expected error for priming
            }
        }

        /// <summary>
        /// Test XML get
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        public void GetXmlDeserialized()
        {
            WebServiceDriver client = new WebServiceDriver(new Uri(url));
            ArrayOfProduct result = client.Get<ArrayOfProduct>("/api/XML_JSON/GetAllProducts", "application/xml", false);

            Assert.AreEqual(3, result.Product.Length, "Expected 3 products to be returned");
        }

        /// <summary>
        /// Test Json Get
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        public void GetJsonDeserialized()
        {
            WebServiceDriver client = new WebServiceDriver(new Uri(url));
            List<ProductJson> result = client.Get<List<ProductJson>>("/api/XML_JSON/GetAllProducts", "application/json", false);

            Assert.AreEqual(3, result.Count, "Expected 3 products to be returned");
        }

        /// <summary>
        /// Test string Get
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        public void GetString()
        {
            WebServiceDriver client = new WebServiceDriver(new Uri("http://localhost:5026"));
            string result = client.Get("/api/String/1", "text/plain", false);

            Assert.IsTrue(result.Contains("Tomato Soup"), "Was expecting a result with Tomato Soup but instead got - " + result);
        }

        /// <summary>
        /// Test getting an image
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.WebService)]
        public void GetImage()
        {
            WebServiceDriver client = new WebServiceDriver(new Uri(url));
            HttpResponseMessage result = client.GetWithResponse("/api/PNGFile/GetImage?image=Red", "image/png", false);

            using var image = Image.Load(result.Content.ReadAsByteArrayAsync().Result);

            Assert.AreEqual(200, image.Width, "Image width should be 200");
            Assert.AreEqual(200, image.Height, "Image hight should be 200");

            image.Dispose();
        }
    }
}