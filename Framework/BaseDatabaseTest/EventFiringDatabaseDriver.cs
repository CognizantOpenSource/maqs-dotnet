//--------------------------------------------------
// <copyright file="EventFiringDatabaseDriver.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>The event firing database interactions</summary>
//--------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CognizantSoftvision.Maqs.BaseDatabaseTest
{
    /// <summary>
    /// Wrap basic firing database interactions
    /// </summary>
    public class EventFiringDatabaseDriver : DatabaseDriver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventFiringDatabaseDriver"/> class
        /// </summary>
        /// <inheritdoc select="param" />
        public EventFiringDatabaseDriver(string connectionType, string connectionString)
            : base(connectionType, connectionString)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventFiringDatabaseDriver" /> class
        /// </summary>
        /// <inheritdoc select="param" />
        public EventFiringDatabaseDriver(IDbConnection dataBaseConnectionOverride)
            : base(dataBaseConnectionOverride)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventFiringDatabaseDriver"/> class.
        /// </summary>
        protected EventFiringDatabaseDriver()
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
        public override int Execute(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            try
            {
                this.RaiseEvent("execute", sql);
                return base.Execute(sql, param, transaction, commandTimeout, commandType);
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override IEnumerable<dynamic> Query(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            try
            {
                this.RaiseEvent("query", sql);
                return base.Query(sql, param, transaction, buffered, commandTimeout, commandType);
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override IEnumerable<T> Query<T>(
            string sql,
            object param = null,
            IDbTransaction transaction = null,
            bool buffered = true,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            try
            {
                this.RaiseEvent("query", sql);
                return base.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override IEnumerable<T> Query<T>(Func<IDbConnection, IEnumerable<T>> actionToPerform)
        {
            try
            {
                // We currently do not have a built in way to get the query info from the function.
                // The user can utilize their own logger inside the function for a workaround
                this.RaiseEvent("query", "Performing Function Base Query");
                return base.Query(actionToPerform);
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override long Insert<T>(
            T entityToInsert,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            params string[] items)
        {
            try
            {
                this.RaiseEvent("insert", items);
                return base.Insert<T>(entityToInsert, transaction, commandTimeout);
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override bool Delete<T>(
            T entityToDelete,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            params string[] items)
        {
            try
            {
                this.RaiseEvent("delete", items);
                return base.Delete<T>(entityToDelete, transaction, commandTimeout);
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override bool Update<T>(
            T entityToUpdate,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            params string[] items)
        {
            try
            {
                this.RaiseEvent("update", items);
                return base.Update<T>(entityToUpdate, transaction, commandTimeout);
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        protected override void Dispose(bool disposing)
        {
            try
            {
                this.OnActionEvent("Release connection");
                base.Dispose(disposing);
                this.OnEvent("Released connection");
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
        /// Database action event
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
        /// <param name="query">The query string</param>
        private void RaiseEvent(string actionType, string query)
        {
            try
            {
                this.OnActionEvent($"Perform {actionType} with:\r\n{query}");
            }
            catch (Exception e)
            {
                this.OnErrorEvent($"Failed to log event because: {e}");
            }
        }

        /// <summary>
        /// Raise an event message
        /// </summary>
        /// <param name="actionType">The type of action</param>
        /// <param name="items">The items to log</param>
        private void RaiseEvent(string actionType, params string[] items)
        {
            try
            {
                StringBuilder builder = new StringBuilder();

                foreach (var item in items)
                {
                    builder.AppendLine(item);
                }

                this.OnActionEvent($"Perform {actionType} with:\r\n{builder}");
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
