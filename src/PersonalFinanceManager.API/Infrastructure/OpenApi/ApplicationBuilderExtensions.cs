namespace PersonalFinanceManager.API.Infrastructure.OpenApi;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder MapSwagger(this IApplicationBuilder app)
    {
        app.UseOpenApi(settings =>
        {
            settings.Path = "/api/swagger/{documentName}/swagger.json";
            settings.PostProcess = (document, _) =>
            {
                // Update server URL for Swagger UI to include version.
                var prefix = "/api/v" + document.Info.Version.Split('.')[0];
                document.Servers.First().Url += prefix;
            };
        });
        app.UseSwaggerUi(settings =>
        {
            settings.Path = "/api";
            settings.TransformToExternalPath = (url, _) => url.EndsWith("swagger.json") ? $"/api{url}" : url;
        });

        return app;
    }
}
