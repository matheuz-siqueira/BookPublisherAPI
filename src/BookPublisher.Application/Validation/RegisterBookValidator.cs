using System.Data;
using BookPublisher.Application.Dtos.Book;
using FluentValidation;

namespace BookPublisher.Application.Validation;

public class RegisterBookValidator : AbstractValidator<RegisterBookRequestJson>
{
    public RegisterBookValidator()
    {
        RuleFor(t => t.Title).NotEmpty().WithMessage("title is required")
            .MinimumLength(5).WithMessage("title must have at least 5 characters")
            .MaximumLength(50).WithMessage("title must have a maximum of 50 characters"); 

        RuleFor(t => t.SubTitle).MinimumLength(5).WithMessage("subtitle must have at least 5 characters")
            .MaximumLength(50).WithMessage("subtitle must have a maximum of 50 characters"); 
        
        RuleFor(t => t.Price).NotEmpty().WithMessage("price is required")
            .GreaterThan(0).WithMessage("price must be greater than zero"); 

        RuleFor(t => t.LaunchDate).NotEmpty().WithMessage("launch date is required")
            .Must(BeValidLaunchDate)
                .WithMessage("launch date cannot be later than today's date"); 

        RuleFor(t => t.Edition).NotEmpty().WithMessage("edition is required")
            .GreaterThan(0).WithMessage("invalid edition number"); 

        RuleFor(t => t.Quantity).NotEmpty().WithMessage("quantity is required")
            .GreaterThan(0).WithMessage("quantity must be greater than zero");
    }

    private bool BeValidLaunchDate(DateOnly date) 
    {
        return !(date > DateOnly.FromDateTime(DateTime.Now)); 
    }
}
