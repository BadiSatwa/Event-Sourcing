using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Billing.App.Features.Seed
{
    public partial class ClearQueryModel
    {
        public class Handler : IRequestHandler<Command>
        {
            private readonly IStorage _storage;

            public Handler(IStorage storage)
            {
                _storage = storage;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _storage.ClearStorage();
                return Unit.Value;
            }
        }
    }
}