namespace CompanyName.ProjectName.Domain.SeedWork;
public interface IAggregateRoot : IEntity
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void RaiseDomainEvent(IDomainEvent eventItem);
    void RemoveDomainEvent(IDomainEvent eventItem);
    void ClearDomainEvents();
}

public interface IAggregateRoot<TKey> : IEntity<TKey>, IAggregateRoot
    where TKey : struct
{
    
}


