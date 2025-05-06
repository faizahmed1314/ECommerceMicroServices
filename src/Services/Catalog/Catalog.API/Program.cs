var builder = WebApplication.CreateBuilder(args);

// Add services to the container
var app = builder.Build();

// configure http request pipeline 

app.Run();
