using DemoWebApi.Authority;
using DemoWebApi.Filters.AuthFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoWebApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    public class AuthorityController(IConfiguration configuration): ControllerBase
    {
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] AppCredential appCredential)
        {
            if (Authenticator.Authenticate(appCredential.ClientId, appCredential.Secret))
            {
                var expiresAt = DateTime.UtcNow.AddMinutes(10);

                return Ok(new
                {
                    access_token = Authenticator.CreateToken(appCredential.ClientId, expiresAt, configuration.GetValue<string>("SecretKey")),
                    expires_at = expiresAt
                });
            }
            else
            {
                ModelState.AddModelError("Unauthorized", "You are not authorized.");
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status401Unauthorized
                };
                return new UnauthorizedObjectResult(problemDetails);
            }
        }
    }
}
