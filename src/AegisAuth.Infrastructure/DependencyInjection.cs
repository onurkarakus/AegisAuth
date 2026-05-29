using AegisAuth.Application.Common.Interfaces;
using AegisAuth.Infrastructure.Common;
using Microsoft.Extensions.DependencyInjection;

namespace AegisAuth.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
