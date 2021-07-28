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
    public class ClientsController : ControllerBase
    {
        private readonly ConfigurationDbContext configurationDbContext;

        public ClientsController(ConfigurationDbContext configurationDbContext)
        {
            this.configurationDbContext = configurationDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddClientAsync(AddClient addClient)
        {
            var clients = await configurationDbContext.Clients.Where(x => x.ClientId.Equals(addClient.ClientId)).CountAsync();
            if (clients > 0)
                return BadRequest("Client id already exists with this name.");

            if (addClient.AllowedScopes.Count < 1)
                return BadRequest("Api scope must be defined.");

            var scopes = await configurationDbContext.ApiScopes.Select(x => x.Name).ToListAsync();
            var validScopes = addClient.AllowedScopes.All(x => scopes.Contains(x));
            if (!validScopes)
                return BadRequest("Invalid api scopes.");

            var clientCore = addClient.ToCore();
            configurationDbContext.Add(clientCore);
            await configurationDbContext.SaveChangesAsync();
            return Ok("Client successfully added.");
        }

        [HttpGet]
        public async Task<IActionResult> GetClientsAsync()
        {
            var clients = await configurationDbContext.Clients.Include(s => s.AllowedScopes).ToListAsync();
            return Ok(clients.ToDto());
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetByClientIdAsync(string clientId)
        {
            var client = await configurationDbContext.Clients.Include(s => s.AllowedScopes).FirstOrDefaultAsync(x => x.ClientId.Equals(clientId));
            if (client == null)
                return NoContent();
            return Ok(client.ToDto());
        }

        [HttpDelete("{clientId}")]
        public async Task<IActionResult> DeleteClientAsync(string clientId)
        {
            var client = await configurationDbContext.Clients.FirstOrDefaultAsync(x => x.ClientId.Equals(clientId));
            if (client == null)
                return BadRequest("Invalid client id entered or already deleted.");
            configurationDbContext.Remove(client);
            await configurationDbContext.SaveChangesAsync();
            return Ok("Client successfully deleted.");
        }

        [HttpPut("{clientId}")]
        public async Task<IActionResult> UpdateClientAsync(string clientId, UpdateClient updateClient)
        {
            var client = await configurationDbContext.Clients.Include(s => s.AllowedScopes).FirstOrDefaultAsync(x => x.ClientId.Equals(clientId));
            if (client == null)
                return BadRequest("Invalid client id entered.");

            if (updateClient.AllowedScopes.Count < 1)
                return BadRequest("Scope must be defined.");

            var scopes = await configurationDbContext.ApiScopes.Select(x => x.Name).ToListAsync();
            var validScopes = updateClient.AllowedScopes.All(x => scopes.Contains(x));
            if (!validScopes)
                return BadRequest("Invalid client scopes.");

            updateClient.UpdateToCore(client);
            configurationDbContext.Update(client);
            await configurationDbContext.SaveChangesAsync();
            return Ok("Client successfully updated.");
        }

        [HttpPut("{clientId}/status")]
        public async Task<IActionResult> EnableDisableClientAsync(string clientId)
        {
            var client = await configurationDbContext.Clients.FirstOrDefaultAsync(x => x.ClientId.Equals(clientId));
            if (client == null)
                return BadRequest("Invalid client id entered.");
            client.Enabled = !client.Enabled;
            configurationDbContext.Update(client);
            await configurationDbContext.SaveChangesAsync();
            return Ok($"Client successfully {(client.Enabled ? "enabled" : "disabled")}.");
        }

        [HttpPut("{clientId}/secret")]
        public async Task<IActionResult> UpdateClientSecretAsync(string clientId, UpdateClientSecret updateClientSecret)
        {
            var client = await configurationDbContext.Clients.Include(x => x.ClientSecrets).FirstOrDefaultAsync(x => x.ClientId.Equals(clientId));
            if (client == null)
                return BadRequest("Invalid client id entered.");
            updateClientSecret.UpdateToCore(client);
            configurationDbContext.Update(client);
            await configurationDbContext.SaveChangesAsync();
            return Ok($"Client secret successfully updated.");
        }
    }
}
