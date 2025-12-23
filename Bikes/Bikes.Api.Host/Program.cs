using Bikes.Application.Contracts.Services;
using Bikes.Application.Services;
using Bikes.Core.Entities;
using Bikes.Core.Repositories;
using Bikes.Infrastructure.EfCore.Data;
using Bikes.Infrastructure.EfCore.Repositories;
using Bikes.Infrastructure.Nats;
using Bikes.ServiceDefaults;
using Bikes.Validator.Nats;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddScoped<IRepository<BikeModel>, BikeModelRepository>();
builder.Services.AddScoped<IRepository<Bike>, BikeRepository>();
builder.Services.AddScoped<IRepository<Client>, ClientRepository>();
builder.Services.AddScoped<IRepository<Rental>, RentalRepository>();

builder.Services.AddScoped<IBikeModelService, BikeModelService>();
builder.Services.AddScoped<IBikeService, BikeService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<IAnalyticService, AnalyticService>();

builder.Services.AddHostedService<BikesNatsConsumer>();
builder.Services.AddHostedService<RentalValidatorService>();
builder.AddNatsClient("bikes-nats");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = false;
});

builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        logger.LogError(exception, "Unhandled exception occurred");

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(new
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Message = "An internal server error has occurred",
            TraceId = context.TraceIdentifier
        });
    });
});

app.UseStatusCodePages();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureDeleted();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapDefaultEndpoints();

app.Run();