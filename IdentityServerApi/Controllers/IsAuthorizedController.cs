using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IdentityServerApi.Controllers
{
    [Authorize(AuthenticationSchemes = "bearerForAnyone")]
    [ApiController]
    [Route("connect/isAuthorized")]
    public class IsAuthorizedController : ControllerBase
    {
        [HttpGet]
        public IActionResult IsAuthorized()
        {
            var claims = User.Identities.First().Claims.Select(x => new Claim(x.Type, x.Value, x.ValueType, x.Issuer, x.OriginalIssuer)).ToList();
            return Ok(claims);
        }
    }
}
