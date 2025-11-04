

namespace Ordering.Domain.ValueObjects
{
    public record CustomerId
    {
        public Guid Value { get; set; }
        public CustomerId(Guid guid)
        {
            Value = guid;
        }

        public static CustomerId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("CustomerId value cannot be empty GUID.");
            }

            return new CustomerId(value);
        }
    }
}
