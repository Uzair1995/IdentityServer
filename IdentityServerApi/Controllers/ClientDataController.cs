using IdentityServer.Repositories.Interfaces;
using IdentityServer.Repositories.Models;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServerApi.Utils.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;

namespace IdentityServerApi.Controllers
{
    //[MultiplePoliciesAuthorizeAttribute("AllAdmins;AllSupports")]
    [Authorize(AuthenticationSchemes = "bearerForAnyone")]
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
            if (!ReadAllowedScopesForAdminAndSupport())
                return Unauthorized();

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
            if (!ReadAllowedScopesForAdminAndSupport())
                return Unauthorized();

            var clientData = await clientDataRepository.GetClientDataUsingId(clientId);
            return Ok(clientData);
        }

        private bool ReadAllowedScopesForAdminAndSupport()
        {
            var claims = User.Claims.Where(x => x.Type.Equals("scope", StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Value);
            return claims.Any(x => x.Contains("admin", StringComparison.InvariantCultureIgnoreCase) || x.Contains("support", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
