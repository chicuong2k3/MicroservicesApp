
namespace Ordering.Application.Dtos
{
    public record OrderItemDto(
        Guid OrderId,
        Guid ProductId,
        int ProductVariantId,
        double Price,
        int Quantity,
        double Total);
}
