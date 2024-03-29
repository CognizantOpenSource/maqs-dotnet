﻿//--------------------------------------------------
// <copyright file="EmailTestObject.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Holds email context data</summary>
//--------------------------------------------------
using CognizantSoftvision.Maqs.BaseTest;
using CognizantSoftvision.Maqs.Utilities.Logging;
using MailKit.Net.Imap;
using System;

namespace CognizantSoftvision.Maqs.BaseEmailTest
{
    /// <summary>
    /// Email test context data
    /// </summary>
    public class EmailTestObject : BaseTestObject, IEmailTestObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailTestObject" /> class
        /// </summary>
        /// <param name="emailConnection">The test's email connection</param>
        /// <param name="logger">The test's logger</param>
        /// <param name="fullyQualifiedTestName">The test's fully qualified test name</param>
        public EmailTestObject(Func<ImapClient> emailConnection, ILogger logger, string fullyQualifiedTestName) : base(logger, fullyQualifiedTestName)
        {
            this.ManagerStore.Add(typeof(EmailDriverManager).FullName, new EmailDriverManager(emailConnection, this));
        }

        /// <inheritdoc /> 
        public EmailDriverManager EmailManager
        {
            get
            {
                return this.ManagerStore.GetManager<EmailDriverManager>();
            }
        }

        /// <inheritdoc /> 
        public EmailDriver EmailDriver
        {
            get
            {
                return this.EmailManager.GetEmailDriver();
            }
        }

        /// <inheritdoc /> 
        public void OverrideEmailClient(Func<ImapClient> emailConnection)
        {
            this.EmailManager.OverrideDriver(emailConnection);
        }

        /// <inheritdoc /> 
        public void OverrideEmailClient(EmailDriver emailDriver)
        {
            this.EmailManager.OverrideDriver(emailDriver);
        }
    }
}
