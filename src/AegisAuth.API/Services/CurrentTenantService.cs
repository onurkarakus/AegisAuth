using AegisAuth.Application.Common.Interfaces;

namespace AegisAuth.API.Services;

public class CurrentTenantService : ICurrentTenantService
{
    public readonly IHttpContextAccessor httpContextAccessor;
    public CurrentTenantService(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public Guid? TenantId => GetTenantIdFromClaims();

    private Guid? GetTenantIdFromClaims()
    {
        var tenantIdClaim = httpContextAccessor?.HttpContext?.Request.Headers.ContainsKey("X-Tenant-Id") == true
            ? httpContextAccessor.HttpContext.Request.Headers["X-Tenant-Id"].FirstOrDefault()
            : string.Empty;

        if (!string.IsNullOrEmpty(tenantIdClaim) && Guid.TryParse(tenantIdClaim, out var tenantId))
        {
            return tenantId;
        }

        return null;
    }
}
