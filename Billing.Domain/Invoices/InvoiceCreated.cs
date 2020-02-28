using System;

namespace Billing.Domain.Invoices
{
    public class InvoiceCreated : DomainEventBase<InvoiceId>
    {
        public InvoiceCreated(InvoiceId aggregateRootId) : base(aggregateRootId)
        {
        }

        public override int EventTypeVersion => 1;
    }
}