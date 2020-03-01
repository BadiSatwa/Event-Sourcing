using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Billing.App.Features.QueryModel
{
    public partial class RecreateQueryModel
    {
        public class Handler : IRequestHandler<Command>
        {
            private readonly IQueryModelManager _queryModelManager;

            public Handler(IQueryModelManager queryModelManager)
            {
                _queryModelManager = queryModelManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _queryModelManager.RecreateModel();
                return Unit.Value;
            }
        }
    }
}