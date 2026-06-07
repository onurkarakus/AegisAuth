using System.Text.Json.Serialization;

namespace AegisAuth.Application.Features.OAuth.Token;

public record GenerateTokenResponse
(
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("token_type")] string TokenType,
    [property: JsonPropertyName("expires_in")] int ExpiresIn
);
