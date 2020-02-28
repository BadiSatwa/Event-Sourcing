using System.Collections.Generic;

namespace Billing.Domain
{
    public class AggregateRoot<TId> where TId : IAggregateRootId
    {
        private readonly List<IDomainEvent<TId>> _domainEvents = new List<IDomainEvent<TId>>();

        public TId AggregateRootId { get; protected set; }
        public long Version { get; protected set; }

        protected void RegisterEvent(IDomainEvent<TId> @event)
        {
            Version++;
            _domainEvents.Add(@event);
        }

        public IReadOnlyCollection<IDomainEvent<TId>> DomainEvents => _domainEvents;

        public void Apply(IDomainEvent<TId> @event)
        {
            ((dynamic) this).Apply((dynamic) @event);
            Version++;
        }
    }
}
