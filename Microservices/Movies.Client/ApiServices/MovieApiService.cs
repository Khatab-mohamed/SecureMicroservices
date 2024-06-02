using Movies.Client.Models;
using Newtonsoft.Json;

namespace Movies.Client.ApiServices;

public class MovieApiService : IMovieApiService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public MovieApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
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

        var request = new HttpRequestMessage(HttpMethod.Get, "api/movies");
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
}