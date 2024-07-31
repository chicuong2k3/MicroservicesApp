
namespace Ordering.Application.Dtos;

public record CreateAddressDto(
        string City,
        string District,
        string Town,
        string? AddressLine
);
public record CreateOrderItemDto(
    Guid ProductId,
    int ProductVariantId,
    double Price,
    int Quantity);


public record CreatePaymentDto(
        string Title,
        int PaymentMethod
);
