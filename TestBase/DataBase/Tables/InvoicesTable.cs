using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TestBase.DataBase.Entities;

namespace TestBase.DataBase.Tables
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
        public async Task<Invoices> GetByUserIdAsync(Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var invoices = await connection.GetListAsync<Invoices>(new {UserId = userId}).ConfigureAwait(false);
                return invoices.FirstOrDefault();
            }
        }
    }
}