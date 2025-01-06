using CompanyName.ProjectName.Domain.SeedWork.Utilities;
using CompanyName.ProjectName.Infrastructure.Configurations;
using StackExchange.Redis;

namespace CompanyName.ProjectName.Presenter.DependencyInjection;

public static class RedisCacheInjection
{
    public static IServiceCollection AddConfiguredStackExchangeRedisCache(
       this IServiceCollection services,
       IConfiguration configuration)
    {
        var config = configuration.GetSection(RedisCacheConfig.Key).Get<RedisCacheConfig>();
        services.AddStackExchangeRedis(instancePrefix: "ProjectName", config);

        return services;
    }

    public static IServiceCollection AddStackExchangeRedis(this IServiceCollection services, string instancePrefix, RedisCacheConfig config)
    {
        ConfigurationOptions configurationOptions = new ConfigurationOptions();
        string[] connections = config.Connections;
        foreach (string hostAndPort in connections)
        {
            configurationOptions.EndPoints.Add(hostAndPort);
        }

        services.AddSingleton((IConnectionMultiplexer)ConnectionMultiplexer.Connect(configurationOptions));
        services.AddScoped((Func<IServiceProvider, ICache>)((IServiceProvider x) => new StackExchangeRedisCache(x.GetRequiredService<IConnectionMultiplexer>(), instancePrefix)));
        return services;
    }
}


