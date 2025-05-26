using PersonalFinanceManager.API.Infrastructure.Behaviors;

namespace PersonalFinanceManager.API.Infrastructure.Behaviors;

public static class ConfigureMediatR
{
    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
          configuration.RegisterServicesFromAssemblies(typeof(ConfigureMediatR).Assembly);
        });

        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(DomainExceptionBehavior<,>));

        return services;
    }
}
