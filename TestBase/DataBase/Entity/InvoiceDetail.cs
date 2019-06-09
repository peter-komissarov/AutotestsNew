using Dapper.Contrib.Extensions;

namespace TestBase.DataBase.Entity
{
    [Table("InvoiceDetail")]
    public class InvoiceDetail
    {
        [ExplicitKey]
        public int InvoiceId { get; set; }

        public string Detail { get; set; }
    }
}