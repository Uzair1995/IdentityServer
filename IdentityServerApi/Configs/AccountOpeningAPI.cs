using System.Collections.Generic;
using System.Linq;

namespace IdentityServerApi.Configs
{
    public static class AccountOpeningAPI
    {
        public static IEnumerable<IdentityServer4.Models.ApiScope> ApiScopes => new List<IdentityServer4.Models.ApiScope>
        {
            new IdentityServer4.Models.ApiScope("iclear-account-opening-api", "IClear Account Opening Api User"),
            new IdentityServer4.Models.ApiScope("iclear-account-opening-api-admin", "Admin for IClear Account Opening Api Admin"),
            new IdentityServer4.Models.ApiScope("iclear-account-opening-api-support", "Support for IClear Account Opening Api Support")
        };

        public static IdentityServer4.Models.ApiResource ApiResource => new IdentityServer4.Models.ApiResource("iclear-account-opening-api", "IClear Account Opening Api")
        {
            Scopes = ApiScopes.Select(x => x.Name).ToList()
        };

        public static IEnumerable<IdentityServerApi.Models.AddClient> Clients => new List<IdentityServerApi.Models.AddClient>
        {
            new IdentityServerApi.Models.AddClient
            {
                Enabled = true,
                ClientId = "User",
                ClientSecret = "secret123",
                AllowedScopes = new List<string>() { "iclear-account-opening-api" },
                AccessTokenLifetimeInSeconds = 3600,
                Description = "",
                Email = null,
                EmailTemplate = null,
                FirmUserBelongsTo = "Logiciel",
                CorrespondentCode = null,
                BoothId = null,
                Office = null
            },
            new IdentityServerApi.Models.AddClient
            {
                Enabled = true,
                ClientId = "AOAdminUser",
                ClientSecret = "secret123",
                AllowedScopes = new List<string>() { "iclear-account-opening-api-admin", "iclear-account-opening-api-support", "iclear-account-opening-api" },
                AccessTokenLifetimeInSeconds = 3600,
                Description = "",
                Email = null,
                EmailTemplate = null,
                FirmUserBelongsTo = "Logiciel",
                CorrespondentCode = null,
                BoothId = null,
                Office = null
            },
            new IdentityServerApi.Models.AddClient
            {
                Enabled = true,
                ClientId = "AOSupportUser",
                ClientSecret = "secret123",
                AllowedScopes = new List<string>() { "iclear-account-opening-api-support", "iclear-account-opening-api" },
                AccessTokenLifetimeInSeconds = 3600,
                Description = "",
                Email = null,
                EmailTemplate = null,
                FirmUserBelongsTo = "Logiciel",
                CorrespondentCode = null,
                BoothId = null,
                Office = null
            },
        };
    }
}
