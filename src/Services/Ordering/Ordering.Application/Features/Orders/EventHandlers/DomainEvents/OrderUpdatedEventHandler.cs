


using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;

namespace Ordering.Application.Features.Orders.EventHandlers.DomainEvents
{
    internal class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger) 
        : INotificationHandler<OrderUpdatedEvent>
    {
        public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event: {} handled.", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
