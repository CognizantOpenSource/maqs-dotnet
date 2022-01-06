//--------------------------------------------------
// <copyright file="Users.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Model representing Users table</summary>
//--------------------------------------------------

using Dapper.Contrib.Extensions;

namespace DatabaseUnitTests.Models
{
    /// <summary>
    /// Model representing the users table
    /// </summary>
    [Table("Users")]
    public class Users
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }
    }
}
