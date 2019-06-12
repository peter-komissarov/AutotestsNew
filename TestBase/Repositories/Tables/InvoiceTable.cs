using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using TestBase.Repositories.Entities;

namespace TestBase.Repositories.Tables
{
    /// <summary>
    /// Table [Invoice].
    /// </summary>
    public class InvoiceTable
    {
        private readonly string _connectionString;

        public InvoiceTable(string connectionString = Configuration.TestDatabase)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Returns record async from [Invoice]  table.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        public async Task<Invoice> GetByUserIdAsync(Guid userId)
        {
            const string sql = "SELECT TOP (1) * FROM [Invoices] WHERE UserId = @UserId";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                var invoice = await connection.QuerySingleOrDefaultAsync<Invoice>(sql, new {UserId = userId}).ConfigureAwait(false);
                return invoice;
            }
        }
    }
}