

namespace Ordering.Application.Dtos
{
    public record AddressDto(
        string City,
        string District,
        string Town,
        string? AddressLine);
}
