namespace CompanyName.ProjectName.Infrastructure.Persistent;

public class BaseRepository<TEntity, TKey> :  IRepository<TEntity, TKey>
        where TEntity : Entity<TKey>, IAggregateRoot<TKey>
        where TKey : struct
{
    protected readonly ProjectNameContext _dbContext;
    protected readonly DbSet<TEntity> _dataSet;
    public BaseRepository(ProjectNameContext dbContext) 
    {
        _dbContext = dbContext ?? dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dataSet = dbContext.Set<TEntity>();
    }
    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _dataSet.CountAsync(filter);
    }

    public IQueryable<TEntity> Get()
    {
        return _dataSet.AsQueryable();
    }

    public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, int skipCount = 0, int maxPageSize = 20)
    {
        return await _dataSet.Where(filter)
            .Skip(skipCount)
            .Take(maxPageSize)
            .ToListAsync();
    }

    public async Task<TEntity?> GetAsync(TKey key)
    {
        return await _dataSet.FindAsync(key);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _dataSet.Where(filter).FirstOrDefaultAsync();
    }
    public async Task DeleteAsync(TKey key)
    {
        var entity = await _dataSet.FindAsync(key);
        _dataSet.Remove(entity);
    }

    public void Delete(TEntity entity)
    {
        _dataSet.Remove(entity);
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        if (entity.IsTransient())
        {
            var addedEntity = await _dataSet.AddAsync(entity);
            return addedEntity.Entity;

        }
        return entity;

    }

    public void Update(TEntity entity)
    {
        entity.LastModificationTime = DateTime.Now;
        _dataSet.Update(entity);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _dataSet.AnyAsync(filter); 
    }
}


