using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using TestBase.Repositories.Entities;

namespace TestBase.Repositories.Tables
{
    /// <summary>
    /// Таблица Invoices.
    /// </summary>
    public class InvoicesTable
    {
        private readonly string _connectionString;

        public InvoicesTable(string connectionString = Configuration.TestDatabase)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Асинхронно возвращает единственную запись из таблицы Invoices по параметру UserId, либо null.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
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