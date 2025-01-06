namespace CompanyName.ProjectName.Domain.SeedWork;

public interface IRepository : IQueryService
{
}
public interface IRepository<TEntity, TKey> : IRepository, IQueryService<TEntity, TKey>
    where TEntity : IAggregateRoot<TKey>
    where TKey : struct
{
    Task<TEntity> InsertAsync(TEntity entity);
    void Update(TEntity entity);
    Task DeleteAsync(TKey key);
    void Delete(TEntity entity);
}

