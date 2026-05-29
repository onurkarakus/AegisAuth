using AegisAuth.Domain.Shared;
using AegisAuth.Domain.Errors;
using AegisAuth.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AegisAuth.Application.Features.Auth.Register;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<RegisterUserResponse>>
{
    private readonly IApplicationDbContext dbContext;
    private readonly IPasswordHasher passwordHasher;

    public RegisterUserHandler(IApplicationDbContext dbContext, IPasswordHasher passwordHasher)
    {
        this.dbContext = dbContext;
        this.passwordHasher = passwordHasher;
    }

    public async Task<Result<RegisterUserResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await dbContext.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
        {
            return Result.Failure<RegisterUserResponse>(DomainErrors.ValidationErrors.ValidationFailed);
        }

        var passwordHash = passwordHasher.HashPassword(request.Password);

        var newUser = Domain.Entities.User.Create(request.TenantId, request.Email, request.UserName, request.FullName, passwordHash);
        dbContext.Users.Add(newUser);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(new RegisterUserResponse(request.Email, "User registered successfully.", newUser.Id.ToString()));
    }
}
