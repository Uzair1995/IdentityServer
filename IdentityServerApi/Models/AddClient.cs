using IdentityServer4.Models;
using System.ComponentModel.DataAnnotations;
using IdentityServer4.EntityFramework.Mappers;
using System.Collections.Generic;
using IdentityServer.Repositories.Models;
using System.Linq;

namespace IdentityServerApi.Models
{
    public class AddClient
    {
        [Required] public bool? Enabled { get; set; }

        [Required] public string ClientId { get; set; }

        [Required] public string ClientSecret { get; set; }

        [Required] public List<string> AllowedScopes { get; set; }

        public string Description { get; set; }

        [Required] public int? AccessTokenLifetimeInSeconds { get; set; }

        //Extra client related data
        [Required] public string Email { get; set; }

        public string EmailTemplate { get; set; }

        [Required] public string FirmUserBelongsTo { get; set; }

        [Required] public string CorrespondentCode { get; set; }

        [Required] public string BoothId { get; set; }

        [Required] public string Office { get; set; }
    }

    public static class ClientConverter
    {
        public static IdentityServer4.EntityFramework.Entities.Client ToCore(this AddClient client)
        {
            return new IdentityServer4.Models.Client
            {
                Enabled = client.Enabled.Value,
                ClientId = client.ClientId,
                Description = client.Description,
                ClientSecrets = { new Secret(client.ClientSecret.Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = client.AllowedScopes,
                AlwaysSendClientClaims = true,
                ClientClaimsPrefix = string.Empty,
                AccessTokenLifetime = client.AccessTokenLifetimeInSeconds.Value
            }.ToEntity();
        }

        public static ClientData ToExtraClientDataCore(this AddClient client)
        {
            return new ClientData
            {
                ClientId = client.ClientId,
                Email = client.Email,
                EmailTemplate = client.EmailTemplate,
                FirmUserBelongsTo = client.FirmUserBelongsTo,
                CorrespondentCode = client.CorrespondentCode,
                BoothId = client.BoothId,
                Office = client.Office
            };
        }

        public static IEnumerable<IdentityServer4.EntityFramework.Entities.Client> ToCoreList(this IEnumerable<AddClient> clients)
        {
            return clients.Select(x => x.ToCore());
        }

        public static IEnumerable<ClientData> ToExtraClientDataCore(this IEnumerable<AddClient> clients)
        {
            return clients.Select(x => x.ToExtraClientDataCore());
        }
    }
}
