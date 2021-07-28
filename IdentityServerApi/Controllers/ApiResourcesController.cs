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
    public class ApiResourcesController : ControllerBase
    {
        private readonly ConfigurationDbContext configurationDbContext;

        public ApiResourcesController(ConfigurationDbContext configurationDbContext)
        {
            this.configurationDbContext = configurationDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddApiResourceAsync(AddApiResource addResources)
        {
            var resources = await configurationDbContext.ApiResources.Where(x => x.Name.Equals(addResources.Name)).CountAsync();
            if (resources > 0)
                return BadRequest("Api resource already exists with this name.");

            if (addResources.AllowedScopes.Count < 1)
                return BadRequest("Api scope must be defined.");

            var scopes = await configurationDbContext.ApiScopes.Select(x => x.Name).ToListAsync();
            var validScopes = addResources.AllowedScopes.All(x => scopes.Contains(x));
            if (!validScopes)
                return BadRequest("Invalid api scopes.");

            configurationDbContext.Add(addResources.ToCore());
            await configurationDbContext.SaveChangesAsync();
            return Ok("Api resource successfully added.");
        }

        [HttpGet]
        public async Task<IActionResult> GetApiResourceAsync()
        {
            var resources = await configurationDbContext.ApiResources.Include(s => s.Scopes).ToListAsync();
            return Ok(resources.ToDto());
        }

        [HttpDelete("{resourceName}")]
        public async Task<IActionResult> DeleteApiResourceAsync(string resourceName)
        {
            var resource = await configurationDbContext.ApiResources.FirstOrDefaultAsync(x => x.Name.Equals(resourceName));
            if (resource == null)
                return BadRequest("Invalid api resource entered or already deleted.");
            configurationDbContext.Remove(resource);
            await configurationDbContext.SaveChangesAsync();
            return Ok("Api resource successfully deleted.");
        }
    }
}
