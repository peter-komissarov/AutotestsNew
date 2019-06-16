using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TestBase.Helpers;
using TestBase.Repositories.Entities;

namespace TestBase.Repositories.Tables
{
    /// <summary>
    /// Таблица Invoices.
    /// </summary>
    public class InvoicesTable
    {
        private readonly string _connectionString;

        public InvoicesTable(string connectionString = null)
        {
            _connectionString = connectionString ?? ConfigurationHelper.TestConfig["ConnectionStrings:Epayments"];
        }

        /// <summary>
        /// Асинхронно возвращает единственную запись из таблицы Invoices по параметру UserId, либо null.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        public async Task<Invoices> GetByUserIdAsync(Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                LogHelper.WriteText($"SELECT TOP 1 (*) FROM Invoices WHERE UserId = '{userId}'");
                var invoice = (await connection
                    .GetListAsync<Invoices>(new {UserId = userId})
                    .ConfigureAwait(false))
                    .FirstOrDefault();

                LogHelper.WriteValue("Invoice founded", invoice);
                return invoice;
            }
        }
    }
}