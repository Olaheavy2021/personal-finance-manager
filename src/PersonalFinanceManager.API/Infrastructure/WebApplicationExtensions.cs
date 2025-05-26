namespace PersonalFinanceManager.API.Infrastructure;

public static class WebApplicationExtensions
{
    public static WebApplication UseMyAppDefaults(WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseCommonExceptionHandler();
        app.UseCors(Constants.CorsPolicy);
        app.NewVersionedApi()
            .MapGroup("/api/v{version:apiVersion}")
            .MapCarter();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapSwagger();
        }

        app.MapDefaultEndpoints()
           .UseHttpsRedirection();

        return app;
    }
}
