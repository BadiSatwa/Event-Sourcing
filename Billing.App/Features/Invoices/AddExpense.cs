using System.Threading;
using System.Threading.Tasks;
using Billing.Domain;
using Billing.Domain.Invoices;
using MediatR;

namespace Billing.App.Features.Invoices
{
    public partial class AddExpense
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
                var invoice = await _repository.GetById(new InvoiceId(request.InvoiceId));
                invoice.AddExpense(new Money(), "taki tam expense");
                await _repository.Save(invoice);
                return Unit.Value;
            }
        }
    }
}