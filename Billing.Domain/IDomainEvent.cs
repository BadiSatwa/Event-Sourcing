using MediatR;

namespace Billing.Domain
{
    public interface IDomainEvent<out TId> : INotification
    {
        TId AggregateRootId { get; }
    }
}