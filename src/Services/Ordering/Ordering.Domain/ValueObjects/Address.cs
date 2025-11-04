namespace Ordering.Domain.ValueObjects
{
    public record Address
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string EmailAddress { get; }
        public string AddressLine { get; }
        public string Street { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string PostalCode { get; init; }
        public string Country { get; init; }

        protected Address() { }
        private Address(string firstName, string lastName, string emailAddress, string addressLine, string street, string city, string state, string postalCode, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            AddressLine = addressLine;
            Street = street;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
        }

        public static Address Of(string firstName, string lastName, string? emailAddress, string? addressLine, string street, string city, string state, string postalCode, string country)
        {
            ArgumentException.ThrowIfNullOrEmpty(emailAddress);
            ArgumentException.ThrowIfNullOrEmpty(addressLine);

            return new Address(firstName, lastName, emailAddress, addressLine, street, city, state, postalCode, country);
        }
    }
}
