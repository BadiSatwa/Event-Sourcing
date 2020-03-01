using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.Domain;
using MediatR;

namespace Billing.App
{
    public class MediatrEventDispatcher<TId> : IDomainEventsDispatcher<TId>
    {
        private readonly IMediator _mediator;

        public MediatrEventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Dispatch(IReadOnlyCollection<IDomainEvent<TId>> events)
        {
            foreach (var domainEvent in events)
                await _mediator.Publish(domainEvent);

        }
    }
}
