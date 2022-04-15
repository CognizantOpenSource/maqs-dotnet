//--------------------------------------------------
// <copyright file="DriverManager.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Base driver manager</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.Utilities.Logging;
using System;

namespace CognizantSoftvision.Maqs.BaseTest
{
    /// <summary>
    /// Base driver manager object
    /// </summary>
    public abstract class DriverManager : IDriverManager
    {
        /// <summary>
        /// The test object associated with the driver
        /// </summary>
        private readonly ITestObject testObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="DriverManager"/> class
        /// </summary>
        /// <param name="funcToRun">How to get the underlying driver</param>
        /// <param name="testObject">The associate test object</param>
        protected DriverManager(Func<object> funcToRun, ITestObject testObject)
        {
            this.GetDriver = funcToRun;
            this.testObject = testObject;
        }


        /// <inheritdoc />
        public ILogger Log
        {
            get
            {
                return this.testObject.Log;
            }
        }

        /// <summary>
        /// Gets or sets the underlying driver; like the web driver or database connection driver
        /// </summary>
        protected object BaseDriver { get; set; }

        /// <summary>
        /// Gets or sets the function for getting the underlying driver
        /// </summary>
        protected Func<object> GetDriver { get; private set; }

        /// <summary>
        /// Overrides the driver get
        /// </summary>
        /// <param name="driverGet">Function of the driver get</param>
        protected void OverrideDriverGet(Func<object> driverGet)
        {
            this.DriverDispose();
            this.BaseDriver = null;
            this.GetDriver = driverGet;
        }

        /// <inheritdoc />
        public bool IsDriverIntialized() => this.BaseDriver != null;

        /// <inheritdoc />
        public abstract object Get();

        /// <summary>
        /// Cleanup the driver
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Cleanup the driver
        /// </summary>
        /// <param name="disposing">Dispose managed objects</param>
        protected virtual void Dispose(bool disposing)
        {
            // Only dealing with managed objects
            if (disposing)
            {
                this.DriverDispose();
            }
        }

        /// <summary>
        /// Dispose driver specific objects
        /// </summary>
        protected abstract void DriverDispose();

        /// <summary>
        /// Get the underlying driver
        /// </summary>
        /// <returns>The underlying driver</returns>
        protected object GetBase()
        {
            // Initialize the driver if we haven't already
            if (this.BaseDriver == null)
            {
                this.BaseDriver = this.GetDriver();
            }

            return this.BaseDriver;
        }
    }
}
