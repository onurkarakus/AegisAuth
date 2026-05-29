using AegisAuth.Application.Common.Interfaces;
using AegisAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AegisAuth.Persistence;

public class AegisAuthDbContext : DbContext, IApplicationDbContext
{
    public AegisAuthDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AegisAuthDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
