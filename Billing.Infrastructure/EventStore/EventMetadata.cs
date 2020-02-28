namespace Billing.Infrastructure.EventStore
{
    public class EventMetadata
    {
        public EventMetadata(string eventType)
        {
            EventType = eventType;
        }

        public string EventType { get; }
    }
}