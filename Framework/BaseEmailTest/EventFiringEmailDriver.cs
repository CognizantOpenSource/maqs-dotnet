//--------------------------------------------------
// <copyright file="EventFiringEmailDriver.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>The basic database interactions</summary>
//--------------------------------------------------
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System;
using System.Collections.Generic;

namespace CognizantSoftvision.Maqs.BaseEmailTest
{
    /// <summary>
    /// Wraps the basic database interactions
    /// </summary>
    public class EventFiringEmailDriver : EmailDriver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventFiringEmailDriver" /> class
        /// </summary>
        /// <inheritdoc select="param" />
        public EventFiringEmailDriver(string host, string username, string password, int port, int serverTimeout = 10000, bool isSSL = true, bool skipSslCheck = false)
            : base(host, username, password, port, serverTimeout, isSSL, skipSslCheck)
        {
            this.EmailActionEvent?.Invoke(this, $"Connect to email with user '{username}' on host '{host}', port '{port}'");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventFiringEmailDriver" /> class
        /// </summary>
        /// <inheritdoc select="param" />
        public EventFiringEmailDriver(Func<ImapClient> setupEmailBaseConnectionOverride)
            : base(setupEmailBaseConnectionOverride)
        {
            this.EmailActionEvent?.Invoke(this, $"Connect to email with function '{setupEmailBaseConnectionOverride.Method.Name}'");
        }

        /// <summary>
        /// Email event
        /// </summary>
        public event EventHandler<string> EmailEvent;

        /// <summary>
        /// Email action event
        /// </summary>
        public event EventHandler<string> EmailActionEvent;

        /// <summary>
        /// Email error event
        /// </summary>
        public event EventHandler<string> EmailErrorEvent;

        /// <inheritdoc /> 
        public override bool CanAccessEmailAccount()
        {
            try
            {
                bool canAccess = base.CanAccessEmailAccount();
                this.OnEvent($"Access account check returned {canAccess}");
                return canAccess;
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override List<string> GetMailBoxNames()
        {
            try
            {
                this.OnEvent("Get mailbox names");
                return base.GetMailBoxNames();
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override IMailFolder GetMailbox(string mailbox)
        {
            try
            {
                this.OnEvent($"Get mailbox named '{mailbox}'");
                return base.GetMailbox(mailbox);
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override void SelectMailbox(string mailbox)
        {
            try
            {
                this.OnActionEvent($"Select mailbox named '{mailbox}'");
                base.SelectMailbox(mailbox);
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override void CreateMailbox(string newMailBox)
        {
            try
            {
                this.OnActionEvent($"Create mailbox named '{newMailBox}'");
                base.CreateMailbox(newMailBox);
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override MimeMessage GetMessage(string uid, bool headerOnly = false, bool markRead = false)
        {
            try
            {
                this.OnEvent($"Get message with uid '{uid}',  get header only '{headerOnly}' and mark as read '{markRead}'");
                return base.GetMessage(uid, headerOnly, markRead);
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc />  
        public override List<MimeMessage> GetAllMessageHeaders(string mailBox)
        {
            try
            {
                this.OnEvent($"Get all message headers from '{mailBox}'");
                return base.GetAllMessageHeaders(mailBox);
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override void DeleteMessage(MimeMessage message)
        {
            try
            {
                this.OnActionEvent($"Delete message '{message.Subject}' from '{message.From}' received '{message.Date}'");
                base.DeleteMessage(message);
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override void DeleteMessage(string uid)
        {
            try
            {
                this.OnActionEvent($"Delete message with uid '{uid}' from mailbox '{this.CurrentMailBox}'");
                base.DeleteMessage(uid);
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override void MoveMailMessage(MimeMessage message, string destinationMailbox)
        {
            try
            {
                this.OnActionEvent($"Move message '{message.Subject}' from '{message.From}' received '{message.Date}' to mailbox '{destinationMailbox}'");
                base.MoveMailMessage(message, destinationMailbox);
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override void MoveMailMessage(string uid, string destinationMailbox)
        {
            try
            {
                this.OnActionEvent($"Move message with uid '{uid}' from mailbox '{this.CurrentMailBox}' to mailbox '{destinationMailbox}'");
                base.MoveMailMessage(uid, destinationMailbox);
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override List<MimeEntity> GetAttachments(string uid)
        {
            try
            {
                this.OnEvent($"Get list of attachments for message with uid '{uid}' in mailbox '{this.CurrentMailBox}'");
                return base.GetAttachments(uid);
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override List<MimeEntity> GetAttachments(MimeMessage message)
        {
            try
            {
                this.OnEvent(
                    $"Get list of attachments for message '{message.Subject}' from '{message.From}' received '{message.Date}' in mailbox '{this.CurrentMailBox}'");
                return base.GetAttachments(message);
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override List<string> DownloadAttachments(MimeMessage message, string downloadFolder)
        {
            try
            {
                this.OnActionEvent(
                    $"Download attachments for message '{message.Subject}' from '{message.From}' revived '{message.Date}' in mailbox '{this.CurrentMailBox}' to '{downloadFolder}'");
                return base.DownloadAttachments(message, downloadFolder);
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override List<MimeMessage> SearchMessages(SearchQuery condition, bool headersOnly = true, bool markRead = false)
        {
            try
            {
                this.OnActionEvent(
                    $"Search for messages in mailbox '{this.CurrentMailBox}' with search condition '{condition}', header only '{headersOnly}' and mark as read '{markRead}'");
                return base.SearchMessages(condition, headersOnly, markRead);
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override List<string> GetContentTypes(MimeMessage message)
        {
            try
            {
                this.OnEvent($"Get list of content types for message '{message.Subject}' from '{message.From}' received '{ message.Date}'");
                return base.GetContentTypes(message);
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <inheritdoc /> 
        public override string GetBodyByContentTypes(MimeMessage message, string contentType)
        {
            try
            {
                this.OnEvent($"Get '{contentType}' content for message '{message.Subject}' from '{message.From}' received '{message.Date}'");
                string body = base.GetBodyByContentTypes(message, contentType);
                this.OnEvent($"Got message body:\r\n{body}");
                return body;
            }
            catch (Exception ex)
            {
                this.RaiseErrorMessage(ex);
                throw;
            }
        }

        /// <summary>
        /// Email event
        /// </summary>
        /// <param name="message">The event message</param>
        protected virtual void OnEvent(string message)
        {
            this.EmailEvent?.Invoke(this, message);
        }

        /// <summary>
        /// Email action event
        /// </summary>
        /// <param name="message">The event message</param>
        protected virtual void OnActionEvent(string message)
        {
            this.EmailActionEvent?.Invoke(this, message);
        }

        /// <summary>
        /// Email error event
        /// </summary>
        /// <param name="message">The event error message</param>
        protected virtual void OnErrorEvent(string message)
        {
            this.EmailErrorEvent?.Invoke(this, message);
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
