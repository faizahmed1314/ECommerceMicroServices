
namespace Ordering.Domain.Models
{
    public class OrderItem : Entity<Guid>
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public OrderItem(Guid orderId, Guid productId, decimal price, int quantity)
        {
            OrderId = orderId;
            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }
    }
}
