using CompanyName.ProjectName.Application.Behaviors;
using CompanyName.ProjectName.Application.Commands.Sample;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyName.ProjectName.Presenter.DependencyInjection;

public static class MediatRInjection
{
    public static IServiceCollection AddConfiguredMediatR(this IServiceCollection services)
    {
        // Handlers

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(SampleCommand).Assembly);
        });

        // Generic behaviors

        // Validation behaviors
        services.AddValidatorsFromAssemblyContaining<SampleCommandValidator>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TranasactionBehavior<,>));

        services.AddScoped<CommandHelper>();

        return services;
    }
}


