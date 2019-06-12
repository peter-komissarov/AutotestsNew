using System;

namespace TestBase.Repositories.Entities
{
    /// <summary>
    /// Сущность инвойса.
    /// </summary>
    public class Invoice
    {
        /// <summary>Идентификатор инвойса.</summary>
        public int InvoiceId { get; set; }

        /// <summary>
        /// Дата создания инвойса.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Дата оплаты инвойса.
        /// </summary>
        public DateTime? PayDate { get; set; }

        /// <summary>
        /// Дата закрытия инвойса.
        /// </summary>
        public DateTime? CloseDate { get; set; }

        /// <summary>
        /// Guid через который инвойс связан с внутренними и внешними транзакциями.
        /// </summary>
        public Guid OperationGuid { get; set; }

        /// <summary>Детали платежа.</summary>
        public string Details { get; set; }

        /// <summary>Идентификатор пакетной операции проведения инвойсов.</summary>
        public Guid BatchOperationGuid { get; set; }

        /// <summary>Идентификатор получателя.</summary>
        public string ReceiverIdentity { get; set; }

        /// <summary>Идентификатор отправителя.</summary>
        public string SenderIdentity { get; set; }

        /// <summary>Идентификатор пользователя.</summary>
        public Guid UserId { get; set; }

        /// <summary>Родительский инвойс.</summary>
        public int? ParentInvoiceId { get; set; }

        /// <summary>Дополнительные данные, видные только внутри системы.</summary>
        public string AdditionalData { get; set; }

        /// <summary>Внешняя транзакция.</summary>
        public string ExternalTransaction { get; set; }
    }
}