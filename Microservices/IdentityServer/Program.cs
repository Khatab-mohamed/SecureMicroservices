using IdentityServer;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddIdentityServer()
    .AddInMemoryClients(Config.Clients) // Ensure these have valid configurations
    //.AddInMemoryIdentityResources(Config.IdentityResources)
    //.AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    //.AddTestUsers(Config.TestUsers)
    .AddDeveloperSigningCredential();

var app = builder.Build();

// Use HTTPS redirection
app.UseHttpsRedirection();

// Add UseIdentityServer
app.UseIdentityServer();

// Add routing and endpoints (if using)
app.UseRouting();


app.Run();
