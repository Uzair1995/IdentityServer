using IdentityServer.Repositories.Interfaces;
using IdentityServer.Repositories.Models;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientDataController : ControllerBase
    {
        private readonly IClientDataRepository clientDataRepository;
        private readonly ConfigurationDbContext configurationDbContext;

        public ClientDataController(IClientDataRepository clientDataRepository, ConfigurationDbContext configurationDbContext)
        {
            this.clientDataRepository = clientDataRepository;
            this.configurationDbContext = configurationDbContext;
        }

        [HttpPost("AddExtraClientData")]
        public async Task<IActionResult> AddExtraClientData([FromBody] ClientData clientData)
        {
            var client = configurationDbContext.Clients.Where(x => x.ClientId.Equals(clientData.ClientId)).FirstOrDefault();

            if (client == null)
            {
                return NotFound("Client does not exist.");
            }

            await clientDataRepository.InsertOrUpdateClientDataAsync(clientData);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetClientDataByClientIdAsync(string clientId)
        {
            var clientData = await clientDataRepository.GetClientDataUsingId(clientId);
            return Ok(clientData);
        }
    }
}
