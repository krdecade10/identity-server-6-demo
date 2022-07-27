using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace Personal.IdentityServer6Demo.IdP
{
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("custom.profile", new[] { JwtClaimTypes.Name, JwtClaimTypes.Email })
                {
                    Required = true 
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("scope", "My API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "web",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RedirectUris = { "https://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                    RequirePkce = false,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "custom.profile"
                    }
                },
                new Client
                {
                    ClientId = "viedoc-web",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    ClientSecrets =
                    {
                        new Secret("viedoc-secret".Sha256())
                    },
                    RedirectUris = { "https://localhost:44301/callback" },
                    PostLogoutRedirectUris = { "https://localhost:44301/signout-callback-odic" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    RequirePkce = false,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AlwaysSendClientClaims = true
                }
            };
    }
}
