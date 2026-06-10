using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AegisAuth.Application.Common.Interfaces;
using AegisAuth.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AegisAuth.Application.Features.Auth.Login;

public class LoginHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly IApplicationDbContext applicationDbContext;
    private readonly ICurrentTenantService currentTenantService;
    private readonly IPasswordHasher passwordHasher;
    private readonly IHttpContextAccessor httpContextAccessor;

    public LoginHandler(IApplicationDbContext applicationDbContext, ICurrentTenantService currentTenantService, IPasswordHasher passwordHasher, IHttpContextAccessor httpContextAccessor)
    {
        this.applicationDbContext = applicationDbContext;
        this.currentTenantService = currentTenantService;
        this.passwordHasher = passwordHasher;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (currentTenantService.TenantId == Guid.Empty)
        {
            return Result.Failure<LoginResponse>(Domain.Errors.DomainErrors.Auth.TenantNotFound);
        }

        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
        {
            return Result.Failure<LoginResponse>(Domain.Errors.DomainErrors.Auth.InvalidCredentials);
        }

        if (!await applicationDbContext.Users.AnyAsync(u => u.Email == request.Email && u.TenantId == currentTenantService.TenantId, cancellationToken))
        {
            return Result.Failure<LoginResponse>(Domain.Errors.DomainErrors.Auth.InvalidCredentials);
        }

        var user = await applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email && u.TenantId == currentTenantService.TenantId, cancellationToken);

        if (user == null)
        {
            return Result.Failure<LoginResponse>(Domain.Errors.DomainErrors.Auth.InvalidCredentials);
        }

        var isPasswordValid = passwordHasher.VerifyPassword(user.PasswordHash, request.Password);

        if (!isPasswordValid)
        {
            return await Task.FromResult(Result.Failure<LoginResponse>(Domain.Errors.DomainErrors.Auth.InvalidCredentials));
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("tenant_id",currentTenantService.TenantId.ToString()),
            new Claim("user_id", user.Id.ToString())
        };

        var userRoles = await applicationDbContext.UserRoles
                    .Where(ur => ur.UserId == user.Id)
                    .Select(ur => ur.Role.Name)
                    .ToListAsync(cancellationToken);

        var rolesClaims = userRoles.Select(role => new Claim(ClaimTypes.Role, role));
        claims.AddRange(rolesClaims);

        var claimsIdentity = new ClaimsIdentity(claims, "AegisAuthForgeCookie");

        var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
        };

        await httpContextAccessor.HttpContext?.SignInAsync("AuthForgeCookie", new ClaimsPrincipal(claimsIdentity), authProperties);

        return Result.Success(new LoginResponse { Message = "Logged in successfully" });
    }
}
