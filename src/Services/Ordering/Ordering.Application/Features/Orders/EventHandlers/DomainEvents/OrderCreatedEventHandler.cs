
using Ordering.Domain.Events;

namespace Ordering.Application.Features.Orders.EventHandlers.DomainEvents
{
    internal class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) 
        : INotificationHandler<OrderCreatedEvent>
    {
        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event: {} handled.", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
