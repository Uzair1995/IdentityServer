using System.Collections.Generic;
using System.Linq;

namespace IdentityServerApi.Configs
{
    public static class LexisNexisHandler
    {
        public static IEnumerable<IdentityServer4.Models.ApiScope> ApiScopes => new List<IdentityServer4.Models.ApiScope>
        {
            new IdentityServer4.Models.ApiScope("lexis-nexis-handler-admin", "Admin for Lexis Nexis Handler Api")
        };

        public static IdentityServer4.Models.ApiResource ApiResource => new IdentityServer4.Models.ApiResource("lexis-nexis-handler", "Lexis Nexis Handler Api")
        {
            Scopes = ApiScopes.Select(x => x.Name).ToList()
        };

        public static IEnumerable<IdentityServerApi.Models.AddClient> Clients => new List<IdentityServerApi.Models.AddClient>
        {
            new IdentityServerApi.Models.AddClient
            {
                Enabled = true,
                ClientId = "LexisNexisAdmin",
                ClientSecret = "secret123",
                AllowedScopes = new List<string>() { "lexis-nexis-handler-admin" },
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
