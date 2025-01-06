namespace CompanyName.ProjectName.Domain.SeedWork;

public interface IQueryService
{
}
public interface IQueryService<TEntity, Tkey> : IQueryService
    where TEntity : IEntity<Tkey>
    where Tkey : struct
{
    IQueryable<TEntity> Get();
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);
    Task<IReadOnlyCollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, int skipCount = 0, int maxPageSize = 20);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter);
}


