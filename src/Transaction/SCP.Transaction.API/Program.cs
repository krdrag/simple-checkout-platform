using SCP.Common.Middleware;
using SCP.Common.Tools;
using SCP.Transaction.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Load environment variables form .env.dev file (if in development mode)
DevEnvLoader.Load();

builder.Services.AddExternalServices();
builder.Services.AddCustomServices();

builder.Services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication("Bearer", options => {
        options.ApiName = "SCP.Transaction";
        options.Authority = "https://localhost:6100";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionMiddleware();

app.MapControllers();

app.Run();
