namespace Billing.Domain.Invoices
{
    public class Expense
    {
        public Expense(int position, Money value, string description)
        {
            Position = position;
            Value = value;
            Description = description;
        }

        public int Position { get; }
        public Money Value { get; }
        public string Description { get;  }
    }
}