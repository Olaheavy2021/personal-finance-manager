namespace PersonalFinanceManager.API.Infrastructure;

public static class ConfigureCors
{
   public static IServiceCollection AddCustomCors(this IServiceCollection services)
   {
        services.AddCors(options =>
        {
            options.AddPolicy(name: Constants.CorsPolicy, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });
        return services;
   }
}

