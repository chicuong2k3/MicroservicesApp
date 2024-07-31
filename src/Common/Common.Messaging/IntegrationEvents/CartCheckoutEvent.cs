namespace Common.Messaging.IntegrationEvents;

public class CartItem
{
    public Guid ProductId { get; set; }
    public int VariantId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
}
public class CartCheckoutEvent : IntegrationEvent
{
    // Cart Items
    public List<CartItem> CartItems { get; set; } = new();

    // Customer's Info
    public string UserName { get; set; } = default!;

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    // Shipping Address
    public string City { get; set; } = default!;
    public string District { get; set; } = default!;
    public string Town { get; set; } = default!;
    public string? AddressLine { get; set; }

    // Payment
    public string PaymentTitle { get; set; } = default!;
    public int PaymentMethod { get; set; }

    public double TotalPrice { get; set; }
}
