using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Domain;

namespace Billing.App
{
    public interface IDomainEventsDispatcher<in TId>
    {
        Task Dispatch(IReadOnlyCollection<IDomainEvent<TId>> @events);
    }
}