using System;
using System.Data.SqlClient;
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
        public async ValueTask<Invoices> GetByUserIdAsync(Guid userId)
        {
            LogProvider.WriteText($"SELECT TOP (1) * FROM Invoices WHERE UserId = '{userId}'");

            using var connection = new SqlConnection(AppSettingsProvider.Configuration["ConnectionString:Epayments"]);
            var invoice = await connection
                .QueryFirstOrDefaultAsync<Invoices>("SELECT TOP (1) * FROM Invoices WHERE UserId = @userId", new {userId})
                .ConfigureAwait(false);

            return invoice;
        }
    }
}