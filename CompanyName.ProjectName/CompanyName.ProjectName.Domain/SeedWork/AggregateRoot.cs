namespace CompanyName.ProjectName.Domain.SeedWork;

public abstract class AggregateRoot : Entity, IAggregateRoot
{
    protected List<IDomainEvent> _domainEvents;
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly() ?? new List<IDomainEvent>().AsReadOnly();
    protected AggregateRoot() : base()
    {
        _domainEvents = new List<IDomainEvent>();
    }
    public void RaiseDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents ??= new List<IDomainEvent>();
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}

public abstract class AggregateRoot<Tkey> : Entity<Tkey>, IAggregateRoot<Tkey>
    where Tkey : struct
{
    protected List<IDomainEvent> _domainEvents;
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly() ?? new List<IDomainEvent>().AsReadOnly();

    protected AggregateRoot() : base()
    {
        _domainEvents = new List<IDomainEvent>();
    }

    public void RaiseDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents ??= new List<IDomainEvent>();
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}

