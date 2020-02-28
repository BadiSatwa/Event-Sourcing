namespace Billing.Infrastructure.EventStore
{
    public class EventStoreOptions
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }    
    }
}