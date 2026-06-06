using AegisAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AegisAuth.Persistence.Configurations;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("Tenants");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name).IsRequired().HasMaxLength(200);
        builder.HasIndex(t => t.Name).IsUnique();

        builder.Property(t => t.Domain).IsRequired().HasMaxLength(200);

        builder.Property(t => t.Email).IsRequired().HasMaxLength(200);
        builder.Property(t => t.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.Property(t => t.Address).IsRequired().HasMaxLength(500);
        builder.Property(t => t.Description).HasMaxLength(1000);
        builder.Property(t => t.IsActive).IsRequired();
        builder.Property(t => t.EmailConfirmed).IsRequired();

        // Configure relationships
        builder.HasMany(t => t.Users)
            .WithOne(u => u.Tenant)
            .HasForeignKey(u => u.TenantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Clients)
            .WithOne(c => c.Tenant)
            .HasForeignKey(c => c.TenantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.RefreshTokens)
            .WithOne(rt => rt.Tenant)
            .HasForeignKey(rt => rt.TenantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Roles)
            .WithOne(r => r.Tenant)
            .HasForeignKey(r => r.TenantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Scopes)
            .WithOne(s => s.Tenant)
            .HasForeignKey(s => s.TenantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
