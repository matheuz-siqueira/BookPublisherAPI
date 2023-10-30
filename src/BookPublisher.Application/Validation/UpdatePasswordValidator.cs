using BookPublisher.Application.Dtos.Author;
using FluentValidation;

namespace BookPublisher.Application.Validation;

public class UpdatePasswordValidator : AbstractValidator<UpdatePasswordRequestJson>
{
    public UpdatePasswordValidator()
    {
        RuleFor(u => u.CurrentPassword).NotEmpty().WithMessage("current password is required")
            .MinimumLength(8).WithMessage("current password must be at least 8 characters");

        RuleFor(u => u.NewPassword).NotEmpty().WithMessage("new password is required")
            .MinimumLength(8).WithMessage("new password must be at least 8 characters");
    }
}
