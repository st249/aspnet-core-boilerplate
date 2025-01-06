using CompanyName.ProjectName.Infrastructure.Persistent;
using System.Data;
using static CompanyName.ProjectName.Infrastructure.Persistent.ProjectNameContext;

namespace CompanyName.ProjectName.Infrastructure.Persistent;

public class BaseQueryService<TEntity, TKey> : IQueryService<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    protected readonly ProjectNameReadOnlyContext _dbContext;
    protected readonly DbSet<TEntity> _dataSet;
    protected IQueryable<TEntity> _query;
    protected readonly IDbConnectionFactory _dbConnectionFactory;
    public BaseQueryService(ProjectNameReadOnlyContext dbContext, IDbConnectionFactory dbConnection)
    {
        _dbContext = dbContext ?? dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dataSet = dbContext.Set<TEntity>();
        _query = _dataSet.AsNoTracking().AsQueryable();
        _dbConnectionFactory = dbConnection;
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _query.AnyAsync(filter);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _query.CountAsync(filter);
    }

    public IQueryable<TEntity> Get()
    {
        return _query.AsQueryable();
    }

    public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, int skipCount = 0, int maxPageSize = 20)
    {
        return await _query.Where(filter)
            .Skip(skipCount)
            .Take(maxPageSize)
            .ToListAsync();
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _query.Where(filter).FirstOrDefaultAsync();
    }
}

