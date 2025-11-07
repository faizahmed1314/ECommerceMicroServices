

namespace Ordering.Infrastucture.Data.Extensions
{
    internal class InitialData
    {
        public static List<Customer> Customers = new List<Customer>
        {
            Customer.Create(CustomerId.Of(new Guid("d9a7f6a1-6bcd-4b82-b2ee-f274a9a88f47")), "John Doe", "johndoe@gmail.com" ),
            Customer.Create(CustomerId.Of(new Guid("a3b2c1d4-e5f6-7a8b-9c0d-e1f2a3b4c5d6")), "Jane Smith", "JaneSmith@gmail.com" )
        };

    }
}
