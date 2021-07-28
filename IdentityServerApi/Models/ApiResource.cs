using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace IdentityServerApi.Models
{
    public class ApiResource
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public IEnumerable<string> AllowedScopes { get; set; }
    }

    internal static class ApiResourceMapper
    {
        public static ApiResource ToDto(this IdentityServer4.EntityFramework.Entities.ApiResource resource) => new ApiResource
        {
            Name = resource.Name,
            DisplayName = resource.DisplayName,
            AllowedScopes = resource.Scopes.Select(x => x.Scope)
        };

        public static IEnumerable<ApiResource> ToDto(this IEnumerable<IdentityServer4.EntityFramework.Entities.ApiResource> resources)
        {
            return resources.Select(x => x.ToDto());
        }
    }
}
