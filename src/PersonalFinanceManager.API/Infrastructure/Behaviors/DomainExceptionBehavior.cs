using PersonalFinanceManager.API.Infrastructure.Behaviors.Domain;

namespace PersonalFinanceManager.API.Infrastructure.Behaviors;

public class DomainExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult
{
    // ReSharper disable once StaticMemberInGenericType
    private static readonly Dictionary<string, string[]> EmptyErrors = new();

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            var response = await next();
            return response;
        }
        catch (DomainException ex)
        {
            return (TResponse)Results.ValidationProblem(EmptyErrors, ex.Message);
        }
    }
}