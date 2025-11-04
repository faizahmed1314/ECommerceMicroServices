namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public int PaymentMethod { get; }
        public string CardNumber { get; }
        public string? CardHolderName { get; }
        public string Expiration { get; }
        public string CVV { get; }
    }
}
