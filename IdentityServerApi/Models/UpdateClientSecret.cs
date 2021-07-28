using IdentityServer4.Models;
using System.ComponentModel.DataAnnotations;

namespace IdentityServerApi.Models
{
    public class UpdateClientSecret
    {
        [Required]
        public string ClientSecret { get; set; }

        internal void UpdateToCore(IdentityServer4.EntityFramework.Entities.Client client)
        {
            client.ClientSecrets.Clear();
            var secret = new Secret(ClientSecret.Sha256());
            client.ClientSecrets.Add(new IdentityServer4.EntityFramework.Entities.ClientSecret
            {
                Type = secret.Type,
                Value = secret.Value
            });
        }
    }
}
