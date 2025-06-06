using PersonalFinanceManager.API.Features.Auth.Commands;
using PersonalFinanceManager.Application;
using PersonalFinanceManager.Shared.Infrastructure.Logging;

namespace PersonalFinanceManager.API.Infrastructure;

public static class WebApplicationBuilderExtension
{
    public static ApiVersion V1 { get; } = new(1, 0); 

    public static ApiVersion[] Versions { get; } = [V1];
    public static WebApplicationBuilder AddMyAppDefaults(this WebApplicationBuilder builder, IConfiguration config)
    {
        builder.Services.AddHealthChecks();
        builder.AddServiceDefaults();

        builder.AddSerilogLogging();

        builder.Services.AddCustomCors()
                        .AddVersioning()
                        .AddOpenApi()
                        .AddMediatR()
                        .AddProblemDetails()
                        .AddSwagger(Constants.ApplicationName, Versions)
                        .AddJsonConverters()
                        .AddCarterModules(typeof(RevokeToken));

        // Add any additional services here, such as database context, repositories, etc.
        builder.Services.AddApplication(config);
        builder.Services.AddDatabase(config["ConnectionStrings:pfmdb"]!);
        builder.Services.AddScoped<ClaimsPrincipal>();

        return builder;
    }
}
