using Identity.API;
using SCP.Common.Tools;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables form .env.dev file (if in development mode)
DevEnvLoader.Load();

builder.Services.AddIdentityServer()
    .AddInMemoryClients(Config.Clients)
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddTestUsers(Config.Users)
    .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseIdentityServer();

app.MapGet("/", () => "Identity server is running...");

app.Run();
