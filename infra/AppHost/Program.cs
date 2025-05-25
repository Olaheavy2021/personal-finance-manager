var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.PersonalFinanceManger_API>("personalfinancemanger-api");

builder.Build().Run();
