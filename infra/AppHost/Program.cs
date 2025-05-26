var builder = DistributedApplication.CreateBuilder(args);

var pfmApi = builder.AddProject<Projects.PersonalFinanceManager_API>("personalfinancemanager-api")
    .WithExternalHttpEndpoints();

builder.AddNpmApp("react", "../../frontend/pfm-app")
    .WithReference(pfmApi)
    .WaitFor(pfmApi)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();
