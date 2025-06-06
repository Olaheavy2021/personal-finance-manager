using PersonalFinanceManager.API.Infrastructure.Validation;
using PersonalFinanceManager.Application;

namespace PersonalFinanceManager.API.Infrastructure.Behaviors;

public static class ConfigureMediatR
{
    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
          configuration.RegisterServicesFromAssemblies(typeof(ConfigureMediatR).Assembly);
        });

        services.AddValidators([typeof(Program), typeof(IApplicationMarker)]);
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(DomainExceptionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
       
        return services;
    }

    public static IServiceCollection AddCarterModules(
  this IServiceCollection services,
  params Type[] handlerAssemblyMarkerTypes)
    {
        DependencyContextAssemblyCatalog assemblyCatalog = new DependencyContextAssemblyCatalog(((IEnumerable<Type>)handlerAssemblyMarkerTypes).Select<Type, Assembly>((Func<Type, Assembly>)(t => t.Assembly)).ToArray<Assembly>());
        return services.AddCarter(assemblyCatalog);
    }
}

