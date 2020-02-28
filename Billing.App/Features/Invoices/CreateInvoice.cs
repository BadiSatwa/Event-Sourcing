using System;
using System.Threading;
using System.Threading.Tasks;
using Billing.Domain.Invoices;
using MediatR;

namespace Billing.App.Features.Invoices
{
    public partial class CreateInvoice
    {
        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly IRepository<InvoiceId, Invoice> _repository;

            public Handler(IRepository<InvoiceId, Invoice> repository)
            {
                _repository = repository;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var invoice = new Invoice(new InvoiceId(Guid.NewGuid()));
                await _repository.Save(invoice);
                return new Result(invoice.AggregateRootId.Id);
            }
        }
    }
}