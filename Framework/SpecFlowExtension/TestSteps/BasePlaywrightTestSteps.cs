//--------------------------------------------------
// <copyright file="BasePlaywrightTestSteps.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Base teststeps code for tests using Playwright</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BasePlaywrightTest;
using TechTalk.SpecFlow;
using MaqsPlaywright = CognizantSoftvision.Maqs.BasePlaywrightTest.BasePlaywrightTest;

namespace CognizantSoftvision.Maqs.SpecFlow.TestSteps
{
    /// <summary>
    /// Base for Playwright TestSteps classes
    /// </summary>
    [Binding, Scope(Tag = "MAQS_Playwright")]
    public class BasePlaywrightTestSteps : ExtendableTestSteps<IPlaywrightTestObject, MaqsPlaywright>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasePlaywrightTestSteps" /> class
        /// </summary>
        /// <param name="context">The scenario context.</param>
        public BasePlaywrightTestSteps(ScenarioContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets the page driver from the test object
        /// </summary>
        protected PageDriver PageDriver
        {
            get { return this.TestObject.PageDriver; }
        }
    }
}
