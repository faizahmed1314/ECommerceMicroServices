namespace Ordering.Domain.Abstruction
{

    public interface IAggregate<T> : IAggregate, IEntity<T>
    {
    }

    public interface IAggregate : IEntity
    {
        IReadOnlyList<IDomainEvents> DomainEvents { get; }
        IDomainEvents[] ClearDomainEvents();
    }
}
