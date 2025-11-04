namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        private const int DefaultLength = 5;
        public string Value { get; }
        private OrderName(string value)
        {
            Value = value;
        }
        public static OrderName Of(string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);

            //if (string.IsNullOrWhiteSpace(value))
            //{
            //    throw new DomainException("OrderName value cannot be null or empty.");
            //}
            //if (value.Length > DefaultLength)
            //{
            //    throw new DomainException("OrderName value cannot exceed 5 characters.");
            //}
            return new OrderName(value);
        }
    }
}
