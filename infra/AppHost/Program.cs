var builder = DistributedApplication.CreateBuilder(args);

var pfmApi = builder.AddProject<Projects.PersonalFinanceManger_API>("personalfinancemanger-api")
    .WithExternalHttpEndpoints();

builder.AddNpmApp("react", "../../frontend/pfm-app")
    .WithReference(pfmApi)
    .WaitFor(pfmApi)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();
