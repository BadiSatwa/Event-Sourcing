namespace Billing.Domain.Invoices
{
    public class ExpenseAdded : DomainEventBase<InvoiceId>
    {
        public ExpenseAdded(InvoiceId aggregateRootId, int position, Money value, string description) : base(aggregateRootId)
        {
            Position = position;
            Value = value;
            Description = description;
        }

        public int Position { get; }
        public Money Value { get;  }
        public string Description { get; }
        public override int EventTypeVersion => 1;
    }
}