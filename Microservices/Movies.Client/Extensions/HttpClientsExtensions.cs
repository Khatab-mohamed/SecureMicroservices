﻿using Microsoft.Net.Http.Headers;
using Movies.Client.HttpHandlers;

namespace Movies.Client.Extensions;

public static class HttpClientsExtensions
{
    public static void AddHttpClients(this IServiceCollection services)
    {
        #region Movies
        // Api
        /*services.AddHttpClient("MovieAPIClient", client =>
        {
            client.BaseAddress = new Uri("https://localhost:5001/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        }).AddHttpMessageHandler<AuthenticationDelegatingHandler>();*/
        // Gateway
        services.AddHttpClient("MovieAPIClient", client =>
        {
            client.BaseAddress = new Uri("https://localhost:5010/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        }).AddHttpMessageHandler<AuthenticationDelegatingHandler>();
        #endregion

        #region IS4

        services.AddHttpClient("IDPClient", client =>
        {
            client.BaseAddress = new Uri("https://localhost:5005/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        });
        services.AddHttpContextAccessor(); // we accessing the token using this 
        /*   services.AddSingleton(new ClientCredentialsTokenRequest
           {
               ClientId = "moviesClient",
               ClientSecret = "secret",
               Scope = "moviesAPI",
               Address = "https://localhost:5005/connect/token"
           });*/
        #endregion
    }
}
