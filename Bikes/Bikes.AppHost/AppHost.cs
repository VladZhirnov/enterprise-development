var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddSqlServer("sqlserver")
    .WithDataVolume()
    .AddDatabase("BikesDb");

builder.AddProject<Projects.Bikes_Api_Host>("bikes-api")
    .WithReference(db)
    .WaitFor(db)
    .WithExternalHttpEndpoints();

builder.Build().Run();