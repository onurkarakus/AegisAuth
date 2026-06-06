using System.Text;
using AegisAuth.Application.Common.Interfaces;
using AegisAuth.Domain.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AegisAuth.Infrastructure.Services;

public class JwtTokenService : ITokenService
{
    private readonly IOptions<JwtOptions> _jwtOptions;
    private readonly IApplicationDbContext _dbContext;

    public JwtTokenService(IOptions<JwtOptions> jwtOptions, IApplicationDbContext dbContext)
    {
        _jwtOptions = jwtOptions;
        _dbContext = dbContext;
    }

    public string GenerateToken(string clientId, string clientSecret)
    {
        var secretKey = _jwtOptions.Value.Key;
        var issuer = _jwtOptions.Value.Issuer;
        var audience = _jwtOptions.Value.Audience;
        var expirationMinutes = _jwtOptions.Value.ExpirationMinutes;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddMinutes(expirationMinutes);

        var clientInformation = _dbContext.Clients.FirstOrDefault(c => c.ClientId == clientId && c.ClientSecretHash == clientSecret);

        var claims = new[]
        {
            new System.Security.Claims.Claim("sub", clientId),
            new System.Security.Claims.Claim("clientId", clientId),
            new System.Security.Claims.Claim("clientName", clientInformation?.Name ?? "Unknown Client"),
            new System.Security.Claims.Claim("scope", "api.read api.write")
        };




        throw new NotImplementedException();
    }
}
