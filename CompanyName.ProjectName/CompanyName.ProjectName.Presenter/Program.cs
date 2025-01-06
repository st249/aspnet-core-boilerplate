using CompanyName.ProjectName.Application.IntegrationEvents;
using CompanyName.ProjectName.Presenter.DependencyInjection;
using CompanyName.ProjectName.Presenter.Filters;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.OpenApi.Models;
using HttpRequest = CompanyName.ProjectName.Infrastructure.Utilities.HttpRequest;

var builder = WebApplication.CreateBuilder(args);

const string CorsPolicy = "CorsPolicy";

ConfigurationManager _configuration = builder.Configuration;
IWebHostEnvironment _env = builder.Environment;


#region Configure Services

builder.Host.UseSerilog((hostBuilderContext, LoggerConfiguration) =>
{
    LoggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration)
    .Filter.ByExcluding(c => FilterLog(c.Properties));
});

builder.Services.AddScoped(typeof(IntegrationEventStore));

builder.Services.AddDbContext<ProjectNameContext>(options =>
               options.UseSqlServer(_configuration.GetConnectionString("Default"), b => b.MigrationsAssembly("CompanyName.ProjectName.Infrastructure")));

builder.Services.AddDbContext<ProjectNameContext>(options =>
               options.UseSqlServer(_configuration.GetConnectionString("Readonly")));

builder.Services.AddCors(_configuration, _env, CorsPolicy);
builder.Services.AddHttpClient();


builder.Services.AddConfiguredMediatR();
builder.Services.AddConfiguredMassTransit(_configuration);
builder.Services.AddConfiguredMinio(_configuration);

builder.Services.AddConfiguredStackExchangeRedisCache(_configuration);
builder.Services.AddOperationLockManager();

builder.Services.AddRepositories(_configuration);
builder.Services.AddScoped<IHttpRequest, HttpRequest>();
builder.Services.AddScoped<IProjectNameIntegrationEventService, ProjectNameIntegrationEventService>();

builder.Services.AddConfiguredHostedServices(_configuration);


builder.Services.AddControllers(options => { options.Filters.Add<UnhandledExceptionFilterAttribute>(); });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectName.Api", Version = "v1" });
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
});

var app = builder.Build();


#endregion


#region Pipelines

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c => c.RouteTemplate = "swagger/{documentName}/swagger.json");
    app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "V1"));
}

Console.WriteLine($"Current running environment is: {app.Environment.EnvironmentName}");

app.UseSerilogRequestLogging(options =>
{
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("endpointMethod",
            httpContext.Features.Get<IEndpointFeature>()?.Endpoint?.DisplayName);
    };
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(CorsPolicy);

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProjectNameContext>();
    db.Database.Migrate();
}



app.Run();

#endregion


static bool FilterLog(IReadOnlyDictionary<string, Serilog.Events.LogEventPropertyValue> logProperties)
{
    if (logProperties.ContainsKey("StatusCode") && logProperties.ContainsKey("RequestMethod"))
    {
        //ignore successful GET requests
        if (logProperties["StatusCode"].ToString().Equals("200") && logProperties["RequestMethod"].ToString().Contains("GET"))
        {
            return true;
        }
    }
    return false;
}

