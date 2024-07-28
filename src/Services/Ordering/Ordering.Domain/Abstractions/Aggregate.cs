
namespace Ordering.Domain.Abstractions
{
    public abstract class Aggregate<TKey> : Entity<TKey>, IAggregate<TKey>
    {
        private readonly List<IDomainEvent> domainEvents = new();
        public IReadOnlyList<IDomainEvent> DomainEvents => domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            domainEvents.Add(domainEvent);
        }

        public IDomainEvent[] ClearDomainEvents()
        {
            var events = domainEvents.ToArray();
            domainEvents.Clear();
            return events;
        }
    }
}
