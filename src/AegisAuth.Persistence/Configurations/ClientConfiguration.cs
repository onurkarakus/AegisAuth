using System;
using AegisAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AegisAuth.Persistence.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.ClientId).IsRequired().HasMaxLength(100);
        builder.HasIndex(c => c.ClientId).IsUnique();

        builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
        builder.Property(c => c.ClientSecretHash).IsRequired();

        builder.HasMany(c => c.ClientScopes)
            .WithOne(cs => cs.Client)
            .HasForeignKey(cs => cs.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
