using System;
using MediatR;

namespace Billing.App.Features.Invoices
{
    public partial class AddExpense
    {
        public class Command : IRequest
        {
            public Guid InvoiceId { get; set; }
        }
    }
}