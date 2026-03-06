namespace Basket.API.Dto
{
    public class BasketCheckoutDto
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid CustomerId { get; set; }

        // Shipping Address & billing address
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        // Payment
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public DateTime Expiration { get; set; }
        public string CVV { get; set; }
        public int PaymentMethod { get; set; }


    }
}
