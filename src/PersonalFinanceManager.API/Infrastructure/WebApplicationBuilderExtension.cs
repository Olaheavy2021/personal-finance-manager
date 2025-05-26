using PersonalFinanceManager.API.Infrastructure.Behaviors;
using PersonalFinanceManager.API.Infrastructure.Versioning;
using PersonalFinanceManager.Shared.Infrastructure.Logging;

namespace PersonalFinanceManager.API.Infrastructure;

public static class WebApplicationBuilderExtension
{
    public static ApiVersion V1 { get; } = new(1, 0); 

    public static ApiVersion[] Versions { get; } = [V1];
    public static WebApplicationBuilder AddMyAppDefaults(this WebApplicationBuilder builder)
    {
        builder.AddServiceDefaults();

        builder.AddSerilogLogging();

        builder.Services.AddCustomCors()
                        .AddVersioning()
                        .AddOpenApi()
                        .AddMediatR()
                        .AddProblemDetails()
                        .AddSwagger(Constants.ApplicationName, Versions)
                        .AddJsonConverters()
                        .AddCarter();

        return builder;
    }
}
