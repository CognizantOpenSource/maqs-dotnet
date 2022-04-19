//--------------------------------------------------
// <copyright file="PlaywrightUnitTestSteps.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>TestSteps class that inherits from BasePlaywrightTestSteps</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BasePlaywrightTest;
using CognizantSoftvision.Maqs.SpecFlow.TestSteps;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowExtensionNUnitTests.Steps
{
    /// <summary>
    /// BasePlaywright unit test steps
    /// </summary>
    [Binding]
    public class PlaywrightUnitTestSteps : BasePlaywrightTestSteps
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaywrightUnitTestSteps" /> class
        /// </summary>
        /// <param name="context">The context to pass to the base class</param>
        protected PlaywrightUnitTestSteps(ScenarioContext context) : base(context)
        {
        }

        /// <summary>
        /// Class is instantiated
        /// </summary>
        [Given(@"class BasePlaywrightTestSteps")]
        public void GivenClassBasePlaywrightTestSteps()
        {
            Assert.IsNotNull(this);
        }

        /// <summary>
        /// Test object exists
        /// </summary>
        [Then(@"BasePlaywrightTestSteps TestObject is not null")]
        public void ThenBasePlaywrightTestStepsTestObjectIsNotNull()
        {
            Assert.IsNotNull(this.TestObject, "TestObject for BasePlaywrightTestSteps class is null.");
        }

        /// <summary>
        /// Test object exists
        /// </summary>
        [Then(@"TestObject is type PlaywrightTestObject")]
        public void ThenTestObjectIsTypePlaywrightTestObject()
        {
            Assert.IsTrue(this.TestObject.GetType().Equals(typeof(PlaywrightTestObject)), $"TestObject for BasePlaywrightTestSteps class is the wrong type : {this.TestObject.GetType()}.");
        }

        /// <summary>
        /// ScenarioContext exists
        /// </summary>
        [Then(@"BasePlaywrightTestSteps ScenarioContext is not null")]
        public void ThenScenarioContextIsNotNull()
        {
            Assert.IsNotNull(this.LocalScenarioContext, "LocalScenarioContext for BasePlaywrightTestSteps class is null.");
        }

        /// <summary>
        /// ScenarioContext is valid
        /// </summary>
        [Then(@"BasePlaywrightTestSteps ScenarioContext is type ScenarioContext")]
        public void AndScenarioContextIsTypeScenarioContext()
        {
            Assert.IsTrue(this.LocalScenarioContext.GetType().Equals(typeof(ScenarioContext)), $"LocalScenarioContext for BasePlaywrightTestSteps class is the wrong type : {this.LocalScenarioContext.GetType()}.");
        }

        /// <summary>
        /// ScenarioContext is valid
        /// </summary>
        [Then(@"BasePlaywrightTestSteps Null driver is null")]
        public void AndNullDriverIsNull()
        {
            this.TestObject.OverridePageDriver(() => null);
            Assert.IsNull(this.PageDriver.AsyncPage);
        }
    }
}
