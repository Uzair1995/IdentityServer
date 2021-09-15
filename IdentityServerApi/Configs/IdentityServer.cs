using System.Collections.Generic;
using System.Linq;

namespace IdentityServerApi.Configs
{
    public static class IdentityServer
    {
        public static IEnumerable<IdentityServer4.Models.ApiScope> ApiScopes => new List<IdentityServer4.Models.ApiScope>
        {
            new IdentityServer4.Models.ApiScope("identity-server-api-admin", "Admin for Identity Server Internal Api")
        };

        public static IdentityServer4.Models.ApiResource ApiResource => new IdentityServer4.Models.ApiResource("identity-server-api", "Identity Server Api")
        {
            Scopes = ApiScopes.Select(x => x.Name).ToList()
        };

        public static IEnumerable<IdentityServerApi.Models.AddClient> Clients => new List<IdentityServerApi.Models.AddClient>()
        {
            new IdentityServerApi.Models.AddClient
            {
                Enabled = true,
                ClientId = "IdentityServerAdmin",
                ClientSecret = "secret123",
                AllowedScopes = new List<string>() { "identity-server-api-admin" },
                AccessTokenLifetimeInSeconds = 3600,
                Description = "",
                Email = null,
                EmailTemplate = null,
                FirmUserBelongsTo = "Logiciel",
                CorrespondentCode = null,
                BoothId = null,
                Office = null
            }
        };
    }
}
