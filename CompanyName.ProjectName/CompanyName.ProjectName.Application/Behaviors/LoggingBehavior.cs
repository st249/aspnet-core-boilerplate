using System.Diagnostics;

namespace CompanyName.ProjectName.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger _logger;
    public LoggingBehavior(ILogger logger) => _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        _logger.Information("----- Handling CQ {@CQName} ({@Command})", request.GetGenericTypeName(), request);
        var response = await next();
        _logger.Information("----- CQ {@CQName} handled - response: {@Response}", request.GetGenericTypeName(), response);
        sw.Stop();
        if (sw.ElapsedMilliseconds > 500)
            _logger.Warning("Log Run CQ {@CQName}: {@request}", request.GetGenericTypeName(), request);
        return response;
    }
}


