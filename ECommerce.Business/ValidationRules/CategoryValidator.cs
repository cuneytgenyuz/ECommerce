using ECommerce.Models.Dtos.CategoryDtos;
using FluentValidation;

namespace ECommerce.Business.ValidationRules;

public class CategoryValidator : AbstractValidator<CategoryDto>
{
    public CategoryValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(50).WithMessage("Category name cannot exceed 50 characters.");

        RuleFor(c => c.Name)
            .MaximumLength(200).WithMessage("Category description cannot exceed 250 characters.");
    }
}