var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddSqlServer("sqlserver")
    .WithDataVolume()
    .AddDatabase("BikesDb");

var natsUserName = builder.AddParameter("NatsLogin");
var natsPassword = builder.AddParameter("NatsPassword");
var nats = builder.AddNats("bikes-nats", userName: natsUserName, password: natsPassword, port: 4222)
    .WithJetStream()
    .WithArgs("-m", "8222")
    .WithHttpEndpoint(port: 8222, targetPort: 8222);

var natsStream = builder.AddParameter("NatsStream");
var rawSubject = builder.AddParameter("RawSubject");
var validatedSubject = builder.AddParameter("ValidatedSubject");
var batchSize = builder.AddParameter("GeneratorBatchSize");
var payloadLimit = builder.AddParameter("GeneratorPayloadLimit");
var waitTime = builder.AddParameter("GeneratorWaitTime");

builder.AddProject<Projects.Bikes_Generator_Nats_Host>("bikes-generator-nats")
    .WithReference(nats)
    .WaitFor(nats)
    .WithEnvironment("Generator:BatchSize", batchSize)
    .WithEnvironment("Generator:PayloadLimit", payloadLimit)
    .WithEnvironment("Generator:WaitTime", waitTime)
    .WithEnvironment("Nats:StreamName", natsStream)
    .WithEnvironment("Nats:RawSubject", rawSubject)
    .WithEnvironment("Nats:ValidatedSubject", validatedSubject);

builder.AddProject<Projects.Bikes_Api_Host>("bikes-api")
    .WithReference(db)
    .WaitFor(db)
    .WithExternalHttpEndpoints()
    .WithEnvironment("Nats:RawSubject", rawSubject)
    .WithEnvironment("Nats:ValidatedSubject", validatedSubject)
    .WithEnvironment("Nats:StreamName", natsStream)
    .WithReference(nats)
    .WaitFor(nats);

builder.Build().Run();