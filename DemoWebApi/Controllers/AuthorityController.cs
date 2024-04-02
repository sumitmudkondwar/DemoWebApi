using DemoWebApi.Authority;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoWebApi.Controllers
{
    [ApiController]
    public class AuthorityController(IConfiguration configuration): ControllerBase
    {
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] AppCredential appCredential)
        {
            if (AppRepository.Authenticate(appCredential.ClientId, appCredential.Secret))
            {
                var expiresAt = DateTime.UtcNow.AddMinutes(10);

                return Ok(new
                {
                    access_token = CreateToken(appCredential.ClientId, expiresAt),
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

        private string CreateToken(string clientId, DateTime expiresAt)
        {
            
            var app = AppRepository.GetApplicationByClientId(clientId);

            var claims = new List<Claim>
            {
                new("AppName", app?.ApplicationName??string.Empty),
                new("Read", (app?.Scopes??string.Empty).Contains("read")?"true":"false"),
                new("Write", (app?.Scopes??string.Empty).Contains("write")?"true":"false")
            };

            var secretKey = Encoding.ASCII.GetBytes(configuration.GetValue<string>("SecretKey"));

            var jwt = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature),
                claims: claims,
                expires: expiresAt,
                notBefore: DateTime.UtcNow
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
