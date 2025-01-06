using CompanyName.ProjectName.Domain.Aggregates.ScheduledJobLogAggregate;
using CompanyName.ProjectName.Infrastructure.Extentions;
using MassTransit;
using Microsoft.EntityFrameworkCore.Storage;
using System.Transactions;

namespace CompanyName.ProjectName.Infrastructure.Persistent;

public partial class ProjectNameContext : DbContext
{
    public const string DEFAULT_SCHEMA = "ProjectName";
    //Here define DbSets<TEntites>
    public DbSet<ScheduledJobLog> ScheduledJobs { get; set; }

    private IDbContextTransaction _currentTransaction;

    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

    public bool HasActiveTransaction => _currentTransaction != null;

    public ProjectNameContext(DbContextOptions<ProjectNameContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly, t => t.GetInterfaces().Any(i =>
              i.IsGenericType &&
              i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync();

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}


