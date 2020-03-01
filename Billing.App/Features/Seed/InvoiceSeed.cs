using System;
using System.Threading;
using System.Threading.Tasks;
using Billing.Domain.Invoices;
using MediatR;

namespace Billing.App.Features.Seed
{
    public partial class InvoiceSeed
    {
        public class Handler : IRequestHandler<Command>
        {
            private readonly IRepository<InvoiceId, Invoice> _repository;

            public Handler(IRepository<InvoiceId, Invoice> repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var invoice = new Invoice(new InvoiceId(Guid.NewGuid()));
                await _repository.Save(invoice);
                return Unit.Value;
            }
        }
    }
}