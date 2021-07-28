using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace IdentityServerApi.Models
{
    public class UpdateClient
    {
        [Required]
        public bool? Enabled { get; set; }

        public string Description { get; set; }

        [Required]
        public List<string> AllowedScopes { get; set; }

        [Required]
        public int? AccessTokenLifetimeInSeconds { get; set; }

        internal void UpdateToCore(IdentityServer4.EntityFramework.Entities.Client client)
        {
            client.AllowedScopes.Clear();
            client.AllowedScopes.AddRange(AllowedScopes.Select(x => new IdentityServer4.EntityFramework.Entities.ClientScope
            {
                Scope = x
            }));

            client.Enabled = Enabled.Value;
            client.Description = Description;
            client.AccessTokenLifetime = AccessTokenLifetimeInSeconds.Value;
        }
    }
}
