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
        /// <summary>
        /// Асинхронно возвращает единственную запись из таблицы Invoices по параметру UserId, либо null.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        public async Task<Invoices> GetByUserIdAsync(Guid userId)
        {
            using (var connection = new SqlConnection(ConfigurationHelper.TestConfig["ConnectionStrings:Epayments"]))
            {
                var invoice = (await connection
                    .GetListAsync<Invoices>(new {UserId = userId})
                    .ConfigureAwait(false))
                    .FirstOrDefault();

                LogHelper.WriteValue($"Founded Invoice by UserId = '{userId}'", invoice);
                return invoice;
            }
        }
    }
}