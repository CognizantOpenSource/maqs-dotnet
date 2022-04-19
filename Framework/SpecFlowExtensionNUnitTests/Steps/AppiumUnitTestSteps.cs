//--------------------------------------------------
// <copyright file="AppiumUnitTestSteps.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>TestSteps class that inherits from BaseAppiumTestSteps</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseAppiumTest;
using CognizantSoftvision.Maqs.SpecFlow.TestSteps;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SpecFlowExtensionNUnitTests.Steps
{
    /// <summary>
    /// BaseAppium unit test steps
    /// </summary>
    [Binding]
    public class AppiumUnitTestSteps : BaseAppiumTestSteps
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppiumUnitTestSteps" /> class
        /// </summary>
        /// <param name="context">The context to pass to the base class</param>
        protected AppiumUnitTestSteps(ScenarioContext context) : base(context)
        {
        }

        /// <summary>
        /// Class is instantiated
        /// </summary>
        [Given(@"class BaseAppiumTestSteps")]
        public void GivenClassBaseAppiumTestSteps()
        {
            Assert.IsNotNull(this);
        }

        /// <summary>
        /// Test object exists
        /// </summary>
        [Then(@"BaseAppiumTestSteps TestObject is not null")]
        public void ThenBaseAppiumTestStepsTestObjectIsNotNull()
        {
            Assert.IsNotNull(this.TestObject, "TestObject for BaseAppiumTestSteps class is null.");
        }

        /// <summary>
        /// Test object exists
        /// </summary>
        [Then(@"TestObject is type AppiumTestObject")]
        public void ThenTestObjectIsTypeAppiumTestObject()
        {
            Assert.IsTrue(this.TestObject.GetType().Equals(typeof(AppiumTestObject)), $"TestObject for BaseAppiumTestSteps class is the wrong type : {this.TestObject.GetType()}.");
        }

        /// <summary>
        /// ScenarioContext exists
        /// </summary>
        [Then(@"BaseAppiumTestSteps ScenarioContext is not null")]
        public void ThenScenarioContextIsNotNull()
        {
            Assert.IsNotNull(this.LocalScenarioContext, "LocalScenarioContext for BaseAppiumTestSteps class is null.");
        }

        /// <summary>
        /// ScenarioContext is valid
        /// </summary>
        [Then(@"BaseAppiumTestSteps ScenarioContext is type ScenarioContext")]
        public void AndScenarioContextIsTypeScenarioContext()
        {
            Assert.IsTrue(this.LocalScenarioContext.GetType().Equals(typeof(ScenarioContext)), $"LocalScenarioContext for BaseAppiumTestSteps class is the wrong type : {this.LocalScenarioContext.GetType()}.");
        }
    }
}
