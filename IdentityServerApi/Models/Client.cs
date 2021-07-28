using System.Collections.Generic;
using System.Linq;

namespace IdentityServerApi.Models
{
    public class Client
    {
        public string ClientId { get; set; }
        public bool Enabled { get; set; }
        public string Description { get; set; }
        public int AccessTokenLifetimeInSeconds { get; set; }
        public IEnumerable<string> AllowedScopes { get; set; }
    }

    internal static class ClientMapper
    {
        public static Client ToDto(this IdentityServer4.EntityFramework.Entities.Client client) => new Client
        {
            ClientId = client.ClientId,
            Enabled = client.Enabled,
            Description = client.Description,
            AccessTokenLifetimeInSeconds = client.AccessTokenLifetime,
            AllowedScopes = client.AllowedScopes.Select(x => x.Scope)
        };

        public static IEnumerable<Client> ToDto(this IEnumerable<IdentityServer4.EntityFramework.Entities.Client> clients)
        {
            return clients.Select(x => x.ToDto());
        }
    }
}
