﻿//--------------------------------------------------
// <copyright file="Products.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Model representing Products table</summary>
//--------------------------------------------------

using Dapper.Contrib.Extensions;

namespace DatabaseUnitTests.Models
{
    /// <summary>
    /// Class representing the products table.
    /// </summary>
    [Table("Products")]
    public class Products
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string ProductName { get; set; }
    }
}
