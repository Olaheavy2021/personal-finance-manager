namespace PersonalFinanceManager.API.Infrastructure.OpenApi;

public static class ConfigureSwagger
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, string name, IEnumerable<ApiVersion> apiVersions)
    {
        services.AddEndpointsApiExplorer();

        foreach (var version in apiVersions)
        {
            services.AddOpenApiDocument((settings, provider) =>
            {
                var suffixedVersion = $"v{version.MajorVersion}";
                settings.Title = $"{name} API {suffixedVersion}";
                settings.DocumentName = suffixedVersion;
                settings.ApiGroupNames = [suffixedVersion];
                settings.Version = $"{version.MajorVersion}.{version.MinorVersion}";
                settings.SchemaSettings.SchemaNameGenerator = new CustomSchemaNameGenerator();
                settings.PostProcess = document =>
                {
                    var prefix = "/api/v" + version.MajorVersion;
                    foreach (var pair in document.Paths.ToArray())
                    {
                        document.Paths.Remove(pair.Key);
                        document.Paths[pair.Key[prefix.Length..]] = pair.Value;
                    }
                };

                var additionalConfigurations = provider.GetServices<IConfigureOpenApiSettings>();
                foreach (var additional in additionalConfigurations)
                {
                    additional.ConfigureOpenApiSettings(version, settings);
                }
            });
        }

        return services;
    }
}
