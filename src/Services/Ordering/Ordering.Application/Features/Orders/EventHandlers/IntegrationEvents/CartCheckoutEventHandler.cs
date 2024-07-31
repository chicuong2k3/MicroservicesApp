using Common.Messaging.IntegrationEvents;
using MassTransit;
using Ordering.Application.Features.Orders.Commands;

namespace Ordering.Application.Features.Orders.EventHandlers.IntegrationEvents
{
    public class CartCheckoutEventHandler(
        ISender sender, 
        ILogger<CartCheckoutEventHandler> logger) 
        : IConsumer<CartCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<CartCheckoutEvent> context)
        {
            logger.LogInformation("Integration Event: {integrationEvent} handled.", context.Message.GetType().Name);

            var command = CreateOrderCommandFrom(context.Message);
            await sender.Send(command);

        }

        private CreateOrderCommand CreateOrderCommandFrom(CartCheckoutEvent message)
        {
            var shippingAddress = new CreateAddressDto(
                message.City, 
                message.District, 
                message.Town, 
                message.AddressLine);

            var payment = new CreatePaymentDto(message.PaymentTitle, message.PaymentMethod);

            var orderItems = message.CartItems
                .Select(x => new CreateOrderItemDto(x.ProductId, x.VariantId, x.Price, x.Quantity))
                .ToList();

            return new CreateOrderCommand(
                message.UserName,
                message.UserName,
                shippingAddress,
                payment,
                orderItems
            );
        }
    }
}
