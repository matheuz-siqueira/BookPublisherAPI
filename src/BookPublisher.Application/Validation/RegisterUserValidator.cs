using BookPublisher.Application.Dtos.User;
using FluentValidation;

namespace BookPublisher.Application.Validation;

public class RegisterUserValidator : AbstractValidator<RegisterUserRequestJon>
{
    public RegisterUserValidator()
    {
        RuleFor(u => u.FullName)
            .NotEmpty().WithMessage("fullname is required")
            .MinimumLength(3).WithMessage("fullname must have at least 3 characters")
            .MaximumLength(40).WithMessage("fullname must have a maximum of 40 characters");

        RuleFor(u => u.UserName)
            .NotEmpty().WithMessage("username is required")
            .MinimumLength(6).WithMessage("username must have at least 3 characters")
            .MaximumLength(15).WithMessage("username must have a maximum of 40 characters");

        RuleFor(u => u.Password).NotEmpty().WithMessage("password is required")
            .MinimumLength(8).WithMessage("username must have at least 8 characters");        
    }
}
