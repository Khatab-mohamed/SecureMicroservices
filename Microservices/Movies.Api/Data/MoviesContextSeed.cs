using Movies.Api.Models;

namespace Movies.Api.Data;

public class MoviesContextSeed
{
    public static void SeedAsync(MoviesApiContext context)
    {
        if (!context.Movies.Any())
        {
            var movies = new List<Movie> {
            new Movie
            {
                Id = 1,
                Title = 300.ToString(),
                Owner = "Leaonardo",
                Genre = "Action",
                ReleaseDate = DateTime.UtcNow,
                ImageUrl = "images/a.png",
                Rating ="5.0"
            },
                new Movie
                {
                    Id = 2,
                    Title = "Hulk",
                    Owner = "Mr AAA",
                    Genre = "Action",
                    ReleaseDate = DateTime.UtcNow,
                    ImageUrl = "images/a.png",
                    Rating ="5.0"
                }
            };
            context.Movies.AddRange(movies);
            context.SaveChanges();

        }

    }
}
