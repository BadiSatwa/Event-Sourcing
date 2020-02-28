using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Billing.App.Features.Invoices
{
    public partial class GetInvoice
    {
        public class Handler : IRequestHandler<Command, Result>
        {
            public Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new Result { InvoiceId = request.InvoiceId });
            }
        }
    }
}