using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

namespace PersonalFinanceManager.API.Infrastructure;

public static class ConfigureJson
{
    public static IServiceCollection AddJsonConverters(this IServiceCollection services)
    {
        services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
        {
            options.SerializerOptions.Converters.Add(new DoubleInfinityConverter());
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        return services;
    }
}
