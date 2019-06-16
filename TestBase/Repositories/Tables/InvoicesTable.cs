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
            LogHelper.WriteText($"Searching Invoice by UserId = '{userId}' in db...");

            using (var connection = new SqlConnection(ConfigHelper.Config["ConnectionStrings:Epayments"]))
            {
                var invoice = (await connection
                    .GetListAsync<Invoices>(new {UserId = userId})
                    .ConfigureAwait(false)).FirstOrDefault();

                return invoice;
            }
        }
    }
}