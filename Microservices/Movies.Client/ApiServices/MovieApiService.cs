using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Movies.Client.Models;
using Newtonsoft.Json;

namespace Movies.Client.ApiServices;

public class MovieApiService : IMovieApiService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MovieApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _httpContextAccessor = httpContextAccessor;
    }

    // 1- Get Token From IS4 
    // 2- Send Request To Protected API
    // 3- Deserialize the Response object to MovieList


    public async Task<IEnumerable<Movie>> Movies()
    {
        var httpClient = _httpClientFactory.CreateClient("MovieAPIClient");

        // Ensure the BaseAddress is set in the HttpClient configuration
        if (httpClient.BaseAddress == null)
        {
            throw new InvalidOperationException("BaseAddress must be set.");
        }

        var request = new HttpRequestMessage(HttpMethod.Get, "/movies");
        var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var moviesList = JsonConvert.DeserializeObject<List<Movie>>(content);
        return moviesList;
    }
    public Task<Movie> Create(Movie movie)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Movie> Movie(int id)
    {
        throw new NotImplementedException();
    }


    public Task<Movie> Update(Movie movie)
    {
        throw new NotImplementedException();
    }

    public async Task<UserInfoViewModel> GetUserInfo()
    {
        var idpClient = _httpClientFactory.CreateClient("IDPClient");
        var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();
        if (metaDataResponse.IsError)
        
            throw new HttpRequestException("Something Went Wrong While Requesting The Access Token");

        var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
        var userInfoResponse = await idpClient.GetUserInfoAsync(
            new UserInfoRequest
            {
                Address= metaDataResponse.UserInfoEndpoint,
                Token = accessToken
            });
        if (userInfoResponse.IsError)
            throw new HttpRequestException("Something Went Wrong While User Info");
        var userInfoDictionary = new Dictionary<string, string>();
        foreach (var claim in userInfoResponse.Claims)
        {
            userInfoDictionary.Add(claim.Type,claim.Value);
        }
        var userInfoViewModel = new UserInfoViewModel(userInfoDictionary);
        return userInfoViewModel;
    }
}