using System;

namespace Billing.App.Features.Invoices
{
    public partial class GetInvoices
    {
        public class Result
        {
            public Guid InvoiceId { get; set; }
            public int NumberOfExpenses { get; set; }
        }
    }
}