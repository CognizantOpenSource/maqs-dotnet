using System;

namespace CognizantSoftvision.Maqs.BaseTest
{
    /// <summary>
    /// Expected assert keys for a <see cref="SoftAssert"/> instance.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SoftAssertExpectedAssertsAttribute : Attribute
    {
        /// <summary>
        /// Collection of Expected Assert key strings from this attribute.
        /// </summary>
        public string[] ExpectedAssertKeys { get; private set; }

        /// <summary>
        /// Expected assert keys for a <see cref="SoftAssert"/> instance.
        /// </summary>
        /// <param name="expectedAssertKeys">Collection of keys for the soft assert instance in this test.</param>
        public SoftAssertExpectedAssertsAttribute(params string[] expectedAssertKeys)
        {
            ExpectedAssertKeys = expectedAssertKeys;
        }
    }
}
