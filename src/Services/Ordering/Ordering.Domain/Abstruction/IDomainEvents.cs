using MediatR;

namespace Ordering.Domain.Abstruction
{
    public interface IDomainEvents : INotification
    {
        Guid EventId => Guid.NewGuid();
        public DateTime OccurredOn => DateTime.UtcNow;
        public string EventType => GetType().AssemblyQualifiedName;
    }
}
