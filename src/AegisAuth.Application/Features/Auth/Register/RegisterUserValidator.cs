using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace AegisAuth.Application.Features.Auth.Register;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Email)
        .NotEmpty().WithMessage("E-mail Required.")
        .EmailAddress().WithMessage("Valid E-Mail Is Required.");

        RuleFor(x => x.UserName)
        .NotEmpty().WithMessage("User Name Is Required.")
        .MaximumLength(10).WithMessage("User name cannot exceed 10 characters.");

        RuleFor(x => x.Password)
        .NotEmpty().WithMessage("Password Is Required.")
        .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

        RuleFor(x => x.FullName)
        .NotEmpty().WithMessage("Full Name Is Required.")
        .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters.");
    }

}
