using FluentValidation;
using System.Linq.Expressions;

namespace MedicalCare.Application.Features.TestCategories.Validators;

public abstract class TestCategoryBaseValidator<T> : AbstractValidator<T>
{
    protected void ValidateName(
        Expression<Func<T, string>> selector)
    {
        RuleFor(selector)
            .NotEmpty().WithMessage("Category name is required")
            .MinimumLength(3).WithMessage("Category name must be at least 3 characters")
            .Matches(@"^[A-Za-z\s()/]+$")
            .WithMessage("Only letters, spaces, (, ), and / are allowed");
    }
}