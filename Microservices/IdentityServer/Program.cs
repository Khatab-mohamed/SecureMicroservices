using IdentityServer4.Models;
using IdentityServer4.Test;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddIdentityServer()
    .AddInMemoryClients(new List<Client>()) // Ensure these have valid configurations
    .AddInMemoryIdentityResources(new List<IdentityResource>())
    .AddInMemoryApiResources(new List<ApiResource>())
    .AddInMemoryApiScopes(new List<ApiScope>())
    .AddTestUsers(new List<TestUser>())
    .AddDeveloperSigningCredential();

var app = builder.Build();

// Use HTTPS redirection
app.UseHttpsRedirection();

// Add UseIdentityServer
app.UseIdentityServer();

// Add routing and endpoints (if using)
app.UseRouting();


app.Run();
