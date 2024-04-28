namespace Ordering.Application.Dtos
{
    public record OrderDto(
        Guid Id,
        Guid CustomerId,
        string OrderName,
        AddressDto ShippingAddress,
        AddressDto BillingAddress,
        PaymentDto Payment,
        OrderStatus Status,
        List<OrderItemDto> OrderItems
        );

    public record OrderItemDto(
        Guid Id, 
        Guid ProductId,
        int Quantity, 
        decimal Price
        );

    public record AddressDto(
        string FirstName,
        string LastName,
        string EmailAddress,
        string AddressLine,
        string Country,
        string State,
        string ZipCode
        );

    public record PaymentDto(
        string CardName,
        string CardNumber,
        string Expiration, 
        string Cvv,
        int PaymentMethod
        );
}