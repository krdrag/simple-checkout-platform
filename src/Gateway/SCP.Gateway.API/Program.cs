using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using SCP.Common.Tools;

string configName = EnvironmentCheck.IsDevEnv() ? "configuration.json" : "configuration.docker.json";

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile(configName)
                            .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot(configuration);

var app = builder.Build();

app.MapGet("/", () => "Gateway is running...");

app.UseOcelot().Wait();

app.Run();
