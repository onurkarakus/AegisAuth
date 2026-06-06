using AegisAuth.Domain.Common;

namespace AegisAuth.Domain.Entities;

public class UserRole : Entity
{
    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }
    public Guid TenantId { get; private set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;
    public Tenant Tenant { get; set; } = null!;

    private UserRole() { }

    public static UserRole Create(Guid tenantId, Guid userId, Guid roleId)
    {
        if (tenantId == Guid.Empty)
        {
            throw new ArgumentException("TenantId cannot be empty.", nameof(tenantId));
        }

        if (userId == Guid.Empty)
        {
            throw new ArgumentException("UserId cannot be empty.", nameof(userId));
        }

        if (roleId == Guid.Empty)
        {
            throw new ArgumentException("RoleId cannot be empty.", nameof(roleId));
        }

        return new UserRole
        {
            TenantId = tenantId,
            UserId = userId,
            RoleId = roleId
        };
    }
}
