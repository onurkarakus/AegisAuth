using AegisAuth.Domain.Shared;
using MediatR;
namespace AegisAuth.Application.Features.Auth.Register;

public record RegisterUserCommand(
Guid TenantId,
string Email,
string UserName,
string FullName,
string Password
) : IRequest<Result<RegisterUserResponse>>;
