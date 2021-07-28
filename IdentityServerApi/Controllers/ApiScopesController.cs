using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IdentityServerApi.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace IdentityServerApi.Controllers
{
    [Authorize(Policy = "IdentityServerAdmin")]
    [ApiController]
    [Route("api/[controller]")]
    public class ApiScopesController : ControllerBase
    {
        private readonly ConfigurationDbContext configurationDbContext;

        public ApiScopesController(ConfigurationDbContext configurationDbContext)
        {
            this.configurationDbContext = configurationDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddScopeAsync(AddApiScope addScope)
        {
            var scopes = await configurationDbContext.ApiScopes.Where(x => x.Name.Equals(addScope.Name)).CountAsync();
            if (scopes > 0)
                return BadRequest("Api scope already exists with this name.");

            configurationDbContext.Add(addScope.ToCore());
            await configurationDbContext.SaveChangesAsync();
            return Ok("Api scope successfully added.");
        }

        [HttpGet]
        public async Task<IActionResult> GetScopesAsync()
        {
            var scopes = await configurationDbContext.ApiScopes.ToListAsync();
            return Ok(scopes.ToDto());
        }

        [HttpDelete("{scopeName}")]
        public async Task<IActionResult> DeleteScopesAsync(string scopeName)
        {
            var scope = await configurationDbContext.ApiScopes.FirstOrDefaultAsync(x => x.Name.Equals(scopeName));
            if (scope == null)
                return BadRequest("Invalid api scope entered or already deleted.");
            configurationDbContext.Remove(scope);
            await configurationDbContext.SaveChangesAsync();
            return Ok("Api scope successfully deleted.");
        }
    }
}
