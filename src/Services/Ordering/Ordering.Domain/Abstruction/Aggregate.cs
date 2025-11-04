namespace Ordering.Domain.Abstruction
{
    public abstract class Aggregate<TID> : Entity<TID>, IAggregate<TID>
    {
        private readonly List<IDomainEvents> _domainEvents = new();
        public IReadOnlyList<IDomainEvents> DomainEvents => _domainEvents.AsReadOnly();
        public void AddDomainEvent(IDomainEvents domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        public IDomainEvents[] ClearDomainEvents()
        {
            var events = _domainEvents.ToArray();
            _domainEvents.Clear();
            return events;
        }
    }
}
