using Ordering.API;
using Ordering.Application;
using Ordering.Infrastucture;

var builder = WebApplication.CreateBuilder(args);

// service configuration

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

// http request pipeline configuration

app.Run();
