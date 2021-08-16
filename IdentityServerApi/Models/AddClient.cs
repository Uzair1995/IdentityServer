using IdentityServer4.Models;
using System.ComponentModel.DataAnnotations;
using IdentityServer4.EntityFramework.Mappers;
using System.Collections.Generic;
using IdentityServer.Repositories.Models;

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


        internal IdentityServer4.EntityFramework.Entities.Client ToCore()
        {
            return new IdentityServer4.Models.Client
            {
                Enabled = Enabled.Value,
                ClientId = ClientId,
                Description = Description,
                ClientSecrets = { new Secret(ClientSecret.Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = AllowedScopes,
                AccessTokenLifetime = AccessTokenLifetimeInSeconds.Value
            }.ToEntity();
        }

        internal ClientData ToExtraClientDataCore()
        {
            return new ClientData
            {
                ClientId = ClientId,
                Email = Email,
                EmailTemplate = EmailTemplate,
                FirmUserBelongsTo = FirmUserBelongsTo,
                CorrespondentCode = CorrespondentCode,
                BoothId = BoothId,
                Office = Office
            };
        }
    }
}
