
using Ordering.Domain.Enums;

namespace Ordering.Application.Dtos
{
    public record OrderDto(
        Guid Id,
        Guid CustomerId,
        string OrderName,
        AddressDto ShippingAddress,
        PaymentDto Payment,
        List<OrderItemDto> OrderItems,
        OrderStatus Status
    );
}
