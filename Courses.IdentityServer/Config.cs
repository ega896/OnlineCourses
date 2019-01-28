using System.Collections.Generic;
using IdentityServer4.Models;

namespace Courses.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new[]
            {
                new ApiResource("demo_api", "Demo API with Swagger")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "demo_api_swagger",
                    ClientName = "Swagger UI for demo_api",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris =
                    {
                        "https://localhost/swagger/oauth2-redirect.html",
                        "https://localhost/swagger/o2c.html"
                    },
                    AllowedScopes = { "demo_api" }
                }
            };
        }
    }
}