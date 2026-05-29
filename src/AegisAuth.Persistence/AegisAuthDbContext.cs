using AegisAuth.Application.Common.Interfaces;
using AegisAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AegisAuth.Persistence;

public class AegisAuthDbContext : DbContext, IApplicationDbContext
{
    private readonly ICurrentTenantService currentTenantService;

    public AegisAuthDbContext(DbContextOptions options, ICurrentTenantService currentTenantService) : base(options)
    {
        this.currentTenantService = currentTenantService;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Tenant> Tenants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasQueryFilter(e => !currentTenantService.TenantId.HasValue || e.TenantId == currentTenantService.TenantId.Value);
        modelBuilder.Entity<Client>().HasQueryFilter(e => !currentTenantService.TenantId.HasValue || e.TenantId == currentTenantService.TenantId.Value);
        modelBuilder.Entity<RefreshToken>().HasQueryFilter(e => !currentTenantService.TenantId.HasValue || e.TenantId == currentTenantService.TenantId.Value);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AegisAuthDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
