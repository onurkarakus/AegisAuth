using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AegisAuth.Domain.Shared;
using AegisAuth.Persistence;
using MediatR;

namespace AegisAuth.Application.Features.Auth.Register;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<RegisterUserResponse>>
{
    private readonly AegisAuthDbContext dbContext;

    public RegisterUserHandler(AegisAuthDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result<RegisterUserResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (dbContext.Users.Any(u => u.Email == request.Email))
        {
            return Result.Failure<RegisterUserResponse>("Email is already registered.");
        }

        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);


        var passwordHash = HashPassword(request.Password);

        var newUser = Domain.Entities.User.Create(request.Email, request.UserName, request.FullName, passwordHash);
        dbContext.Users.Add(newUser);
        await dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return Result.Success(new RegisterUserResponse(request.Email, "User registered successfully.", newUser.Id.ToString()));
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, workFactor: 12);
    }
}
