using System;
using AegisAuth.Domain.Entities.Base;
using AegisAuth.Domain.Entities.Clients;
using AegisAuth.Domain.Entities.Users;

namespace AegisAuth.Domain.Entities.Tokens;

public class RefreshToken : BaseEntity
{
    public string TokenHash { get; set; }
    public Guid TenantId { get; set; }
    public Guid UserId { get; set; }
    public Guid ClientId { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset? ConsumedAt { get; set; }
    public string? ConsumedByTokenHash { get; set; }
    public bool IsRevoked { get; set; } = false;
    public DateTimeOffset? RevokedAt { get; set; }
    public string? RevokedReason { get; set; }
    public Guid? ParentTokenId { get; set; }
    public RefreshToken? ParentToken { get; set; }
    public ICollection<RefreshToken> ChildTokens { get; set; } = new List<RefreshToken>();
    public User User { get; set; }
    public Client Client { get; set; }


}
