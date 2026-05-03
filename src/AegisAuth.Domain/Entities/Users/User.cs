using System;
using AegisAuth.Domain.Entities.Base;
using AegisAuth.Domain.Entities.Tenants;
using AegisAuth.Domain.Enums;
using AegisAuth.Domain.ValueObjects;

namespace AegisAuth.Domain.Entities.Users;

public class User : BaseEntity
{
    public Guid? TenantId { get; set; }

    public UserType UserType { get; set; }
    public EmailAddress Email { get; set; }
    public string? NormalizedEmail { get; set; }
    public string PasswordHash { get; set; }

    public bool MfaEnabled { get; set; } = false;
    public string? MfaSecretKey { get; set; }

    public bool IsActive { get; set; } = true;
    public bool IsLockedOut { get; set; } = false;
    public DateTimeOffset? LockoutEnd { get; set; }
    public int AccessFailedCount { get; set; } = 0;

    public DateTimeOffset? LastLoginAt { get; set; }
    public DateTimeOffset? LastLogoutAt { get; set; }

    public UserProfile UserProfile { get; set; }
    public Tenant Tenant { get; set; }

}
