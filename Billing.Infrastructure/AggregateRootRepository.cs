using System;
using System.Threading.Tasks;
using Billing.App;
using Billing.Domain;

namespace Billing.Infrastructure
{
    public class AggregateRootRepository<TId, TAgg> : IRepository<TId, TAgg> 
        where TAgg : AggregateRoot<TId>, new()
        where TId : IAggregateRootId
    {
        private readonly IDomainEventsDispatcher<TId> _dispatcher;
        private readonly IEventStore<TId> _eventStore;

        public AggregateRootRepository(IDomainEventsDispatcher<TId> dispatcher, IEventStore<TId> eventStore)
        {
            _dispatcher = dispatcher;
            _eventStore = eventStore;
        }

        public async Task<TAgg> GetById(TId id)
        {
            var events = _eventStore.GetEvents(id);
            var aggregate = new TAgg();
            await foreach (var @event in events) aggregate.Apply(@event);
            return aggregate;
        }

        public async Task Save(TAgg aggregate)
        {
            await _eventStore.AppendEvents(aggregate.AggregateRootId, aggregate.DomainEvents, aggregate.Version);
            await _dispatcher.Dispatch(aggregate.DomainEvents);
        }
    }
}
