using System;
using MediatR;

namespace Billing.App.Features.Invoices
{
    public partial class GetInvoice
    {
        public class Command : IRequest<Result>
        {
            public Guid InvoiceId { get; set; }
        }
    }
}