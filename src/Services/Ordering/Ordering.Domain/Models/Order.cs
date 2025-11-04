

namespace Ordering.Domain.Models
{
    public class Order : Aggregate<Guid>
    {
        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public Guid CustomerId { get; private set; }
        public string OrderNumber { get; private set; }
        public Address ShippingAddress { get; private set; }
        public Address BillingAddress { get; private set; }
        public Payment Payment { get; private set; }
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount
        {
            get => OrderItems.Sum(x => x.Price * x.Quantity);
            private set { }
        }
        // Additional properties and methods related to Order can be added here
    }
}
