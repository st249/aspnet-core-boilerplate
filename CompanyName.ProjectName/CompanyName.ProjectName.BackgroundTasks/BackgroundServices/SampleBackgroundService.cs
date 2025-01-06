using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CompanyName.ProjectName.BackgroundTasks.BackgroundServices;

public class SampleBackgroundService : ScheduledBackgroundService
{
    public SampleBackgroundService(ILogger logger, IServiceProvider services) : base(logger, services, false, false)
    {
    }

    protected override async Task ExecuteAsync(IServiceScope scope, CancellationToken cancellationToken)
    {
        Console.WriteLine("Testing job");
        //HERE WE SHOULD CALL COMMANDS AND QUERIES

    }

    protected override string GetCronExpression()
    {
        return "* */1 * * *";
    }
}


