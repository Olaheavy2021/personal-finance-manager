using Asp.Versioning;
using NSwag.Generation.AspNetCore;

namespace PersonalFinanceManager.API.Infrastructure.OpenApi;

public interface IConfigureOpenApiSettings
{
    void ConfigureOpenApiSettings(ApiVersion version, AspNetCoreOpenApiDocumentGeneratorSettings settings);
}
