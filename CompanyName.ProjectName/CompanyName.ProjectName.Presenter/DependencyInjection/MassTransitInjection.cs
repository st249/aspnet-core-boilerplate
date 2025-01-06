using CompanyName.ProjectName.Infrastructure.Configurations;
using MassTransit;

namespace CompanyName.ProjectName.Presenter.DependencyInjection;

public static class MassTransitInjection
{
    public static IServiceCollection AddConfiguredMassTransit(this IServiceCollection services,
        IConfiguration configuration)
    {
        var rabbitOption = new RabbitMqConfig();
        configuration.GetSection(RabbitMqConfig.Key).Bind(rabbitOption);
        services.AddMassTransit(x =>
        {
            x.SetEndpointNameFormatter(
                new PrefixEndpointNameFormatter("ProjectName"));


            //x.AddRequestClient<REQUEST_CLIENT_SAMPLE>();


            //x.AddConsumer<CONSUMER_SAMPLE>();


            x.UsingRabbitMq((context, config) =>
            {
                config.Host(rabbitOption.Host, rabbitOption.Port ?? 5672, rabbitOption.VirtualHost, h =>
                {
                    h.Username(rabbitOption.Username);
                    h.Password(rabbitOption.Password);
                    if (rabbitOption.ClusterEnabled && rabbitOption.ClusterNodes != null)
                        h.UseCluster(c =>
                        {
                            foreach (var node in rabbitOption.ClusterNodes)
                                c.Node(node);
                        });
                }
                );

                //SAMPLE FOR CONFIG RECIEVE ENDPOINT
                //config.ReceiveEndpoint(e =>
                //{
                //    e.Durable = !rabbitOption.ClusterEnabled;
                //    e.ConfigureConsumer<TikkieNotificationReceivedEventHandler>(context);
                //});

                config.ConfigureEndpoints(context);
            });
        }
        );
        return services;
    }
}


