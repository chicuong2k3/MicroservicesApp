
namespace Ordering.Application.Dtos
{
    public record PaymentDto(
        string Title,
        int PaymentMethod,
        DateTime DatePaid);
}
