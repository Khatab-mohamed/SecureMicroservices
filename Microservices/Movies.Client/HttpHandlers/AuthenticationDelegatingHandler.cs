using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Movies.Client.HttpHandlers;

public class AuthenticationDelegatingHandler : DelegatingHandler
{
    // For Getting Token From IS4
    // This class will Intercepts All Http requests
    // And try to get token Before Sending Request To Resources.

    /*private readonly IHttpClientFactory _httpClientFactory;
    private readonly ClientCredentialsTokenRequest _tokenRequest;

    public AuthenticationDelegatingHandler(ClientCredentialsTokenRequest clientCredentialsTokenRequest,
                                           IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _tokenRequest = clientCredentialsTokenRequest ?? throw new ArgumentNullException(nameof(clientCredentialsTokenRequest));
    }*/

    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {

        // Create HttpClient 
        /* var httpClient = _httpClientFactory.CreateClient("IDPClient");
         var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(_tokenRequest);
         if (tokenResponse.IsError)
         {
             throw new HttpRequestException("Something Went Wrong While Requesting The Access Token");
         }
         request.SetBearerToken(tokenResponse.AccessToken);*/
        var accessToken = await  _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            request.SetBearerToken(accessToken );
        }
        return await base.SendAsync(request, cancellationToken);
    }
}
