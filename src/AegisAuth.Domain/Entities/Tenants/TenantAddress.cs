using AegisAuth.Domain.Entities.Base;

namespace AegisAuth.Domain.Entities.Tenants;

public class TenantAddress : BaseEntity
{
    public Guid TenantId { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }

    public Tenant Tenant { get; set; }
}
