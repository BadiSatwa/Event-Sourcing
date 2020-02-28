using System;

namespace Billing.Domain.Invoices
{
    public class InvoiceId : IAggregateRootId
    {
        public InvoiceId(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public override string ToString()
        {
            return $"invoice-{Id}";
        }
    }

}
