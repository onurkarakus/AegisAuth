using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AegisAuth.Domain.Common;

namespace AegisAuth.Domain.Entities;

public class ClientScope : Entity
{
    public Guid ClientId { get; private set; }
    public Guid ScopeId { get; private set; }
    public Guid TenantId { get; private set; }

    // Navigation properties
    public Client Client { get; set; } = null!;
    public Scope Scope { get; set; } = null!;
    public Tenant Tenant { get; set; } = null!;

    private ClientScope() { }

    public static ClientScope Create(Guid clientId, Guid scopeId, Guid tenantId)
    {
        if (clientId == Guid.Empty)
        {
            throw new ArgumentException("ClientId cannot be empty.", nameof(clientId));
        }

        if (scopeId == Guid.Empty)
        {
            throw new ArgumentException("ScopeId cannot be empty.", nameof(scopeId));
        }

        if (tenantId == Guid.Empty)
        {
            throw new ArgumentException("TenantId cannot be empty.", nameof(tenantId));
        }

        return new ClientScope
        {
            ClientId = clientId,
            ScopeId = scopeId,
            TenantId = tenantId
        };
    }
}
