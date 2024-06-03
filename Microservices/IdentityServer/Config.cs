global using IdentityServer4.Models;
global using IdentityServer4.Test;
using IdentityModel;
using IdentityServer4;
using System.Security.Claims;

namespace IdentityServer;

public class Config
{

    /// <summary>
    /// Defines all the clients interacting with our resources.
    /// </summary>
    public static IEnumerable<Client> Clients => new Client[]
    {
       /* new() {
            ClientId ="moviesClient",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets =
            {
                new Secret("secret".Sha256()),

            },
            AllowedScopes={"moviesAPI"}
        },*/
          new() {
            ClientId ="movies_mvc_client",
            ClientName="Movies MVC Web App",
            AllowedGrantTypes = GrantTypes.Hybrid,
            RequirePkce = false,
            AllowRememberConsent = false,
            RedirectUris= new List<string>()
            {
                "https://localhost:5002/signin-oidc" // this is the client app port
            },
            PostLogoutRedirectUris = new List<string>()
            {
                "https://localhost:5002/signout-callback-oidc"
            },
            ClientSecrets =
            {
                new Secret("secret".Sha256()),

            },
            AllowedScopes =
            {
                  IdentityServerConstants.StandardScopes.OpenId,
                  IdentityServerConstants.StandardScopes.Profile,
                  "moviesAPI"
            }
        }
    };

    /// <summary>
    /// Defines all the api scopes.
    /// </summary>
    public static IEnumerable<ApiScope> ApiScopes => new ApiScope[] 
    {
        new("moviesAPI","Movies APi")
    };


    /// <summary>
    /// Defines all the data resources.
    /// </summary>
    public static IEnumerable<ApiResource> ApiResources => new ApiResource[] 
    {
    };

    /// <summary>
    /// Defines all the data identity resources.
    /// </summary>
    public static IEnumerable<IdentityResource> IdentityResources => 
        new IdentityResource[] 
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    /// <summary>
    /// Defines all the test users.
    /// </summary>
    public static List<TestUser> TestUsers =>
        new()
        {
            new()
            {
                SubjectId = "36e34668-27ed-48c5-874a-e63bc2368beb",
                Username="Khatab",
                Password ="Abc_123",
                Claims= new List<Claim>
                {
                    new Claim(JwtClaimTypes.GivenName,"Khatab"),
                    new Claim(JwtClaimTypes.FamilyName,"Adam")
                }
            }
        };

}
