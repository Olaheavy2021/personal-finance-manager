using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using PersonalFinanceManager.Client;

namespace PersonalFinanceManager.API.Test.Drivers;

public class ApiDriver : IDisposable
{
    private readonly WebApplicationFactory<Program> _webApp;

    public ApiDriver()
    {
        _webApp = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            builder
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((_, configBuilder) =>
                {
                })
                .ConfigureServices(services =>
                {
                })
        );

        HttpClient = _webApp.CreateClient();
        PFMv1Client = new PFMv1Client(_webApp.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri(_webApp.ClientOptions.BaseAddress, "api/v1/")
            }));
    }

    public HttpClient HttpClient { get; }
    public IPFMv1Client PFMv1Client { get; }
    public TestServer Server => _webApp.Server;

    public void Dispose()
    {
        _webApp.Dispose();
        HttpClient.Dispose();
    }
}
