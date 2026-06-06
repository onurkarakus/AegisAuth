using AegisAuth.Domain.Common;

namespace AegisAuth.Domain.Entities;

public class Role : Entity
{
    public Guid TenantId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    // Navigation properties
    public Tenant Tenant { get; set; } = null!;

    public ICollection<UserRole> UserRoles { get; private set; }

    private Role() { }

    public static Role Create(Guid tenantId, string name, string description)
    {
        if (tenantId == Guid.Empty)
        {
            throw new ArgumentException("TenantId cannot be empty.", nameof(tenantId));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        }

        return new Role
        {
            TenantId = tenantId,
            Name = name,
            Description = description
        };
    }

    public void Update(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        }

        Name = name;
        Description = description;
    }
}
