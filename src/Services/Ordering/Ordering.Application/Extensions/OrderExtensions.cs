

using Ordering.Application.Features.Orders.Commands;
using System.Data.Common;

namespace Ordering.Application.Extensions
{
    public static class OrderExtensions
    {
        
        public static List<OrderDto> ToOrderDtos(this IEnumerable<Order>? orders)
        {
            var dtos = new List<OrderDto>();

            if (orders != null)
            {
                foreach (var order in orders)
                {
                    var shippingAddress = new AddressDto
                    (
                        order.ShippingAddress.City,
                        order.ShippingAddress.District,
                        order.ShippingAddress.Town,
                        order.ShippingAddress.AddressLine
                    );

                    var payment = new PaymentDto
                    (
                        order.Payment.Title,
                        order.Payment.PaymentMethod,
                        order.Payment.DatePaid
                    );

                    var orderItemDtos = order.OrderItems
                        .Select(x => new OrderItemDto(x.OrderId.Value, x.ProductId.Value, x.ProductVariantId, x.Price, x.Quantity, x.Total))
                        .ToList();

                    dtos.Add(new OrderDto
                    (
                        order.Id.Value,
                        order.CustomerId.Value,
                        order.OrderName.Value,
                        shippingAddress,
                        payment,
                        orderItemDtos,
                        order.Status
                    ));
                }

            }


            return dtos;
        }

        public static OrderDto ToOrderDto(this Order order)
        {
            var shippingAddress = new AddressDto
                    (
                        order.ShippingAddress.City,
                        order.ShippingAddress.District,
                        order.ShippingAddress.Town,
                        order.ShippingAddress.AddressLine
                    );

            var payment = new PaymentDto
            (
                order.Payment.Title,
                order.Payment.PaymentMethod,
                order.Payment.DatePaid
            );

            var orderItemDtos = order.OrderItems
                .Select(x => new OrderItemDto(x.OrderId.Value, x.ProductId.Value, x.ProductVariantId, x.Price, x.Quantity, x.Total))
                .ToList();

            return new OrderDto
            (
                order.Id.Value,
                order.CustomerId.Value,
                order.OrderName.Value,
                shippingAddress,
                payment,
                orderItemDtos,
                order.Status
            );
        }
    }
}
