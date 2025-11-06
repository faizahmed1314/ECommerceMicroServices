namespace Ordering.Domain.Events
{
    public record OrderItemRemovedEvent(Order order, OrderItem OrderItem) : IDomainEvents
    {
    }
}
