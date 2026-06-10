using AegisAuth.Application.Common.Interfaces;
using AegisAuth.Domain.Errors;
using AegisAuth.Domain.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AegisAuth.Application.Features.OAuth.Token;

public class GenerateTokenHandler : IRequestHandler<GenerateTokenCommand, Result<GenerateTokenResponse>>
{
    private const string CLIENT_CREDENTIALS_KEY = "client_credentials";
    private readonly IApplicationDbContext dbContext;
    private readonly IPasswordHasher passwordHasher;
    private readonly ITokenService tokenService;

    public GenerateTokenHandler(IApplicationDbContext dbContext, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        this.dbContext = dbContext;
        this.passwordHasher = passwordHasher;
        this.tokenService = tokenService;
    }

    public async Task<Result<GenerateTokenResponse>> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
    {
        if (request.GrantType != CLIENT_CREDENTIALS_KEY)
        {
            return await Task.FromResult(Result.Failure<GenerateTokenResponse>(DomainErrors.Auth.UnsupportedGrantType));
        }

        var client = await dbContext.Clients
            .IgnoreQueryFilters()
            .Include(c => c.ClientScopes)
            .ThenInclude(cs => cs.Scope)
            .FirstOrDefaultAsync(c => c.ClientId == request.ClientId, cancellationToken);

        if (client == null)
        {
            return await Task.FromResult(Result.Failure<GenerateTokenResponse>(DomainErrors.Auth.InvalidClient));
        }

        var isPasswordValid = passwordHasher.VerifyPassword(client.ClientSecretHash, request.ClientSecret);

        if (!isPasswordValid)
        {
            return await Task.FromResult(Result.Failure<GenerateTokenResponse>(DomainErrors.Auth.InvalidClient));
        }

        var scopes = client.ClientScopes.Select(cs => cs.Scope.Name).ToList();
        var accessToken = tokenService.GenerateToken(client.Id.ToString(), scopes);

        return await Task.FromResult(Result.Success(new GenerateTokenResponse
        (
            AccessToken: accessToken.AccessToken,
            TokenType: "Bearer",
            ExpiresIn: accessToken.ExpiresIn
        )));
    }
}
