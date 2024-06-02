global using IdentityModel.Client;

namespace Movies.Client.HttpHandlers;

public class AuthenticationDelegatingHandler : DelegatingHandler
{
    // For Getting Token From IS4
    // This class will Intercepts All Http requests
    // And try to get token Before Sending Request To Resources.

    #region Properities
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ClientCredentialsTokenRequest _tokenRequest;

    #endregion

    #region Constructor
    public AuthenticationDelegatingHandler(ClientCredentialsTokenRequest clientCredentialsTokenRequest,
                                          IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _tokenRequest = clientCredentialsTokenRequest ?? throw new ArgumentNullException(nameof(clientCredentialsTokenRequest));
    }
    #endregion
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {

        // Create HttpClient 
        var httpClient = _httpClientFactory.CreateClient("IDPClient");
        var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(_tokenRequest);
        if (tokenResponse.IsError)
        {
            throw new HttpRequestException("Something Went Wrong While Requesting The Access Token");
        }
        request.SetBearerToken(tokenResponse.AccessToken);
        return await base.SendAsync(request, cancellationToken);
    }
}
