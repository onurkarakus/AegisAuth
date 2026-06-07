using AegisAuth.Domain.Responses;

namespace AegisAuth.Application.Common.Interfaces;

public interface ITokenService
{
    GenerateJwtTokenResponse GenerateToken(string clientId, IEnumerable<string> scopes);
}
