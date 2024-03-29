﻿//--------------------------------------------------
// <copyright file="EventFiringMongoDBDriver.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>The event firing mongoDB collection interactions</summary>
//--------------------------------------------------
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace CognizantSoftvision.Maqs.BaseMongoTest
{
    /// <summary>
    /// Wrap basic firing database interactions
    /// </summary>
    /// <typeparam name="T">The mongo collection type</typeparam>
    public class EventFiringMongoDBDriver<T> : MongoDBDriver<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventFiringMongoDBDriver{T}" /> class
        /// </summary>
        /// <inheritdoc select="param" />
        public EventFiringMongoDBDriver(IMongoCollection<T> collection) : base(collection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventFiringMongoDBDriver{T}" /> class
        /// </summary>
        /// <inheritdoc select="param" />
        public EventFiringMongoDBDriver(string connectionString, string databaseName, string collectionString)
            : base(connectionString, databaseName, collectionString)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventFiringMongoDBDriver{T}" /> class
        /// </summary>
        /// <inheritdoc select="param" />
        public EventFiringMongoDBDriver(string collectionString)
             : base(collectionString)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventFiringMongoDBDriver{T}" /> class
        /// </summary>
        public EventFiringMongoDBDriver() : base()
        {
        }

        /// <summary>
        /// Database event
        /// </summary>
        public event EventHandler<string> DatabaseEvent;

        /// <summary>
        /// Database action event
        /// </summary>
        public event EventHandler<string> DatabaseActionEvent;

        /// <summary>
        /// database error event
        /// </summary>
        public event EventHandler<string> DatabaseErrorEvent;

        /// <inheritdoc /> 
        public override List<T> ListAllCollectionItems()
        {
            try
            {
                this.RaiseEvent("list all collection items");
                return base.ListAllCollectionItems();
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override bool IsCollectionEmpty()
        {
            try
            {
                this.RaiseEvent("Is collection empty");
                return base.IsCollectionEmpty();
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override int CountAllItemsInCollection()
        {
            try
            {
                this.RaiseEvent("Count all items in collection");
                return base.CountAllItemsInCollection();
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <summary>
        /// Database event
        /// </summary>
        /// <param name="message">event message</param>
        protected virtual void OnEvent(string message)
        {
            this.DatabaseEvent?.Invoke(this, message);
        }

        /// <summary>
        /// Database event
        /// </summary>
        /// <param name="message">event message</param>
        protected virtual void OnActionEvent(string message)
        {
            this.DatabaseActionEvent?.Invoke(this, message);
        }

        /// <summary>
        /// Database error event
        /// </summary>
        /// <param name="message">The event error message</param>
        protected virtual void OnErrorEvent(string message)
        {
            this.DatabaseErrorEvent?.Invoke(this, message);
        }

        /// <summary>
        /// Raise an event message
        /// </summary>
        /// <param name="actionType">The type of action</param>
        private void RaiseEvent(string actionType)
        {
            try
            {
                this.OnEvent($"Performing {actionType}.");
            }
            catch (Exception e)
            {
                this.OnErrorEvent($"Failed to log event because: {e}");
            }
        }

        /// <summary>
        /// Raise an exception message
        /// </summary>
        /// <param name="e">The exception</param>
        private void RaiseErrorMessage(Exception e)
        {
            this.OnErrorEvent($"Failed because: {e.Message}{Environment.NewLine}{e}");
        }
    }
}