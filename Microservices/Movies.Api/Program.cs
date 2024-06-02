using Movies.Api;
using Movies.Api.Data;


public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        SeedDatabase(host);
        host.Run();

    }

    // Seed Movies

    static void SeedDatabase(IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var moviesContext = services.GetRequiredService<MoviesApiContext>();
            MoviesContextSeed.SeedAsync(moviesContext);
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

}
