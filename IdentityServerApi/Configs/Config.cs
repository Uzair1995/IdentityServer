using IdentityServer.Repositories.Models;
using IdentityServer4.Models;
using IdentityServerApi.Models;
using System.Collections.Generic;

namespace IdentityServerApi.Configs
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource> {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

        public static List<IdentityServer4.Models.ApiResource> CompileApiResourcesFromRegisteredResources()
        {
            List<IdentityServer4.Models.ApiResource> apiResources = new List<IdentityServer4.Models.ApiResource>();
            apiResources.Add(Configs.IdentityServer.ApiResource);
            apiResources.Add(Configs.LexisNexisHandler.ApiResource);
            apiResources.Add(Configs.AccountOpeningAPI.ApiResource);
            return apiResources;
        }

        public static List<IdentityServer4.EntityFramework.Entities.Client> CompileClientsInfoFromRegisteredResources()
        {
            List<IdentityServer4.EntityFramework.Entities.Client> clients = new List<IdentityServer4.EntityFramework.Entities.Client>();
            clients.AddRange(Configs.IdentityServer.Clients.ToCoreList());
            clients.AddRange(Configs.LexisNexisHandler.Clients.ToCoreList());
            clients.AddRange(Configs.AccountOpeningAPI.Clients.ToCoreList());
            return clients;
        }

        public static List<ClientData> CompileClientsDataFromRegisteredResources()
        {
            List<ClientData> clients = new List<ClientData>();
            clients.AddRange(Configs.IdentityServer.Clients.ToExtraClientDataCore());
            clients.AddRange(Configs.LexisNexisHandler.Clients.ToExtraClientDataCore());
            clients.AddRange(Configs.AccountOpeningAPI.Clients.ToExtraClientDataCore());
            return clients;
        }

        public static IEnumerable<IdentityServer4.Models.ApiScope> CompileApiScopesFromRegisteredResources()
        {
            List<IdentityServer4.Models.ApiScope> apiScopes = new List<IdentityServer4.Models.ApiScope>();
            apiScopes.AddRange(Configs.IdentityServer.ApiScopes);
            apiScopes.AddRange(Configs.LexisNexisHandler.ApiScopes);
            apiScopes.AddRange(Configs.AccountOpeningAPI.ApiScopes);
            return apiScopes;
        }
    }
}
