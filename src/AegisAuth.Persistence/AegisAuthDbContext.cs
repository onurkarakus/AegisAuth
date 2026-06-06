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
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Scope> Scopes { get; set; }
    public DbSet<ClientScope> ClientScopes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasQueryFilter(e => e.TenantId == currentTenantService.TenantId.Value);
        modelBuilder.Entity<Client>().HasQueryFilter(e => e.TenantId == currentTenantService.TenantId.Value);
        modelBuilder.Entity<RefreshToken>().HasQueryFilter(e => e.TenantId == currentTenantService.TenantId.Value);
        modelBuilder.Entity<Role>().HasQueryFilter(e => e.TenantId == currentTenantService.TenantId.Value);
        modelBuilder.Entity<UserRole>().HasQueryFilter(e => e.TenantId == currentTenantService.TenantId.Value);
        modelBuilder.Entity<Scope>().HasQueryFilter(e => e.TenantId == currentTenantService.TenantId.Value);
        modelBuilder.Entity<ClientScope>().HasQueryFilter(e => e.TenantId == currentTenantService.TenantId.Value);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AegisAuthDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
