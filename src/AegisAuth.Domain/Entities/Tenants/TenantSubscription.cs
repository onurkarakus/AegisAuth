using System;
using System.Collections.Generic;
using System.Text;
using AegisAuth.Domain.Entities.Base;

namespace AegisAuth.Domain.Entities.Tenants;

public class TenantSubscription : BaseEntity
{
    public Guid TenantId { get; set; }
    public DateTimeOffset ExpiryDate { get; set; }
    public int MaxClients { get; set; }
    public int MaxUsers { get; set; }

    public Tenant Tenant { get; set; }

}
