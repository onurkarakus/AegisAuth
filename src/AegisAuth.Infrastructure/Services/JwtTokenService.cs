using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AegisAuth.Application.Common.Interfaces;

using AegisAuth.Domain.Options;
using AegisAuth.Domain.Responses;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace AegisAuth.Infrastructure.Services;

public class JwtTokenService : ITokenService
{
    private readonly IOptions<JwtOptions> _jwtOptions;

    public JwtTokenService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }

    public GenerateJwtTokenResponse GenerateToken(string clientId, IEnumerable<string> scopes)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, clientId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("scope", string.Join(" ", scopes))
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.Value.ExpirationMinutes),
            Issuer = _jwtOptions.Value.Issuer,
            Audience = _jwtOptions.Value.Audience,
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new GenerateJwtTokenResponse
        {
            AccessToken = tokenHandler.WriteToken(token),
            ExpiresIn = tokenDescriptor.Expires.HasValue ? (int)(tokenDescriptor.Expires.Value - DateTime.UtcNow).TotalSeconds : 0
        };
    }
}
