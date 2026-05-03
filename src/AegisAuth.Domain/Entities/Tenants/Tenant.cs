using AegisAuth.Domain.Entities.Base;

namespace AegisAuth.Domain.Entities.Tenants;

public class Tenant : BaseEntity
{
    public string Identifier { get; private set; } = null!;
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public bool MfaGloballyAwareEnabled { get; set; } = false;
    public TenantSettings Settings { get; private set; } = null!;
    public TenantAddress Address { get; set; }
    public ICollection<TenantSubscription> Subscriptions { get; set; } = new List<TenantSubscription>();

    public static Tenant Create(string identifier, string name, TenantAddress address, TenantSettings? settings = null)
    {
        if (string.IsNullOrWhiteSpace(identifier))
            throw new ArgumentException("Identifier required", nameof(identifier));

        if (!IsValidIdentifier(identifier))
            throw new ArgumentException("Only lowercase, numbers, hyphens", nameof(identifier));

        var newTenantId = Guid.NewGuid();
        return new Tenant
        {
            Id = newTenantId,
            Identifier = identifier.ToLowerInvariant(),
            Name = name,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            Settings = settings ?? TenantSettings.Default,
            Address = address,
            Subscriptions =
            [
                new TenantSubscription {
                Id = Guid.NewGuid(),
                TenantId = newTenantId,
                CreatedAt = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(1),
                MaxClients = 10,
                MaxUsers = 100,
            }]
        };
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;

    private static bool IsValidIdentifier(string id) =>
        System.Text.RegularExpressions.Regex.IsMatch(id, @"^[a-z0-9-]+$");

}
