﻿//--------------------------------------------------
// <copyright file="BaseTestSteps.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Base teststeps code for general SpecFlow tests</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;
using MaqsBase = CognizantSoftvision.Maqs.BaseTest.BaseTest;

namespace CognizantSoftvision.Maqs.SpecFlow.TestSteps
{
    /// <summary>
    /// Base for TestSteps classes
    /// </summary>
    [Binding, Scope(Tag = "MAQS_General")]
    public class BaseTestSteps : AbstractTestSteps<BaseTestObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestSteps" /> class.
        /// </summary>
        /// <param name="context">The scenario context.</param>
        protected BaseTestSteps(ScenarioContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets the TestObject
        /// </summary>
        public override BaseTestObject TestObject
        {
            get
            {
                return this.LocalScenarioContext.Get<BaseTestObject>($"MAQSTESTOBJECT");
            }
        }

        /// <summary>
        /// Set up the Test object
        /// </summary>
        internal override void SetupBaseTest()
        {
            // Build/setup a new base test
            MaqsBase basetest = new MaqsBase();

            // necessary for MsTest log files to have the correct name
            try
            {
                basetest.TestContext = this.LocalScenarioContext.ScenarioContainer.Resolve<TestContext>();
            }
            catch (MemberAccessException)
            {
                // do nothing on this error (will occur if NUnit is being used)
            }

            basetest.Setup();

            // Save the base test
            this.LocalScenarioContext.Set(basetest, $"MAQSBASETEST");

            // Save the base test object
            this.LocalScenarioContext.Set(basetest.TestObject, $"MAQSTESTOBJECT");
        }

        /// <summary>
        /// Teardown the test object
        /// </summary>
        internal override void TeardownBaseTest()
        {
            this.LocalScenarioContext.Get<MaqsBase>($"MAQSBASETEST").Teardown();
        }
    }
}