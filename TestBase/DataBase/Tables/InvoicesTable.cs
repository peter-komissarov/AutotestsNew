using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TestBase.DataBase.Entities;
using TestBase.Helpers;

namespace TestBase.DataBase.Tables
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
            LogProvider.WriteText($"Searching Invoice by UserId = '{userId}' in db...");

            using var connection = new SqlConnection(AppSettingsProvider.Configuration["ConnectionString:Epayments"]);
            var invoices = await connection
                .GetListAsync<Invoices>(new {UserId = userId})
                .ConfigureAwait(false);

            return invoices.FirstOrDefault();
        }
    }
}