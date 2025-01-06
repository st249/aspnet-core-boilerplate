using CompanyName.ProjectName.Domain.SeedWork.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace CompanyName.ProjectName.Domain.SeedWork;

public interface IUnitOfWork
{

    #region Repositories

    IScheduledJobLogRepository ScheduledJobLogRepository { get; }

    #endregion


    bool HasActiveTransaction { get; }
    IExecutionStrategy CreateExecutionStrategy();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitTransactionAsync(IDbContextTransaction transaction);
    void SaveChanges();
    Task SaveChangesAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
}

