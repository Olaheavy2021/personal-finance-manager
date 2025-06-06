using PersonalFinanceManager.API.Infrastructure;
using PersonalFinanceManager.Application.Database;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.AddMyAppDefaults(config);

var app = builder.Build();

WebApplicationExtensions.UseMyAppDefaults(app);

var dbInitializer = app.Services.GetRequiredService<DBInitializer>();
await dbInitializer.SeedData(app);

app.Run();
