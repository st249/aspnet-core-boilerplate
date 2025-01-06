using CompanyName.ProjectName.Domain.SeedWork;
using CompanyName.ProjectName.Domain.SeedWork.RepositoryInterfaces;
using CompanyName.ProjectName.Infrastructure.Persistent.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CompanyName.ProjectName.Presenter.DependencyInjection;

public static class AddRepositoriesInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddSingleton<ISqlServerDbConnectionFactory, SqlServerDbConnectionFactory>();
        services.AddSingleton<IPostgresDbConnectionFactory, PostgresDbConnectionFactory>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IScheduledJobLogRepository, ScheduledJobLogRepository>();


        return services;
    }
}


