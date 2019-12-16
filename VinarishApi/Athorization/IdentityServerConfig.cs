using IdentityServer4.Models;
using System.Collections.Generic;

namespace VinarishApi.Athorization
{
    public class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
        {
            new ApiResource("VinarishApi", "Vinarish API")
        };

        public static IEnumerable<IdentityServer4.Models.Client> Clients =>
            new List<IdentityServer4.Models.Client>
            {
                new IdentityServer4.Models.Client
                {
                    ClientId = "VinarishApiClient",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("SecretVinarishApi".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "VinarishApi" }
                }
            };
    }
}