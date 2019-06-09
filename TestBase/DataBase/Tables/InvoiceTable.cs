using System.Threading.Tasks;
using TestBase.DataBase.Entity;

namespace TestBase.DataBase.Tables
{
    /// <summary>
    /// Table [Invoice]
    /// </summary>
    public class InvoiceTable
    {
        /// <summary>
        /// Select record async from [Invoice]  table
        /// </summary>
        /// <param name="invoiceId">Invoice id</param>
        /// <param name="connectionString">Stroke string for connecting to database</param>
        /// <returns>Success query result or not?</returns>
        public async Task<Invoice> GetByInvoiceIdAsync(int invoiceId, string connectionString = Configuration.TestDatabase)
        {
            using (var client = new DbClient(connectionString))
            {
                var entity = await client
                    .GetByIdentifierAsync<Invoice>(invoiceId)
                    .ConfigureAwait(false);

                return entity;
            }
        }
    }
}