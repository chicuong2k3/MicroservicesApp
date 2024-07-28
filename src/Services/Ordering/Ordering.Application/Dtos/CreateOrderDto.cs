
namespace Ordering.Application.Dtos;

public record CreateOrderItemDto(
    Guid ProductId,
    int ProductVariantId,
    double Price,
    int Quantity);

public record CreateAddressDto(
        string City,
        string District,
        string Town,
        string? AddressLine
);

public record CreatePaymentDto(
        string Title,
        int PaymentMethod
);
