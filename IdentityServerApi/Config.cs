using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServerApi
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope> {
                new ApiScope("iclear-account-opening-api", "IClear Account Opening Api"),
                new ApiScope("iclear-account-opening-api-admin", "Admin for IClear Account Opening Api"),
                new ApiScope("iclear-account-opening-api-support", "Support for IClear Account Opening Api"),
                new ApiScope("identity-server-api-admin", "Admin for Identity Server Internal Api")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("iclear-account-opening-api", "IClear Account Opening Api")
                {
                    Scopes = { "iclear-account-opening-api", "iclear-account-opening-api-admin", "iclear-account-opening-api-support" }
                },
                new ApiResource("identity-server-api", "Identity Server Api")
                {
                    Scopes = { "identity-server-api-admin" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    Enabled = true,
                    ClientId = "User",
                    ClientSecrets = { new Secret("secret123".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "iclear-account-opening-api" },
                    AlwaysSendClientClaims = true,
                    ClientClaimsPrefix = string.Empty,
                },
                new Client
                {
                    Enabled = true,
                    ClientId = "AOSupportUser",
                    ClientSecrets = { new Secret("secret123".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "iclear-account-opening-api-support", "iclear-account-opening-api" },
                    AlwaysSendClientClaims = true,
                    ClientClaimsPrefix = string.Empty,
                },
                new Client
                {
                    Enabled = true,
                    ClientId = "AOAdminUser",
                    ClientSecrets = { new Secret("secret123".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "iclear-account-opening-api-admin", "iclear-account-opening-api-support", "iclear-account-opening-api" },
                    AlwaysSendClientClaims = true,
                    ClientClaimsPrefix = string.Empty,
                },
                new Client
                {
                    Enabled = true,
                    ClientId = "IdentityServerAdmin",
                    ClientSecrets = { new Secret("secret123".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "identity-server-api-admin" }
                }
            };
    }
}
