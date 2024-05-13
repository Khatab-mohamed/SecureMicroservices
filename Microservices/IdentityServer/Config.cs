global using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer;

public class Config
{

    /// <summary>
    /// Defines all the clients interacting with our resources.
    /// </summary>
    public static IEnumerable<Client> Clients => new Client[]
    {
        new() {
            ClientId ="moviesClient",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets =
            {
                new Secret("secret".Sha256()),

            },
            AllowedScopes={"moviesAPI"}
        }
    };

    /// <summary>
    /// Defines all the api scopes.
    /// </summary>
    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[] {
            new("moviesAPI","Movies APi")

    };
    /// <summary>
    /// Defines all the data resources.
    /// </summary>
    public static IEnumerable<ApiResource> ApiResources => new ApiResource[] { };
    /// <summary>
    /// Defines all the data identity resources.
    /// </summary>
    public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[] { };
    /// <summary>
    /// Defines all the test users.
    /// </summary>
    public static List<TestUser> TestUsers => new List<TestUser> { };

}
