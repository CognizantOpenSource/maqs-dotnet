﻿//--------------------------------------------------
// <copyright file="BaseFrameworkTests.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Low level framework tests</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseSeleniumTest;
using CognizantSoftvision.Maqs.BaseTest;
using CognizantSoftvision.Maqs.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;

namespace SeleniumUnitTests
{
    /// <summary>
    /// Framework unit test class
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    [DoNotParallelize]
    public class BaseFrameworkTests : BaseTestUnitTests.BaseFrameworkTests
    {
        /// <summary>
        ///  Base test does soft assert check
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Framework)]
        public new void SoftAssertWithNoFailure()
        {
            base.SoftAssertWithNoFailure();
        }

        /// <summary>
        ///  Base test does soft assert check
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Framework)]
        [ExpectedException(typeof(AssertFailedException))]
        public new void SoftAssertWithFailure()
        {
            base.SoftAssertWithFailure();
        }

        /// <summary>
        ///  Base test does soft assert check
        /// </summary>
        [Test]
        [Category(TestCategories.Framework)]
        [Category(TestCategories.NUnit)]
        public new void SoftAssertNUnitWithNoFailure()
        {
            base.SoftAssertWithNoFailure();
        }

        /// <summary>
        ///  Base test does soft assert check
        /// </summary>
        [Test]
        [Category(TestCategories.Framework)]
        [Category(TestCategories.NUnit)]
        public new void SoftAssertNUnitWithFailure()
        {
            base.SoftAssertNUnitWithFailure();
        }

        /// <summary>
        /// Override the base test object
        /// </summary>
        /// <returns>The base test as base Selenium</returns>
        protected override BaseTest GetBaseTest()
        {
            return new BaseSeleniumTest();
        }
    }
}
