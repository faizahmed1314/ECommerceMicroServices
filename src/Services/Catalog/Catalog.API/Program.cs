using Carter;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddMarten(op =>
{
    op.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
builder.Services.AddLogging(); // Ensure logging is available
var app = builder.Build();

// configure http request pipeline 

app.MapCarter();

app.Run();
