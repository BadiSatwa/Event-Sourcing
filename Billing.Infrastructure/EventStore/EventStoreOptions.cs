﻿namespace Billing.Infrastructure.EventStore
{
    public class EventStoreOptions
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }    
    }
}