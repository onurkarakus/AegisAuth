namespace AegisAuth.Application.Common.Interfaces;

public interface ITokenService
{
    string GenerateToken(string clientId, string clientSecret);
}
