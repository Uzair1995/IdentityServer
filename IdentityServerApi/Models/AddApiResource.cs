using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IdentityServer4.EntityFramework.Mappers;

namespace IdentityServerApi.Models
{
    public class AddApiResource
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public List<string> AllowedScopes { get; set; }

        internal IdentityServer4.EntityFramework.Entities.ApiResource ToCore()
        {
            return new IdentityServer4.Models.ApiResource(Name, DisplayName)
            {
                Scopes = AllowedScopes
            }.ToEntity();
        }
    }
}
