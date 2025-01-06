using CompanyName.ProjectName.Domain.SeedWork.RepositoryInterfaces;
using CompanyName.ProjectName.Infrastructure.Extentions;
using CompanyName.ProjectName.Infrastructure.Persistent.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace CompanyName.ProjectName.Infrastructure.Persistent;

public class UnitOfWork : IUnitOfWork
{
    private readonly ProjectNameContext _dbContext;
    private readonly IPublishEndpoint _publisher;
    private readonly IMediator _mediator;

    public UnitOfWork(ProjectNameContext dbContext, IPublishEndpoint publisher, IMediator mediator)
    {
        _dbContext = dbContext;
        _publisher = publisher;
        _mediator = mediator;
    }

    public bool HasActiveTransaction => _dbContext.HasActiveTransaction;

    public IExecutionStrategy CreateExecutionStrategy()
    {
        return _dbContext.Database.CreateExecutionStrategy();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _dbContext.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {

        await _dbContext.CommitTransactionAsync(transaction);
        await _publisher.DispatchDomainEventsAsync(_dbContext);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }


    #region Repositories

    private IScheduledJobLogRepository _scheduledJobLogRepository;
    public IScheduledJobLogRepository ScheduledJobLogRepository
    {
        get
        {
            if (_scheduledJobLogRepository == null)
                _scheduledJobLogRepository = new ScheduledJobLogRepository(_dbContext);
            return _scheduledJobLogRepository;
        }
    }

    #endregion
}


