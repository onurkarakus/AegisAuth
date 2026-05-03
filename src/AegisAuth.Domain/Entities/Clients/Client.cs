using AegisAuth.Domain.Entities.Base;
using AegisAuth.Domain.Entities.Tenants;
using AegisAuth.Domain.Entities.Tokens;
using AegisAuth.Domain.Entities.Users;
using AegisAuth.Domain.Enums;

namespace AegisAuth.Domain.Entities.Clients;

public class Client : BaseEntity
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; }
    public string ClientId { get; set; }
    public string ClientSecretHash { get; set; }
    public string? ClientName { get; set; }
    public string? Description { get; set; }
    public ClientType ClientType { get; set; }
    public ProtocolType ProtocolType { get; set; } = ProtocolType.OpenIdConnect;
    public bool RequireMfa { get; set; } = false;
    public bool RequireConsent { get; set; } = false;
    public bool AllowRememberConsent { get; set; } = true;
    public bool Enabled { get; set; } = true;
    public DateTimeOffset? EnabledFrom { get; set; }
    public DateTimeOffset? EnabledTo { get; set; }
    public int? MaxRequestsPerMinute { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<UserConsent> Consents { get; set; } = new List<UserConsent>();
    public ICollection<ClientRedirectUri> RedirectUris { get; set; } = new List<ClientRedirectUri>();

    public Tenant Tenant { get; set; }
}
