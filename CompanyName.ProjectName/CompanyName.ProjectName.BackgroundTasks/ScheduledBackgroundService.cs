using CompanyName.ProjectName.Application.Commands.ScheduledJobLogs.CreateFinishJobLog;
using CompanyName.ProjectName.Application.Commands.ScheduledJobLogs.CreateNewScheduledJobLog;
using CompanyName.ProjectName.Domain.SeedWork.Utilities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NCrontab;
using Serilog;

namespace CompanyName.ProjectName.BackgroundTasks;

public abstract class ScheduledBackgroundService : IHostedService, IDisposable
{
    protected readonly ILogger _logger;
    protected readonly IOperationLockManager _lockManager;
    protected IServiceProvider _services;
    protected CrontabSchedule _cronSchedule;
    protected IMediator _mediator;
    protected System.Timers.Timer _timer;

    /// <summary>
    /// When Execution Timeout reaches, the OperationLock will be released automatically.
    /// </summary>
    /// <returns></returns>
    protected TimeSpan _executionTimeout = new TimeSpan(0, 10, 0); // 10 min
    private readonly bool _needLock = true;
    private readonly bool _needLog = true;

    public ScheduledBackgroundService(ILogger logger, IServiceProvider services, bool needLock, bool needLog)
    {
        _logger = logger;
        _services = services;
        _lockManager = _services.GetRequiredService<IOperationLockManager>();
        _needLock = needLock;
        _needLog = needLog;
    }

    public virtual async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.Information("Starting " + this.GetType().ToString());
        CreateCronSchedule();
        await ScheduleNextRun(cancellationToken);
    }

    private void CreateCronSchedule()
    {
        var cronExpression = GetCronExpression();
        _cronSchedule = CrontabSchedule.Parse(cronExpression,
            new CrontabSchedule.ParseOptions { IncludingSeconds = false });
    }

    protected Task<bool> IsLockedAsync()
    {
        var lockKey = this.GetType().Name;
        return _lockManager.IsLockedAsync(lockKey, _executionTimeout);
    }

    protected Task<bool> ReleaseLockAsync()
    {
        var lockKey = this.GetType().Name;
        return _lockManager.ReleaseLockAsync(lockKey);
    }

    protected async Task RunTask(CancellationToken cancellationToken)
    {
        if (!_needLock || !await IsLockedAsync())
        {
            using (var scope = _services.CreateScope())
            {
                _mediator = scope.ServiceProvider.GetService<IMediator>();
                Guid jobLogId = Guid.NewGuid();
                if (_needLog)
                {
                    var createNewJobLogCommand = new CreateNewScheduledJobLogCommand(this.GetType().Name);
                    var jobLog = await _mediator.Send(createNewJobLogCommand, cancellationToken);
                    jobLogId = jobLog.Id;
                }

                try
                {
                    await ExecuteAsync(scope, cancellationToken);
                    if (_needLog)
                        await SetJobAsSucceeded(jobLogId);
                }
                catch (Exception e)
                {
                    _logger.Error(e, "Error On Running " + this.GetType().ToString());
                    if (_needLog)
                        await SetAsFailed(jobLogId, e.Message);
                }
                finally
                {
                    if (_needLock)
                        await ReleaseLockAsync();
                }
            }
        }
        else
        {
            _logger.Warning("Couldn't Get Lock for Scheduled Background Service.{@name}", this.GetType().Name);
        }
    }

    private async Task SetAsFailed(Guid jobLogId, string message)
    {
        try
        {
            var finishJobCommand = new CreateFinishJobLogCommand(jobLogId, Domain.Aggregates.ScheduledJobLogAggregate.ScheduledJobLogStatus.Failed, message);
            await _mediator.Send(finishJobCommand);
        }
        catch (Exception ex)
        {
            _logger.Error($"Error on logging scheduled job {this.GetType().Name}. Error: {ex.Message}");

        }

    }

    private async Task SetJobAsSucceeded(Guid jobLogId)
    {
        var finishJobCommand = new CreateFinishJobLogCommand(jobLogId, Domain.Aggregates.ScheduledJobLogAggregate.ScheduledJobLogStatus.Succeeded);
        await _mediator.Send(finishJobCommand);
    }

    protected async Task ScheduleNextRun(CancellationToken cancellationToken)
    {
        var nextRunTime = _cronSchedule.GetNextOccurrence(DateTime.UtcNow);
        var delay = nextRunTime - DateTimeOffset.UtcNow;
        if (delay.TotalMilliseconds <= 0) // prevent non-positive values from being passed into Timer
        {
            await ScheduleNextRun(cancellationToken);
        }

        _timer = new System.Timers.Timer(delay.TotalMilliseconds);
        _timer.Elapsed += async (sender, args) =>
        {
            _timer.Dispose(); // reset and dispose timer
            _timer = null;

            if (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await RunTask(cancellationToken);
                }
                catch (Exception e)
                {
                    _logger.Error(e, "Error On RunTask " + this.GetType().ToString());
                }
            }

            if (!cancellationToken.IsCancellationRequested)
            {
                await ScheduleNextRun(cancellationToken); // reschedule next
            }
        };
        _timer.Start();
        await Task.CompletedTask;
    }

    public virtual async Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Stop();
        _logger.Information("Stopped " + this.GetType().ToString());
        await Task.CompletedTask;
    }

    protected abstract string GetCronExpression();

    protected abstract Task ExecuteAsync(IServiceScope scope, CancellationToken cancellationToken);

    public void Dispose()
    {
        _timer?.Dispose();
    }
}

