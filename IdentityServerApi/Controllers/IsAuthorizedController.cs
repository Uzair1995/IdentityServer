using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using IdentityServer.Repositories.Interfaces;
using IdentityServer4.EntityFramework.DbContexts;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IdentityServerApi.Models;
using IdentityServer4.Models;

namespace IdentityServerApi.Controllers
{
    [Authorize(AuthenticationSchemes = "bearerForAnyone")]
    [ApiController]
    [Route("connect/isAuthorized")]
    public class IsAuthorizedController : ControllerBase
    {
        private readonly IClientDataRepository clientDataRepository;
        private readonly ConfigurationDbContext configurationDbContext;

        public IsAuthorizedController(IClientDataRepository clientDataRepository, ConfigurationDbContext configurationDbContext)
        {
            this.clientDataRepository = clientDataRepository;
            this.configurationDbContext = configurationDbContext;
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPasswordAsync(ResetPassword model)
        {
            var clientId = User.Claims.Where(x => x.Type.Equals("client_id")).FirstOrDefault().Value;
            var client = configurationDbContext.Clients.Include(s => s.ClientSecrets).Where(x => x.ClientId.Equals(clientId)).FirstOrDefault();
            if (client == null)
                return BadRequest("Client does not exist.");

            var clientSecret = client.ClientSecrets.Where(x => x.Value.Equals(model.OldPassword.Sha256())).FirstOrDefault();
            if (clientSecret == null)
                return BadRequest("The old password is wrong.");

            clientSecret.Value = model.NewPassword.Sha256();
            await configurationDbContext.SaveChangesAsync();

            return Ok("Password changed successfully");
        }

        [HttpGet]
        public IActionResult IsAuthorized()
        {
            var claims = User.Identities.First().Claims.Select(x => new Claim(x.Type, x.Value, x.ValueType, x.Issuer, x.OriginalIssuer)).ToList();
            return Ok(claims);
        }

        [HttpGet("Data")]
        public async Task<IActionResult> ClientDataAsync()
        {
            var clientId = User.Identities.First().Claims.Where(x => x.Type.Equals("client_id")).Select(x => x.Value).FirstOrDefault();
            var data = await clientDataRepository.GetClientDataUsingId(clientId);
            return Ok(data);
        }
    }
}
