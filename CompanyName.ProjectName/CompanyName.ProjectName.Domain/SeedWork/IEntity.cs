namespace CompanyName.ProjectName.Domain.SeedWork;

public interface IEntity
{
}

public interface IEntity<TKey> : IEntity, IEquatable<IEntity<TKey>>
    where TKey : struct
{
    TKey Id { get; }
    DateTime CreationTime { get; }
    DateTime? LastModificationTime { get; }
    void SetLastModificationTime();
}



