using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Domain;
using Billing.Infrastructure.Shared;
using EventStore.ClientAPI;

namespace Billing.Infrastructure.EventStore
{
    public class EventStore<TId> : IEventStore<TId>
    {
        private readonly IEventStoreConnection _connection;

        public EventStore(IEventStoreConnection connection)
        {
            _connection = connection;
        }

        public async IAsyncEnumerable<IDomainEvent<TId>> GetEvents(TId id)
        {
            var events = await _connection.ReadStreamEventsForwardAsync(id.ToString(), StreamPosition.Start, 1000, false);
            foreach (var @event in events.Events)
            {
                var metadata = @event.Event.Metadata.AsString().ToObject<EventMetadata>();
                var eventType = Type.GetType(metadata.EventType);
                var domainEvent = @event.Event.Data.AsString().ToObject<IDomainEvent<TId>>(eventType);
                yield return domainEvent;
            }
        }

        public async Task AppendEvents(TId streamId, IReadOnlyCollection<IDomainEvent<TId>> events, long expectedVersion)
        {
            var storeEvents = CreateEvents(events);
            await _connection.AppendToStreamAsync(streamId.ToString(), expectedVersion - 2, storeEvents);
        }

        private IEnumerable<EventData> CreateEvents(IReadOnlyCollection<IDomainEvent<TId>> events)
        {
            foreach (var domainEvent in events)
            {
                var @event = new EventData(
                    Guid.NewGuid(),
                    domainEvent.GetType().Name,
                    true,
                    domainEvent.ToJson().ToBytes(),
                    new EventMetadata(domainEvent.GetType().AssemblyQualifiedName).ToJson().ToBytes()
                    );
                yield return @event;
            }
        }
    }
}