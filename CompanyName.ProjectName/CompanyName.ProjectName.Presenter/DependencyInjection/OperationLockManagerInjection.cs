using CompanyName.ProjectName.Domain.SeedWork.Utilities;

namespace CompanyName.ProjectName.Presenter.DependencyInjection
{
    public static class OperationLockManagerInjection
    {

        // It uses Redis Cache setting, so should be called after adding the StackExchangeRedisCache

        public static IServiceCollection AddOperationLockManager(this IServiceCollection services)
        {
            services.AddSingleton<IOperationLockManager, OperationLockManager>();

            return services;
        }
    }
}


