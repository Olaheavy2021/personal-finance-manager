var builder = DistributedApplication.CreateBuilder(args);

/*Backing Services*/

var postgres = builder
         .AddPostgres("postgres")
         .WithPgAdmin()
         .WithDataVolume()
         .WithLifetime(ContainerLifetime.Persistent);
var pfmDb = postgres.AddDatabase("pfmdb");
/*Backing Services*/


/*Projects*/

var migrationWorker = builder.AddProject<Projects.PersonalFinanceManager_MigrationService>("migrations")
    .WithReference(pfmDb)
    .WaitFor(pfmDb);

var pfmApi = builder.AddProject<Projects.PersonalFinanceManager_API>("api")
    .WithReference(pfmDb)
    .WaitFor(pfmDb)
    .WaitFor(migrationWorker);

builder.AddNpmApp("react", "../../frontend/pfm-app")
    .WithReference(pfmApi)
    .WaitFor(pfmApi)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

/*Projects*/

builder.Build().Run();
