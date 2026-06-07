namespace AegisAuth.Domain.Responses;

public class GenerateJwtTokenResponse
{
    public string AccessToken { get; set; }
    public int ExpiresIn { get; set; }
}
