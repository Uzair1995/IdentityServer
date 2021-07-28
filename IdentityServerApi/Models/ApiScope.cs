using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServerApi.Models
{
    public class ApiScope
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string DisplayName { get; set; }
    }

    internal static class ApiScopeMapper
    {
        public static ApiScope ToDto(this IdentityServer4.EntityFramework.Entities.ApiScope scope) => new ApiScope
        {
            Name = scope.Name,
            DisplayName = scope.DisplayName
        };

        public static IEnumerable<ApiScope> ToDto(this IEnumerable<IdentityServer4.EntityFramework.Entities.ApiScope> scopes)
        {
            return scopes.Select(x => x.ToDto());
        }
    }
}
