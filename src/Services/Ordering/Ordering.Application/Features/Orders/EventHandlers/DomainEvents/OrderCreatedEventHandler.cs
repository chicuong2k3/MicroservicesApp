
using MassTransit;
using Microsoft.FeatureManagement;
using Ordering.Domain.Events;

namespace Ordering.Application.Features.Orders.EventHandlers.DomainEvents
{
    internal class OrderCreatedEventHandler(
        ILogger<OrderCreatedEventHandler> logger,
        IFeatureManager featureManager,
        IPublishEndpoint publishEndpoint) 
        : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event: {domainEvent} handled.", domainEvent.GetType().Name);

            if (await featureManager.IsEnabledAsync("OrderFullfilment"))
            {
                var integrationEvent = domainEvent.Order.ToOrderDto();
                await publishEndpoint.Publish(integrationEvent, cancellationToken);
            }

        }
    }
}
