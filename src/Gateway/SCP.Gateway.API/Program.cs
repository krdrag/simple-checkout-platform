using Ocelot.DependencyInjection;
using Ocelot.Middleware;

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("configuration.json")
                            .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot(configuration);

var app = builder.Build();

app.MapGet("/", () => "Gateway is running...");

app.UseOcelot().Wait();

app.Run();
