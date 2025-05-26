using PersonalFinanceManager.API.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddMyAppDefaults();

var app = builder.Build();

WebApplicationExtensions.UseMyAppDefaults(app);

app.Run();
