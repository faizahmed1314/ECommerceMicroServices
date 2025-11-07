

namespace Ordering.Infrastucture.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers = new List<Customer>
        {
            Customer.Create(CustomerId.Of(new Guid("d9a7f6a1-6bcd-4b82-b2ee-f274a9a88f47")), "John Doe", "johndoe@gmail.com" ),
            Customer.Create(CustomerId.Of(new Guid("a3b2c1d4-e5f6-7a8b-9c0d-e1f2a3b4c5d6")), "Jane Smith", "JaneSmith@gmail.com" )
        };

        public static IEnumerable<Product> Products = new List<Product>
        {
            Product.Create(ProductId.Of(new Guid("e1a2b3c4-d5e6-7f8a-9b0c-d1e2f3a4b5c6")), "Laptop",  1200.00m),
            Product.Create(ProductId.Of(new Guid("f6e5d4c3-b2a1-0f9e-8d7c-6b5a4e3d2c1b")), "Smartphone", 800.00m),
            Product.Create(ProductId.Of(new Guid("1a2b3c4d-5e6f-7a8b-9c0d-e1f2a3b4c5d6")), "Headphones", 150.00m),
            Product.Create(ProductId.Of(new Guid("9f8e7d6c-5b4a-3e2d-1c0b-a9b8c7d6e5f4")), "Monitor", 300.00m)
        };

        public static IEnumerable<Order> OrderWithItems
        {
            get
            {
                var address1 = Address.Of("joe", "doe", "johndoe@gmail.com", "123 Main St", "1", "CityA", "StateA", "12345", "CountryA");
                var address2 = Address.Of("jane", "smith", "JaneSmith@gmail.com", "456 Main St", "2", "CityB", "StateB", "12345", "CountryB");

                var payment1 = Payment.Of(1, "Credit Card", "TXN123456", "06/30", "123");
                var payment2 = Payment.Of(2, "Credit Card", "TXN123456", "10/28", "345");

                var order1 = Order.Create(
                    OrderId.Of(new Guid("11111111-2222-3333-4444-555555555555")),
                    CustomerId.Of(new Guid("d9a7f6a1-6bcd-4b82-b2ee-f274a9a88f47")),
                    OrderName.Of("Ord_1"),
                    shippingAddress: address1,
                    billingAddress: address1,
                    payment: payment1);
                order1.AddOrderItem(ProductId.Of(new Guid("e1a2b3c4-d5e6-7f8a-9b0c-d1e2f3a4b5c6")), 1200, 1);
                order1.AddOrderItem(ProductId.Of(new Guid("1a2b3c4d-5e6f-7a8b-9c0d-e1f2a3b4c5d6")), 150, 2);


                var order2 = Order.Create(
                    OrderId.Of(new Guid("66666666-7777-8888-9999-000000000000")),
                    CustomerId.Of(new Guid("a3b2c1d4-e5f6-7a8b-9c0d-e1f2a3b4c5d6")),
                    OrderName.Of("Ord_2"),
                    shippingAddress: address2,
                    billingAddress: address2,
                    payment: payment2);

                order2.AddOrderItem(ProductId.Of(new Guid("f6e5d4c3-b2a1-0f9e-8d7c-6b5a4e3d2c1b")), 800, 1);
                order2.AddOrderItem(ProductId.Of(new Guid("9f8e7d6c-5b4a-3e2d-1c0b-a9b8c7d6e5f4")), 300, 2);

                return new List<Order> { order1, order2 };
            }

        }
    }
}