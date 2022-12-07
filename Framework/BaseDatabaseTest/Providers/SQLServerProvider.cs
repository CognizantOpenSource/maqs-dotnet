//--------------------------------------------------
// <copyright file="SQLServerProvider.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>SQLServerProvider class</summary>
//--------------------------------------------------

using Microsoft.Data.SqlClient;

namespace CognizantSoftvision.Maqs.BaseDatabaseTest.Providers
{
    /// <summary>
    /// The SQL server provider.
    /// </summary>
    public class SqlServerProvider : IProvider<SqlConnection>
    {
        /// <summary>
        /// Method used to create a new connection for SQL server databases
        /// </summary>
        /// <param name="connectionString"> The connection string. </param>
        /// <returns> The <see cref="SqlConnection"/> connection client. </returns>
        public SqlConnection SetupDataBaseConnection(string connectionString)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = connectionString
            };

            return connection;
        }
    }
}
