using CompanyName.ProjectName.Infrastructure.Configurations;
using Minio.AspNetCore;

namespace CompanyName.ProjectName.Presenter.DependencyInjection;

public static class AddConfiguredMinioInjection
{
    public static IServiceCollection AddConfiguredMinio(this IServiceCollection services, IConfiguration configuration)
    {
        var minioOptions = new MinioConfig();
        services.Configure<MinioConfig>(configuration.GetSection(MinioConfig.Key));
        services.AddBhMinio(options =>
        {
            options.Region = "";
            options.Endpoint = minioOptions.Connection;
            options.BucketName = minioOptions.RootBucketName;
            options.SecretKey = minioOptions.SecretKey;
            options.AccessKey = minioOptions.AccessKey;
            options.ConfigureClient(client =>
            {
                client.SetAppInfo("ProjectName", "1.0.0");
                if (minioOptions.WithSSl)
                {
                    client.WithSSL();
                }
            });
        });
        return services;
    }
}


