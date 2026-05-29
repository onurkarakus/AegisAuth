using AegisAuth.API.Services;
using AegisAuth.Application.Common.Interfaces;

namespace AegisAuth.API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentTenantService, CurrentTenantService>();

        return services;
    }

}
