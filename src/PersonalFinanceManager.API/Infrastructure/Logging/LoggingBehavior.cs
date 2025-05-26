namespace PersonalFinanceManager.Shared.Infrastructure.Logging;

[UsedImplicitly]
public class LoggingBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private static readonly Serilog.ILogger Logger = Log.ForContext<LoggingBehavior<TRequest>>();

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        Logger.Information("Processing request: {Name}, {@Request}", requestName, request);

        return Task.CompletedTask;
    }
}
