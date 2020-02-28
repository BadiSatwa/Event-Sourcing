using System;

namespace Billing.App.Features.Invoices
{
    public partial class GetInvoice
    {
        public class Result
        {
            public Guid InvoiceId { get; set; }
        }
    }
}