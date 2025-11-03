var builder = WebApplication.CreateBuilder(args);

// service configuration

var app = builder.Build();

// http request pipeline configuration

app.Run();
