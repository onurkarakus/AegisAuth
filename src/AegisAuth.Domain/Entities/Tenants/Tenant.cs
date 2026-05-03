using AegisAuth.Domain.Entities.Base;

namespace AegisAuth.Domain.Entities.Tenants;

public class Tenant : BaseEntity
{
    public string Identifier { get; private set; } = null!;
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public bool MfaGloballyAwareEnabled { get; set; } = false;
}
