using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AegisAuth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AegisAuth.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Client> Clients { get; set; }
    DbSet<RefreshToken> RefreshTokens { get; set; }
    DbSet<Tenant> Tenants { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
