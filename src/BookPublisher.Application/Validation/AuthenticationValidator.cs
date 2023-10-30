using BookPublisher.Application.Dtos.User;
using FluentValidation;

namespace BookPublisher.Application.Validation;

public class AuthenticationValidator : AbstractValidator<AuthenticationRequestJson>
{
    public AuthenticationValidator()
    {
        RuleFor(u => u.Email).NotEmpty().WithMessage("email is required")
            .EmailAddress().WithMessage("invalid email"); 

        RuleFor(u => u.Password).NotEmpty().WithMessage("password is required")
            .MinimumLength(8).WithMessage("password must have at least 8 characters");
    }
}
