using AegisAuth.Domain.Shared;
using MediatR;

namespace AegisAuth.Application.Features.Auth.Login;

public record LoginCommand(
    string Email,
    string Password
) : IRequest<Result<LoginResponse>>;
