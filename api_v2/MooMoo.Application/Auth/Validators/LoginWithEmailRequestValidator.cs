using FluentValidation;
using MooMoo.Application.Auth.DTOs;

namespace MooMoo.Application.Auth.Validators;

public class LoginWithEmailRequestValidator : AbstractValidator<LoginWithEmailRequest>
{
    public LoginWithEmailRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email format");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required");
    }
}
