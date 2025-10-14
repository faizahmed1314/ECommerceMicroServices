

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCarter();

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidatorBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(op =>
{
    op.Connection(builder.Configuration.GetConnectionString("Database")!);
    op.Schema.For<ShopingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

var app = builder.Build();

// configure http request pipeline
app.MapCarter();

app.Run();
