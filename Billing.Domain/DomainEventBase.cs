using System;

namespace Billing.Domain
{
    public abstract class DomainEventBase<TId> : IDomainEvent<TId>
    {
        protected DomainEventBase(TId aggregateRootId)
        {
            AggregateRootId = aggregateRootId;
            CreateDateTime = DateTimeOffset.Now;
        }

        public TId AggregateRootId { get; }
        public DateTimeOffset CreateDateTime { get; }
        public abstract int EventTypeVersion { get; }
        
    }
}