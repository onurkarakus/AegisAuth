using System;
using AegisAuth.Domain.Entities.Base;
using AegisAuth.Domain.Entities.Clients;

namespace AegisAuth.Domain.Entities.Users;

public class UserConsent : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ClientId { get; set; }
    public string Scopes { get; set; } // "openid profile email api:read"
    public DateTimeOffset? ExpiresAt { get; set; }
    public User User { get; set; }
    public Client Client { get; set; }
}
