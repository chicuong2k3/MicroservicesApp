
using Ordering.Domain.Enums;

namespace Ordering.Application.Dtos;

public record PaymentDto(
        string Title,
        int PaymentMethod,
        DateTime DatePaid);

public record AddressDto(
        string City,
        string District,
        string Town,
        string? AddressLine);

public record OrderItemDto(
        Guid OrderId,
        Guid ProductId,
        int ProductVariantId,
        double Price,
        int Quantity,
        double Total);

public record OrderDto(
    Guid Id,
    Guid CustomerId,
    string OrderName,
    AddressDto ShippingAddress,
    PaymentDto Payment,
    List<OrderItemDto> OrderItems,
    OrderStatus Status
);
