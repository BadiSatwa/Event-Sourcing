using System;

namespace Billing.App.Features.Invoices
{
    public partial class CreateInvoice
    {
        public class Result
        {
            public Result(Guid invoiceId)
            {
                InvoiceId = invoiceId;
            }

            public Guid InvoiceId { get; }
        }
    }
}
