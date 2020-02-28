using System.Collections.Generic;
using System.Linq;

namespace Billing.Domain.Invoices
{
    public class Invoice : AggregateRoot<InvoiceId>
    {
        private readonly List<Expense> _expenses = new List<Expense>();

        public Invoice()
        { 
        }

        public Invoice(InvoiceId id)
        {
            AggregateRootId = id;
            RegisterEvent(new InvoiceCreated(AggregateRootId));
        }

        public void AddExpense(Money value, string description)
        {
            var maxPosition = _expenses.Select(e => e.Position).DefaultIfEmpty(0).Max();
            var expense = new Expense(maxPosition + 1, value, description);
            _expenses.Add(expense);
            
            RegisterEvent(new ExpenseAdded(AggregateRootId, expense.Position, expense.Value, expense.Description));
        }

        public void Apply(InvoiceCreated @event) => AggregateRootId = @event.AggregateRootId;

        public void Apply(ExpenseAdded @event) => _expenses.Add(new Expense(@event.Position, @event.Value, @event.Description));
    }
}