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
            using var connection = new SqlConnection(AppSettingsProvider.Configuration["ConnectionString:Epayments"]);
            var invoice = await connection
                .QueryFirstOrDefaultAsync<Invoices>("SELECT TOP (1) * FROM Invoices WHERE UserId = @userId", new { userId })
                .ConfigureAwait(false);
            LogProvider.WriteText($"SELECT TOP (1) * FROM Invoices WHERE UserId = {userId}{Environment.NewLine}{JsonProvider.Serialize(invoice)}");

            return invoice;
        }
    }
}