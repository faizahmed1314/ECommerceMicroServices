namespace Ordering.Domain.ValueObjects
{
    public record Address
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string? EmailAddress { get; }
        public string? AddressLine { get; }
        public string Street { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string PostalCode { get; init; }
        public string Country { get; init; }
        //public Address(string street, string city, string state, string postalCode, string country)
        //{
        //    Street = street;
        //    City = city;
        //    State = state;
        //    PostalCode = postalCode;
        //    Country = country;
        //}
    }
}
