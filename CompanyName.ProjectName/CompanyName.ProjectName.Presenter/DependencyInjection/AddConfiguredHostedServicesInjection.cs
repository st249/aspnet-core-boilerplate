using CompanyName.ProjectName.BackgroundTasks.BackgroundServices;

namespace CompanyName.ProjectName.Presenter.DependencyInjection;

public static class AddConfiguredHostedServicesInjection
{
    public static IServiceCollection AddConfiguredHostedServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<SampleBackgroundService>();
        return services;
    }
}


