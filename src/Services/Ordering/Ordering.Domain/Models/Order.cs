

namespace Ordering.Domain.Models
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public CustomerId CustomerId { get; private set; }
        public OrderName OrderName { get; private set; }
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

        public static Order Create(OrderId id, CustomerId customerId, OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment, List<OrderItem> orderItems)
        {
            //ArgumentException.ThrowIfNullOrEmpty(orderItems);
            var order = new Order
            {
                Id = id,
                CustomerId = customerId,
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending
            };

            //order.AddDomainEvent(new OrderCreatedEvent(order));
            //order._orderItems.AddRange(orderItems);
            return order;
        }

        public void Update(OrderName orderName, Address shippingAddress, Address BillingAddress, Payment payment, OrderStatus status)
        {
            OrderName = orderName;
            ShippingAddress = shippingAddress;
            this.BillingAddress = BillingAddress;
            Payment = payment;
            Status = status;
            //AddDomainEvent(new OrderUpdatedEvent(this));
        }

        public void AddOrderItem(ProductId productId, decimal price, int quantity)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var orderItem = new OrderItem(Id, productId, price, quantity);
            _orderItems.Add(orderItem);
            //AddDomainEvent(new OrderItemAddedEvent(this, orderItem));
        }

        public void RemoveOrderItem(OrderItemId orderItemId)
        {
            var orderItem = _orderItems.FirstOrDefault(oi => oi.Id == orderItemId);
            if (orderItem != null)
            {
                _orderItems.Remove(orderItem);
                //AddDomainEvent(new OrderItemRemovedEvent(this, orderItem));
            }
        }
    }
}
