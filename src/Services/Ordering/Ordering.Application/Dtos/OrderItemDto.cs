namespace Ordering.Application.Dtos
{
    public record OrderItemDto(Guid OrderId, Guid ProductId, decimal UnitPrice, int Quantity);
}
