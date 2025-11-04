

namespace Ordering.Domain.Models
{
    public class Customer : Entity<CustomerId>
    {
        public string Name { get; private set; }
        //public string FirstName { get; private set; }
        //public string LastName { get; private set; }
        public string Email { get; private set; }
        //public string PhoneNumber { get; private set; }
        //public Customer(string firstName, string lastName, string email, string phoneNumber)
        //{
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Email = email;
        //    PhoneNumber = phoneNumber;
        //}
    }
}
