using Ordering.API;
using Ordering.Application;
using Ordering.Infrastucture;
using Ordering.Infrastucture.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// service configuration

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

// http request pipeline configuration
app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}
app.Run();
