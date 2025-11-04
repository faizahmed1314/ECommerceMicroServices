namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public int PaymentMethod { get; }
        public string CardNumber { get; }
        public string? CardHolderName { get; }
        public string Expiration { get; }
        public string CVV { get; }

        protected Payment() { }
        private Payment(int paymentMethod, string cardNumber, string? cardHolderName, string expiration, string cvv)
        {
            PaymentMethod = paymentMethod;
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            Expiration = expiration;
            CVV = cvv;
        }

        public static Payment Of(int paymentMethod, string cardNumber, string? cardHolderName, string expiration, string cvv)
        {
            ArgumentException.ThrowIfNullOrEmpty(cardNumber);
            ArgumentException.ThrowIfNullOrEmpty(expiration);
            ArgumentException.ThrowIfNullOrEmpty(cvv);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);
            return new Payment(paymentMethod, cardNumber, cardHolderName, expiration, cvv);
        }
    }
}
