using System;

namespace Billing.Domain
{
    public interface IAggregateRootId
    {
        Guid Id { get; }
    }
}