using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Domain;

namespace Billing.Infrastructure
{
    public interface IEventStore<TId>
    {
        IAsyncEnumerable<IDomainEvent<TId>> GetEvents(TId id);

        Task AppendEvents(TId streamId, IReadOnlyCollection<IDomainEvent<TId>> events, long expectedVersion);
    }
}