using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using TestBase.DataBase.Entity;

namespace TestBase.DataBase.Tables
{
    /// <summary>
    /// Table [Invoice]
    /// </summary>
    public class InvoiceTable
    {
        /// <summary>
        /// Returns record async from [Invoice]  table
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="connectionString">Stroke string for connecting to database</param>
        public async Task<Invoice> GetInvoiceByUserIdAsync(Guid userId, string connectionString = Configuration.TestDatabase)
        {
            const string sql = "SELECT TOP (1) * FROM [Invoices] WHERE UserId = @UserId";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                var invoice = await connection.QuerySingleOrDefaultAsync<Invoice>(sql, new {UserId = userId}).ConfigureAwait(false);

                return invoice;
            }
        }
    }
}