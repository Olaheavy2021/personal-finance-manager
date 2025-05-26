namespace PersonalFinanceManager.Shared.Infrastructure.Logging;

public static class ConfigureLogging
{
    public static WebApplicationBuilder AddSerilogLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, _, loggerConfiguration) => loggerConfiguration
                .ReadFrom.Configuration(context.Configuration)
        );

        return builder;
    }
}


