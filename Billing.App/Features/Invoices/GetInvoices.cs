using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Billing.App.Features.Invoices
{
    public partial class GetInvoices
    {
        public class Handler : IRequestHandler<Command, IEnumerable<Result>>
        {
            private readonly IViewModelQuery<Empty, IEnumerable<Result>> _query;

            public Handler(IViewModelQuery<Empty, IEnumerable<Result>> query)
            {
                _query = query;
            }

            public Task<IEnumerable<Result>> Handle(Command request, CancellationToken cancellationToken)
            {
                return _query.Execute(Empty.Value);
            }
        }
    }
}