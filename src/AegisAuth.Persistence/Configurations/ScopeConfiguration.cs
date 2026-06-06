using AegisAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AegisAuth.Persistence.Configurations;

public class ScopeConfiguration : IEntityTypeConfiguration<Scope>
{
    public void Configure(EntityTypeBuilder<Scope> builder)
    {
        builder.ToTable("Scopes");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
        builder.HasIndex(s => s.Name).IsUnique();

        builder.HasOne(s => s.Tenant)
            .WithMany(t => t.Scopes)
            .HasForeignKey(s => s.TenantId);

        builder.HasMany(s => s.ClientScopes)
            .WithOne(cs => cs.Scope)
            .HasForeignKey(cs => cs.ScopeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
