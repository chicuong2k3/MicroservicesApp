

namespace Ordering.Application.Dtos;

public record UpdateAddressDto(
        string City,
        string District,
        string Town,
        string? AddressLine
);

public record UpdatePaymentDto(
        string Title,
        int PaymentMethod
);

public record UpdateOrderItemDto(
    Guid ProductId,
    int ProductVariantId,
    double Price,
    int Quantity);

