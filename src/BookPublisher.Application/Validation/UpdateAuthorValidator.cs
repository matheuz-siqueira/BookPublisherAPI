using BookPublisher.Application.Dtos.Author;
using FluentValidation;

namespace BookPublisher.Application.Validation;

public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorRequestJson>
{
    public UpdateAuthorValidator()
    {
        RuleFor(a => a.FirstName)
            .NotEmpty().WithMessage("first name cannot be empty")
            .MinimumLength(3).WithMessage("first name must be at least 3 characters")
            .MaximumLength(25).WithMessage("first name must have a maximum of 25 characters");

        RuleFor(a => a.LastName)
            .NotEmpty().WithMessage("last name cannot be empty")
            .MinimumLength(3).WithMessage("last name must be at least 3 characters")
            .MaximumLength(25).WithMessage("last name must have a maximum of 25 characters");

        RuleFor(a => a.Gender)
            .NotEmpty().WithMessage("gender cannot be empty")
            .Must(GenderValidation).WithMessage("gender must be male or female");
    }

    private bool GenderValidation(string gender)
    {
        if(gender != "Male" && gender != "Female")
        {
            return false; 
        }
        return true; 
    }
}
