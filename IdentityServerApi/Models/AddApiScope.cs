using System.ComponentModel.DataAnnotations;
using IdentityServer4.EntityFramework.Mappers;

namespace IdentityServerApi.Models
{
    public class AddApiScope
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string DisplayName { get; set; }

        internal IdentityServer4.EntityFramework.Entities.ApiScope ToCore()
        {
            return new IdentityServer4.Models.ApiScope(Name, DisplayName).ToEntity();
        }
    }
}
