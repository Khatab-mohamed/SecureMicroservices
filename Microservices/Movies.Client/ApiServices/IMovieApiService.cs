using Movies.Client.Models;

namespace Movies.Client.ApiServices;
public interface IMovieApiService
{
    Task<IEnumerable<Movie>> Movies();
    Task<Movie> Movie(int id);
    Task<Movie> Create(Movie movie);
    Task<Movie> Update(Movie movie);
    Task Delete(int id);
    Task<UserInfoViewModel> GetUserInfo();
}
