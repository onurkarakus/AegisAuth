using AegisAuth.Domain.Shared;
using MediatR;

namespace AegisAuth.Application.Features.OAuth.Token;

public record GenerateTokenCommand(
    string GrantType,
    string ClientId,
    string ClientSecret
) : IRequest<Result<GenerateTokenResponse>>;
