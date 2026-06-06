using System.ComponentModel.DataAnnotations;

namespace AegisAuth.Domain.Options;

public class JwtOptions
{
    public const string SectionName = "Jwt";

    [Required(AllowEmptyStrings = false)]
    public string Key { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Issuer { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Audience { get; set; } = null!;

    [Range(1, int.MaxValue)]
    public int ExpirationMinutes { get; set; }

}
