using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movies.Api.Data;
namespace Movies.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<MoviesApiContext>(options =>
            options.UseInMemoryDatabase("MoviesDb"));
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:5005";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });
        services.AddAuthorization(options =>
        {
            options.AddPolicy("ClientPloicy",
                policy => 
                policy.RequireClaim("client_id", "moviesClient"));
        });
        // Add controllers
        services.AddControllers();

        // Add Swagger services
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movies API V1");

        });
        app.UseHttpsRedirection();

        // Add routing middleware
        app.UseRouting();
        app.UseAuthentication();

        app.UseAuthorization();

        // Add Swagger middleware


        // Use endpoints for controllers
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

    }
}