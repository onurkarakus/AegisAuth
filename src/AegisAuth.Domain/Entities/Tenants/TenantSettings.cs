namespace AegisAuth.Domain.Entities.Tenants;

public record TenantSettings
{
    public int AccessTokenLifetimeSeconds { get; init; } = 3600;
    public int RefreshTokenLifetimeSeconds { get; init; } = 2592000;
    public int AbsoluteRefreshTokenLifetimeSeconds { get; init; } = 7776000;
    public bool RequireMfa { get; init; } = false;
    public bool RequireConsent { get; init; } = true;
    public bool AllowRememberConsent { get; init; } = true;
    public int MaxFailedAccessAttempts { get; init; } = 5;
    public int DefaultLockoutMinutes { get; init; } = 15;

    public static TenantSettings Default => new();
}
